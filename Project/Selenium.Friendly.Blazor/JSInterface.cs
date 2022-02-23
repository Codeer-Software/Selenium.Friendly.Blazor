using Selenium.Friendly.Blazor.DotNetExecutor;
using Microsoft.JSInterop;

namespace Selenium.Friendly.Blazor
{
    public static class JSInterface
    {
        static DotNetFriendlyControl _ctrl = new DotNetFriendlyControl();

        internal static bool FriendlyAccessEnabled { get; set; }

        [JSInvokable]
        public static string ExecuteFriendly(string x)
        {
            if (!FriendlyAccessEnabled) throw new NotSupportedException();
            return _ctrl.Execute(x);
        }
    }
}
