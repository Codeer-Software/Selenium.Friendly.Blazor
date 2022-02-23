using System;

namespace Selenium.Friendly.Blazor.DotNetExecutor
{
    /// <summary>
    /// 非同期実行インターフェイス。
    /// </summary>
    public interface IAsyncInvoke
    {
        /// <summary>
        /// 非同期実行。
        /// </summary>
        /// <param name="method">実行メソッド。</param>
        void Execute(AsyncMethod method);
    }

    /// <summary>
    /// 非同期実行メソッド。
    /// </summary>
    public delegate void AsyncMethod();


    /// <summary>
    /// 非同期実行インターフェイス。
    /// </summary>
    public class AsyncInvoke : IAsyncInvoke
    {
        /// <summary>
        /// 非同期実行。
        /// </summary>
        /// <param name="method">実行メソッド。</param>
        public void Execute(AsyncMethod method) => method?.Invoke();
    }

}
