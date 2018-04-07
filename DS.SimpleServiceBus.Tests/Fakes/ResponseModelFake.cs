using System;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.Tests.Fakes
{
    [Serializable]
    public class ResponseModelFake : IResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static ResponseModelFake GetResponseModelFake()
        {
            return new ResponseModelFake
            {
                Id = 10,
                Name = "Dennis Hogström"
            };
        }
    }
}