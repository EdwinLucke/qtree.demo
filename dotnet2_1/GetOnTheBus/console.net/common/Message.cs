
namespace console.net.common
{
	public class Message<T>
		where T : class, new()
	{
		internal Message() { }
		internal Message(T message)
		{
			MessageBody = message;
			MessageId = string.Empty;
		}
		public string MessageId { internal set; get; }

		public T MessageBody { internal set; get; }

		internal string Serialize()
		{
			return SerializationHelper.SerializeObject(this);
		}

		internal static Message<T> Deserialize(string message, string messageId)
		{
			var returnValue = SerializationHelper.DeserializeObject<Message<T>>(message);
			returnValue.MessageId = messageId;
			return returnValue;
		}
	}
}
