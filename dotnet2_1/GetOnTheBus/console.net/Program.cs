using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using System.Configuration;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using console.net.common;

namespace console.net
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("O yeah migo");
			Console.ReadLine();
			
		}
	}

}

namespace messages
{
	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}

namespace common
{		
	/// <summary>
	/// Service bus implementation
	/// </summary>
	//public class ServiceBus : IDisposable, IServiceBus
	//{
	//	private readonly MessagingFactory _messageFactory;
	//	private readonly NamespaceManager _namespaceManager;
	//	private readonly ServiceBusConfiguration _serviceBusConfiguration;
	//	private readonly ILogger _logger;


	//	/// <summary>
	//	/// Initializes a new instance of the <see cref="ServiceBus"/> class.
	//	/// </summary>
	//	/// <param name="logger">The logger.</param>
	//	/// <exception cref="System.Configuration.ConfigurationErrorsException">
	//	/// Could not successfully create namespace manager from connection string
	//	/// </exception>
	//	public ServiceBus(ILogger logger)
	//	{
	//		try
	//		{
	//			_logger = logger;
	//			_messageFactory = MessagingFactory.CreateFromConnectionString(
	//				ConfigurationManager.ConnectionStrings["serviceBus"].ConnectionString);
	//			_namespaceManager = NamespaceManager.CreateFromConnectionString(
	//				ConfigurationManager.ConnectionStrings["serviceBus"].ConnectionString);
	//			_serviceBusConfiguration = ServiceBusConfiguration.Create();
	//		}
	//		catch (Exception exception)
	//		{
	//			_logger.LogException(exception);
	//			throw;
	//		}

	//		if (_namespaceManager == null)
	//		{
	//			throw new ConfigurationErrorsException(
	//				"Could not successfully create namespace manager from connection string");
	//		}
	//	}

	//	/// <summary>
	//	/// Sends the message.
	//	/// </summary>
	//	/// <typeparam name="T">Type of message to send</typeparam>
	//	/// <param name="message">The message.</param>
	//	public void SendMessage<T>(T message)
	//	   where T : class, new()
	//	{
	//		foreach (var software in _serviceBusConfiguration.Mappings[typeof(T)])
	//		{
	//			var queueName = GetQueueName<T>(software);
	//			CreateQueueIfDoesNotExist(queueName);
	//			QueueClient queueClient = _messageFactory.CreateQueueClient(queueName);
	//			try
	//			{
	//				var stringMessage = new Message<T>(message).Serialize();
	//				var sendMessage = new BrokeredMessage(stringMessage);
	//				queueClient.Send(sendMessage);
	//			}
	//			catch (Exception exception)
	//			{
	//				queueClient.Close();
	//				_logger.LogException(exception);
	//				throw;
	//			}
	//		}

	//	}

	//	/// <summary>
	//	/// Receives the message.
	//	/// </summary>
	//	/// <typeparam name="T">Type of message to receive</typeparam>
	//	/// <param name="software">The software that gets the message.</param>
	//	/// <param name="processMessageFunction">The process message function.</param>
	//	/// <returns></returns>
	//	public bool ReceiveMessage<T>(Software software, Func<Message<T>, bool> processMessageFunction)
	//	   where T : class, new()
	//	{
	//		var queueName = GetQueueName<T>(software);
	//		bool received = false;
	//		CreateQueueIfDoesNotExist(queueName);
	//		QueueClient queueClient = _messageFactory.CreateQueueClient(queueName, ReceiveMode.PeekLock);

	//		try
	//		{
	//			var message = queueClient.Receive(TimeSpan.FromMilliseconds(100));
	//			while (message != null && !received)
	//			{
	//				var stringMessage = message.GetBody<string>();
	//				Message<T> sampleMessage = Message<T>.Deserialize(stringMessage, message.MessageId);
	//				if (sampleMessage != null)
	//				{
	//					try
	//					{
	//						var result = processMessageFunction(sampleMessage);
	//						if (result)
	//						{
	//							try
	//							{
	//								message.Complete();
	//							}
	//							catch (Exception exception)
	//							{
	//								_logger.LogMessage(
	//									LogLevel.Warning, "Error abandoning message: " + exception);
	//							}

	//						}
	//						else
	//						{
	//							try
	//							{
	//								message.Abandon();
	//							}
	//							catch (Exception exception)
	//							{
	//								_logger.LogMessage(
	//									LogLevel.Warning, "Error abandoning message: " + exception);
	//							}
	//						}
	//						received = true;
	//					}
	//					catch (Exception exception)
	//					{
	//						message.Abandon();
	//						Console.WriteLine(exception.ToString());
	//					}
	//				}
	//				else
	//				{
	//					message.Abandon();
	//				}
	//				message = queueClient.Receive(TimeSpan.FromMilliseconds(100));
	//			}
	//		}
	//		catch (Exception exception)
	//		{
	//			_logger.LogException(exception);
	//			queueClient.Close();
	//			throw;
	//		}
	//		return received;
	//	}

	//	/// <summary>
	//	/// Gets the name of the queue based on software type and message type.
	//	/// </summary>
	//	/// <typeparam name="T">Type of message</typeparam>
	//	/// <param name="software">The software to process 
	//	/// (send ore receive the message).</param>
	//	/// <returns></returns>
	//	private static string GetQueueName<T>(Software software) where T : class
	//	{
	//		string queueName = string.Format("{0}-{1}",
	//			typeof(T).FullName.ToLower(),
	//			software.Name.ToLower());
	//		return queueName;
	//	}


	//	/// <summary>
	//	/// Creates the queue if does not exist.
	//	/// </summary>
	//	/// <param name="queueName">Name of the queue.</param>
	//	private void CreateQueueIfDoesNotExist(string queueName)
	//	{
	//		if (!_namespaceManager.QueueExists(queueName))
	//		{
	//			_namespaceManager.CreateQueue(queueName);
	//		}
	//	}

	//	/// <summary>
	//	/// Performs application-defined tasks associated 
	//	/// with freeing, releasing, or resetting unmanaged resources.
	//	/// </summary>
	//	public void Dispose()
	//	{
	//		if (_messageFactory != null)
	//		{
	//			_messageFactory.Close();
	//		}
	//	}
	//}
}
