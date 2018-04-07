using System;
using DS.SimpleServiceBus.Commands.Interfaces;

namespace DS.SimpleServiceBus.Tests.Fakes
{
    [Serializable]
    public class RequestModelFake : IRequestModel
    {
        public int Id { get; set; }

        public static RequestModelFake GetRequestModelFake()
        {
            return new RequestModelFake
            {
                Id = 10
            };
        }
    }
}