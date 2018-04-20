using DS.SimpleServiceBus.Commands.Interfaces;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Utils;

namespace DS.SimpleServiceBus.Extensions
{
    /// <summary>
    ///     Extension methods to simplify serialization/deserialization for objects being transferred by the bus
    /// </summary>
    public static class SerializationExtension
    {
        /// <summary>
        ///     Deserializes event
        /// </summary>
        /// <param name="eventData">IEvent implementation as byte[]</param>
        /// <returns></returns>
        public static IEvent GetEvent(this byte[] eventData)
        {
            return GenericSerializer.DeSerialize<IEvent>(eventData);
        }

        /// <summary>
        ///     Deserializes responsemodel
        /// </summary>
        /// <param name="responseData">IResponseModel implementation as byte[]</param>
        /// <returns></returns>
        public static IResponseModel GetResponse(this byte[] responseData)
        {
            return GenericSerializer.DeSerialize<IResponseModel>(responseData);
        }

        /// <summary>
        ///     Deserializes requestmodel
        /// </summary>
        /// <param name="requestData">IRequestModel implementation as byte[]</param>
        /// <returns></returns>
        public static IRequestModel GetRequest(this byte[] requestData)
        {
            return GenericSerializer.DeSerialize<IRequestModel>(requestData);
        }

        /// <summary>
        ///     Serializes event
        /// </summary>
        /// <param name="event">Instance of class implementing IEvent</param>
        /// <returns></returns>
        public static byte[] ToBytes(this IEvent @event)
        {
            return GenericSerializer.Serialize(@event);
        }

        /// <summary>
        ///     Serializes requestmodel
        /// </summary>
        /// <param name="requestModel">Instance of class implementing IRequestModel</param>
        /// <returns></returns>
        public static byte[] ToBytes(this IRequestModel requestModel)
        {
            return GenericSerializer.Serialize(requestModel);
        }

        /// <summary>
        ///     Serializes responsemodel
        /// </summary>
        /// <param name="responseModel">Instance of class implementing IResponseModel</param>
        /// <returns></returns>
        public static byte[] ToBytes(this IResponseModel responseModel)
        {
            return GenericSerializer.Serialize(responseModel);
        }
    }
}