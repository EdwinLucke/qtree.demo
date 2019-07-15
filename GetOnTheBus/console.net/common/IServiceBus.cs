using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console.net.common
{
	public interface IServiceBus
	{
		void SendMessage<T>(T message)
			where T : class, new();

		bool ReceiveMessage<T>(Software software, Func<Message<T>, bool> processMessageFunction)
			where T : class, new();
	}

	public class Software
	{
		public string Name = "console";
	}
}
