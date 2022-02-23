using Selenium.Friendly.Blazor.Inside.Protocol;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Selenium.Friendly.Blazor.Inside.CopyDataProtocol
{
	/// <summary>
	/// Windowメッセージ通信。
	/// </summary>
	static class CopyDataProtocolTalker
	{
		/// <summary>
		/// 送受信。
		/// </summary>
		/// <param name="targetWindowHandle">送信対象ウィンドウハンドル。</param>
		/// <param name="data">送信データ。</param>
		/// <returns>受信データ。</returns>
		internal static ReturnInfo SendAndRecieve(object wedbDriver, ProtocolInfo data)
		{
			var arg = JsonConvert.SerializeObject(data);

			Debug.WriteLine(arg);

			var src = ((dynamic)wedbDriver).ExecuteScript(@"
var arg = arguments[0];
return DotNet.invokeMethod(""Selenium.Friendly.Blazor"", ""ExecuteFriendly"", arg);
", arg);
			string aaa = src.ToString();
		//	Debug.WriteLine(aaa);

			//resultにはいってんのね
			var ret =  JsonConvert.DeserializeObject<ReturnInfo>(aaa);

			ret.SetReturnValueFromJson();

			return ret;
		}
		/// <summary>
		/// 送受信。
		/// </summary>
		/// <param name="targetWindowHandle">送信対象ウィンドウハンドル。</param>
		/// <param name="data">送信データ。</param>
		/// <returns>受信データ。</returns>
		internal static ReturnInfo SendAndRecieve(object wedbDriver, ContextOrderProtocolInfo data)
		{
			var arg = JsonConvert.SerializeObject(data);

			var src = ((dynamic)wedbDriver).ExecuteScript(@"
var arg = arguments[0];
return DotNet.invokeMethod(""Selenium.Friendly.Blazor"", ""ExecuteFriendly"", arg);
", arg);
			string aaa = src.ToString();
			Debug.WriteLine(aaa);

			//resultにはいってんのね
			return JsonConvert.DeserializeObject<ReturnInfo>(aaa);
		}
	}
}
