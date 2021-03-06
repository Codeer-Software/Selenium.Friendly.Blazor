using Selenium.Friendly.Blazor;

namespace Selenium.Friendly.Blazor.Inside.Protocol
{
	/// <summary>
	/// 接続者。
	/// </summary>
	public interface IFriendlyConnector
	{
        /// <summary>
        /// 接続者を区別するためのユニークなオブジェクト。
        /// </summary>
        object Identity { get; }

		/// <summary>
		/// 送受信。
		/// </summary>
		/// <param name="info">通信情報。</param>
		/// <returns>戻り値。</returns>
		ReturnInfo SendAndReceive(ProtocolInfo info);

        /// <summary>
        /// アプリケーション操作クラスを取得します。
        /// </summary>
        AppFriend App { get; }
	}
}
