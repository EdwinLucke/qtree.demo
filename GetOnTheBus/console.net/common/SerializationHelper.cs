using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace console.net.common
{
	internal static class SerializationHelper
	{
		internal static JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			ContractResolver = new DefaultContractResolver
			{
				//DefaultMembersSearchFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance
			}
		};

		internal static string SerializeObject(object target)
		{
			return JsonConvert.SerializeObject(target);
		}

		internal static T DeserializeObject<T>(string target)
			where T : class
		{
			return JsonConvert.DeserializeObject<T>(target, Settings);
		}
	}
}
