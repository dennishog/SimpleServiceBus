using DS.SimpleServiceBus.Events;
using DS.SimpleServiceBus.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace DS.SimpleServiceBus.InMemory.Services
{
    public class InMemoryEventProcessor
    {
        private readonly string _path;
        private readonly string _extension;
        private readonly Dictionary<string, bool> _queues;
        private readonly Timer _timer;

        public delegate void MessageReceivedEventHandler(object sender, InMemoryMessageReceivedEventArgs e);

        public event MessageReceivedEventHandler MessageRecieved;

        public InMemoryEventProcessor(string path)
        {
            _path = path;
            _extension = "pointlesseventfileextension";
            _queues = new Dictionary<string, bool>();

            _timer = new Timer {Interval = 1000};
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;

            if (_queues.Count == 0) return;

            foreach (var file in Directory.GetFiles(_path, $"*.{_extension}"))
            {
                var queueName = Path.GetFileNameWithoutExtension(file);

                // Continue if the file does not belong to any of the queues in this instance of EventProcessor
                if (!_queues.Keys.Contains(queueName)) continue;

                // Continue if the queue is locked
                if (_queues[queueName]) continue;

                // Read all lines and trigger events
                foreach (var line in File.ReadAllLines(file))
                {
                    MessageRecieved?.Invoke(this, new InMemoryMessageReceivedEventArgs()
                    {
                        QueueName = queueName,
                        Message = new EventMessage()
                        {
                            Event = Convert.FromBase64String(line)
                        }
                    });
                }

                // All lines are read, remove lines
                File.Delete(file);
                File.Create(file).Dispose();
            }

            _timer.Enabled = true;
        }

        public async Task ConnectHandler(string queueName, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _queues.Add(queueName, false);

                if (!File.Exists($"{_path}{queueName}.{_extension}"))
                {
                    File.Create($"{_path}{queueName}.{_extension}").Dispose();
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Writes messages to all available files
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        public async Task Publish(IEventMessage message, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                foreach (var file in Directory.GetFiles(_path, $"*.{_extension}"))
                {
                    // Lock queue
                    var queueName = Path.GetFileNameWithoutExtension(file);
                    _queues[queueName] = true;

                    using (var fileStreamWriter = new StreamWriter(file, true))
                    {
                        fileStreamWriter.WriteLine(Convert.ToBase64String(message.Event));
                    }

                    // Unlock queue
                    _queues[queueName] = false;
                }
            }, cancellationToken);
        }
    }
}
