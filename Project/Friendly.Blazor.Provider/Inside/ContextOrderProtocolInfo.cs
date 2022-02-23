using Friendly.Blazor.Inside.Protocol;

namespace Friendly.Blazor.Inside
{
    /// <summary>
    /// 実行コンテキスト指定プロトコル情報
    /// </summary>
    [Serializable]
    class ContextOrderProtocolInfo
    {
        ProtocolInfo _protocolInfo;

        /// <summary>
        /// プロトコル情報
        /// </summary>
        internal ProtocolInfo ProtocolInfo { get { return _protocolInfo; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="protocolInfo">プロトコル情報</param>
        /// <param name="executeWindowHandle">実行ウィンドウハンドル</param>
        internal ContextOrderProtocolInfo(ProtocolInfo protocolInfo)
        {
            _protocolInfo = protocolInfo;
        }
    }
}
