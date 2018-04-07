using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Utils;

namespace DS.SimpleServiceBus.Extensions
{
    public static class SerializationExtension
    {
        public static IEvent FromBytes(this byte[] eventData)
        {
            return GenericSerializer.DeSerialize<IEvent>(eventData);
        }

        public static T GetResponse<T>(this byte[] responseData)
        {
            return GenericSerializer.DeSerialize<T>(responseData);
        }

        public static IRequestModel GetRequest(this byte[] requestData)
        {
            return GenericSerializer.DeSerialize<IRequestModel>(requestData);
        }

        public static byte[] ToBytes(this IEvent @event)
        {
            return GenericSerializer.Serialize(@event);
        }

        public static byte[] ToBytes(this IRequestModel requestModel)
        {
            return GenericSerializer.Serialize(requestModel);
        }

        public static byte[] ToBytes(this IResponseModel responseModel)
        {
            return GenericSerializer.Serialize(responseModel);
        }
    }
}