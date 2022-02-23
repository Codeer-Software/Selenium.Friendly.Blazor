using System;
using Friendly.Blazor.Properties;

namespace Friendly.Blazor.Inside
{
    /// <summary>
    /// ローカライズ済みリソース。
    /// </summary>
    [Serializable]
    class ResourcesLocal
    {
        static internal ResourcesLocal Instance;

        internal string ErrorAppCommunication;
        internal string ErrorAppConnection;
        internal string ErrorArgumentInvokeFormat;
        internal string ErrorArgumentInvokeFormatForObjectArray;
        internal string ErrorBinaryInstall;
        internal string ErrorDllLoad;
        internal string ErrorExecuteThreadWindowHandle;
        internal string ErrorFriendlySystem;
        internal string ErrorInvalidThreadCall;
        internal string ErrorManyFoundConstractorFormat;
        internal string ErrorManyFoundInvokeFormat;
        internal string ErrorNotFoundConstractorFormat;
        internal string ErrorNotFoundConstractorFormatForObjectArray;
        internal string ErrorNotFoundInvokeFormat;
        internal string ErrorOperationTypeArgInfoFormat;
        internal string ErrorOperationTypeArgInfoForObjectArrayFormat;
        internal string ErrorProcessAcess;
        internal string ErrorProcessOperation;
        internal string ErrorTargetCpuDifference;
        internal string ErrorUnpredicatableClrVersion;
        internal string HasNotEnumerable;
        internal string NullObjectOperation;
        internal string OutOfCommunicationNo;
        internal string OutOfMemory;
        internal string UnknownTypeInfoFormat;
        internal string ErrorAttachOtherDomainsNeedNet4;

        /// <summary>
        /// 初期化。
        /// </summary>
        internal static void Initialize()
        {
            Instance = new ResourcesLocal();
            Instance.InitializeCore();
        }

        /// <summary>
        /// 対象に文字列インストール
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        internal static void Install(BlazorAppFriend app)
        {
            app[typeof(ResourcesLocal), "Instance"](Instance);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        void InitializeCore()
        {
            ErrorAppCommunication = Resources.ErrorAppCommunication;
            ErrorAppConnection = Resources.ErrorAppConnection;
            ErrorArgumentInvokeFormat = Resources.ErrorArgumentInvokeFormat;
            ErrorArgumentInvokeFormatForObjectArray = Resources.ErrorArgumentInvokeFormatForObjectArray;
            ErrorBinaryInstall = Resources.ErrorBinaryInstall;
            ErrorDllLoad = Resources.ErrorDllLoad;
            ErrorExecuteThreadWindowHandle = Resources.ErrorExecuteThreadWindowHandle;
            ErrorFriendlySystem = Resources.ErrorFriendlySystem;
            ErrorInvalidThreadCall = Resources.ErrorInvalidThreadCall;
            ErrorManyFoundConstractorFormat = Resources.ErrorManyFoundConstractorFormat;
            ErrorManyFoundInvokeFormat = Resources.ErrorManyFoundInvokeFormat;
            ErrorNotFoundConstractorFormat = Resources.ErrorNotFoundConstractorFormat;
            ErrorNotFoundConstractorFormatForObjectArray = Resources.ErrorNotFoundConstractorFormatForObjectArray;
            ErrorNotFoundInvokeFormat = Resources.ErrorNotFoundInvokeFormat;
            ErrorOperationTypeArgInfoFormat = Resources.ErrorOperationTypeArgInfoFormat;
            ErrorOperationTypeArgInfoForObjectArrayFormat = Resources.ErrorOperationTypeArgInfoForObjectArrayFormat;
            ErrorProcessAcess = Resources.ErrorProcessAcess;
            ErrorProcessOperation = Resources.ErrorProcessOperation;
            ErrorTargetCpuDifference = Resources.ErrorTargetCpuDifference;
            ErrorUnpredicatableClrVersion = Resources.ErrorUnpredicatableClrVersion;
            HasNotEnumerable = Resources.HasNotEnumerable;
            NullObjectOperation = Resources.NullObjectOperation;
            OutOfCommunicationNo = Resources.OutOfCommunicationNo;
            OutOfMemory = Resources.OutOfMemory;
            UnknownTypeInfoFormat = Resources.UnknownTypeInfoFormat;
            ErrorAttachOtherDomainsNeedNet4 = Resources.ErrorAttachOtherDomainsNeedNet4;
        }
    }
}
