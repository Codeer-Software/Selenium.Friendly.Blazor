using Friendly.Blazor.DotNetExecutor;
using Newtonsoft.Json;
using System;

namespace Friendly.Blazor.Inside.Protocol
{
	/// <summary>
	/// 戻り値情報。
	/// </summary>
	[Serializable]
	public class ReturnInfo
	{

		public string ReturnValueTypeFullName { get; set; }

		public string ReturnValueJsonText { get; set; }

		/// <summary>
		/// 戻り値。
		/// </summary>
		public object ReturnValue { get; set; }

		/// <summary>
		/// 例外。
		/// </summary>
		public ExceptionInfo Exception { get; set; }

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public ReturnInfo() { }

		static TypeFinder finder = new TypeFinder();

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="returnValue">戻り値。</param>
		public ReturnInfo(object returnValue)
		{
			ReturnValue = returnValue;
			ReturnValueTypeFullName = returnValue == null ? null : returnValue.GetType().FullName;
			ReturnValueJsonText = returnValue == null ? null : SerializeUtility.SerializeObject(returnValue);
		}

		public void SetReturnValueFromJson()
        {
			if (ReturnValueTypeFullName == null) return;
			ReturnValue = SerializeUtility.DeserializeObject(ReturnValueJsonText, finder.GetType(ReturnValueTypeFullName));
		}

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="exception">例外情報。</param>
        public ReturnInfo(ExceptionInfo exception)
		{
            Exception = exception;
		}
	}
}
