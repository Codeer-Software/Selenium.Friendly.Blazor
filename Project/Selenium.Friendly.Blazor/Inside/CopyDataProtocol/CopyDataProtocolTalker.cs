using Selenium.Friendly.Blazor.Inside.Protocol;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Selenium.Friendly.Blazor.Inside.CopyDataProtocol
{
	static class CopyDataProtocolTalker
	{
		internal static ReturnInfo SendAndRecieve(object wedbDriver, ProtocolInfo data)
		{
			var arg = JsonConvert.SerializeObject(data);

			var src = ((dynamic)wedbDriver).ExecuteScript(@"
var arg = arguments[0];
return DotNet.invokeMethod(""Selenium.Friendly.Blazor"", ""ExecuteFriendly"", arg);
", arg);
			var ret =  JsonConvert.DeserializeObject<ReturnInfo>((string)src);
			ret.SetReturnValueFromJson();
			return ret;
		}

		internal static ReturnInfo SendAndRecieve(object wedbDriver, ContextOrderProtocolInfo data)
		{
			var arg = JsonConvert.SerializeObject(data);

			var src = ((dynamic)wedbDriver).ExecuteScript(@"
var arg = arguments[0];
return DotNet.invokeMethod(""Selenium.Friendly.Blazor"", ""ExecuteFriendly"", arg);
", arg);
			var ret = JsonConvert.DeserializeObject<ReturnInfo>((string)src);
			ret.SetReturnValueFromJson();
			return ret;
		}
	}
}
