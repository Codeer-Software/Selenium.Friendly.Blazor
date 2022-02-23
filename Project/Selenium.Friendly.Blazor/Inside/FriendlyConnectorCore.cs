using Selenium.Friendly.Blazor.Inside.CopyDataProtocol;
using Selenium.Friendly.Blazor.Inside.Protocol;

namespace Selenium.Friendly.Blazor.Inside
{
    class FriendlyConnectorCore
    {
        object _wedbDriver;

        public FriendlyConnectorCore(object webDriver)
            => _wedbDriver = webDriver;

        public ReturnInfo SendAndReceive(ProtocolInfo info)
        {
            return SendAndReceiveCore(info);
        }
        
        private ReturnInfo SendAndReceiveCore(ProtocolInfo info)
        {
            switch (info.ProtocolType)
            {
                case ProtocolType.IsEmptyVar:
                case ProtocolType.AsyncResultVarInitialize:
                    return AsyncState(info);
                case ProtocolType.AsyncOperation:
                    return AsyncOperation(info);
                case ProtocolType.Operation:
                    return Operation(info);
                case ProtocolType.BinOff:
                    return BinOff(info);
                default:
                    return SendForExecuteContext(info);
            }
        }

        private ReturnInfo AsyncState(ProtocolInfo info)
        {
            ReturnInfo ret = CopyDataProtocolTalker.SendAndRecieve(_wedbDriver, info) as ReturnInfo;
            if (ret == null)
            {
                throw new FriendlyOperationException(ResourcesLocal.Instance.ErrorAppCommunication);
            }
            return ret;
        }

        private ReturnInfo AsyncOperation(ProtocolInfo info)
        {
            ContextOrderProtocolInfo contextOrder = new ContextOrderProtocolInfo(info);
            ReturnInfo ret = CopyDataProtocolTalker.SendAndRecieve(_wedbDriver, contextOrder) as ReturnInfo;
            if (ret == null)
            {
                throw new FriendlyOperationException(ResourcesLocal.Instance.ErrorAppCommunication);
            }
            return ret;
        }

        /// <summary>
        /// 同期実行。
        /// しかし、Windowsの場合、操作実行は非同期で実行しないと、稀にに操作中にSendMessageが失敗してしまう操作がある。
        /// そのため、非同期操作のプロトコルを使って、実行させ、終了するのを待つ。
        /// </summary>
        /// <param name="info">呼び出し情報。</param>
        /// <param name="receiveWindow">受信ウィンドウ。</param>
        /// <returns>戻り値。</returns>
        private ReturnInfo Operation(ProtocolInfo info)
        {
            //完了の成否確認用
            ReturnInfo isComplete = SendForExecuteContext(new ProtocolInfo(ProtocolType.VarInitialize, null, null, string.Empty, string.Empty, new object[] { null }));
            if (isComplete.Exception != null)
            {
                return isComplete;
            }

            //引数の先頭に存在確認フラグを挿入
            List<object> arg = new List<object>();
            arg.Add(isComplete.ReturnValue);
            arg.AddRange(info.Arguments);

            //非同期実行
            ReturnInfo retValue = SendForExecuteContext(new ProtocolInfo(ProtocolType.AsyncOperation, info.OperationTypeInfo, info.VarAddress, info.TypeFullName, info.Operation, arg.ToArray()));
            if (retValue.Exception != null)
            {
                return retValue;
            }

            //処理が完了するのを待つ
            VarAddress complateCheckHandle = (VarAddress)isComplete.ReturnValue;
            int sleepTime = 1;
            while (true)
            {
                //結果の確認は実行対象スレッド以外で実施する。
                //処理が完了するまで、そのスレッドには割り込まない。
                ReturnInfo ret = CopyDataProtocolTalker.SendAndRecieve(_wedbDriver,
                    new ProtocolInfo(ProtocolType.IsEmptyVar, null, null, string.Empty, string.Empty, new object[] { complateCheckHandle })
                    ) as ReturnInfo;
                if (ret == null)
                {
                    throw new FriendlyOperationException(ResourcesLocal.Instance.ErrorAppCommunication);
                }
                if (!(bool)ret.ReturnValue)
                {
                    break;
                }
                Thread.Sleep(sleepTime);
                sleepTime++;
                if (100 < sleepTime)
                {
                    sleepTime = 100;
                }
            }

            //結果を取得
            ReturnInfo checkComplate = SendForExecuteContext(new ProtocolInfo(ProtocolType.GetValue, null, complateCheckHandle, string.Empty, string.Empty, new object[0]));
            if (checkComplate.Exception != null)
            {
                return checkComplate;
            }
            ReturnInfo checkComplateCore = checkComplate.ReturnValue as ReturnInfo;
            if (checkComplateCore == null)
            {
                throw new FriendlyOperationException(ResourcesLocal.Instance.ErrorAppCommunication);
            }
            if (checkComplateCore.Exception != null)
            {
                return checkComplateCore;
            }

            //解放
            //まあBINOFFのJS公開関数呼び出しとかかな
            //NativeMethods.SendMessage(_friendlyConnectorWindowInAppHandle, FriendlyConnectorWindowInApp.WM_BINOFF, new IntPtr(complateCheckHandle.Core), IntPtr.Zero);

            //戻り値を返す
            return retValue;
        }

        /// <summary>
        /// BinOffはGCのスレッドからコールされるので、SendMessageのみで通信する（受信しない）
        /// </summary>
        /// <param name="info">呼び出し情報。</param>
        /// <returns>戻り値。</returns>
        private ReturnInfo BinOff(ProtocolInfo info)
        {
            //まあBINOFFのJS公開関数呼び出しとかかな
            //NativeMethods.SendMessage(_friendlyConnectorWindowInAppHandle, FriendlyConnectorWindowInApp.WM_BINOFF, new IntPtr(info.VarAddress.Core), IntPtr.Zero);
            return new ReturnInfo();
        }

        /// <summary>
        /// 実行スレッドに送信。
        /// </summary>
        /// <param name="info">呼び出し情報。</param>
        /// <param name="receiveWindow">受信ウィンドウ。</param>
        /// <returns>戻り値。</returns>
        ReturnInfo SendForExecuteContext(ProtocolInfo info)
        {
            ReturnInfo ret = CopyDataProtocolTalker.SendAndRecieve(_wedbDriver, info) as ReturnInfo;
            if (ret == null)
            {
                throw new FriendlyOperationException(ResourcesLocal.Instance.ErrorAppCommunication);
            }
            return ret;
        }
    }
}
