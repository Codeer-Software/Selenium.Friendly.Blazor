#define CODE_ANALYSIS
using System;
using System.ComponentModel;
using Selenium.Friendly.Blazor.Inside.Protocol;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Selenium.Friendly.Blazor.DotNetExecutor
{
	/// <summary>
	/// .NetのFriendly処理制御。
	/// </summary>
	public class DotNetFriendlyControl
	{
		VarPool _pool = new VarPool();
		TypeFinder _typeFinder = new TypeFinder();

        public string Execute(string infoText)
        {
           var info = JsonConvert.DeserializeObject<ProtocolInfo>(infoText);

            info.SetArgumentsFromJson();
            var ret = Execute(new AsyncInvoke(), info);
            return JsonConvert.SerializeObject(ret);
          //  return new string(text.Reverse().ToArray());
        }

        ReturnInfo Execute(IAsyncInvoke async, ProtocolInfo info)
		{
            try
            {
                return DotNetFriendlyExecutor.Execute(async, _pool, _typeFinder, info);
            }
            catch (Exception e)
            {
                return new ReturnInfo(new ExceptionInfo(e));
            }
		}
    }
}
