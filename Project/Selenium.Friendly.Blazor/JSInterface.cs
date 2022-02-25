using Selenium.Friendly.Blazor.DotNetExecutor;
using Microsoft.JSInterop;

namespace Selenium.Friendly.Blazor
{
    public static class JSInterface
    {
        static DotNetFriendlyControl _ctrl = new DotNetFriendlyControl();

        [JSInvokable]
        public static string ExecuteFriendly(string x)
            => _ctrl.Execute(x);
    }
}
