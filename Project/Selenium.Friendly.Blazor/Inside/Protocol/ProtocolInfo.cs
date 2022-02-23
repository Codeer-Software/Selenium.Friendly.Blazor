using Selenium.Friendly.Blazor.DotNetExecutor;
using Newtonsoft.Json;
using System;

namespace Selenium.Friendly.Blazor.Inside.Protocol
{
	/// <summary>
	/// 通信情報。
	/// </summary>
	[Serializable]
	public class ProtocolInfo
	{
        /// <summary>
        /// 通信タイプ。
        /// </summary>
        public ProtocolType ProtocolType { get; set; }

        /// <summary>
        /// 操作タイプ情報。
        /// </summary>
        public OperationTypeInfo OperationTypeInfo { get; set; }

		/// <summary>
		/// 変数アドレス。
		/// </summary>
		public VarAddress VarAddress { get; set; }

		static TypeFinder finder = new TypeFinder();

        internal void SetArgumentsFromJson()
        {
			Arguments = new object[ArgumentTypes.Length];
			for (int i = 0; i < Arguments.Length; i++)
			{
				if (ArgumentTypes[i] == null) continue;
				Arguments[i] = SerializeUtility.DeserializeObject(ArgumentJsons[i], finder.GetType(ArgumentTypes[i]));
			}

		}

        /// <summary>
        /// タイプフルネーム。
        /// </summary>
        public string TypeFullName { get; set; }

		/// <summary>
		/// 操作名称。
		/// </summary>
		public string Operation { get; set; }

		/// <summary>
		/// 引数。
		/// </summary>
		public object[] Arguments { get; set; }


		/// <summary>
		/// 引数。
		/// </summary>
		public string[] ArgumentTypes { get; set; }


		/// <summary>
		/// 引数。
		/// </summary>
		public string[] ArgumentJsons { get; set; }

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="protocolType">通信タイプ。</param>
		/// <param name="operationTypeInfo">操作タイプ情報。</param>
		/// <param name="varAddress">変数アドレス。</param>
		/// <param name="typeFullName">タイプフルネーム。</param>
		/// <param name="operation">操作名称。</param>
		/// <param name="arguments">引数。</param>
		public ProtocolInfo(ProtocolType protocolType, OperationTypeInfo operationTypeInfo, VarAddress varAddress, string typeFullName, string operation, object[] arguments)
		{
            ProtocolType = protocolType;
            OperationTypeInfo = operationTypeInfo;
            VarAddress = varAddress;
			TypeFullName = typeFullName;
			Operation = operation;
			Arguments = arguments;

			ArgumentTypes = arguments.Select(e => e == null ? null : e.GetType().FullName).ToArray();
			ArgumentJsons = arguments.Select(e => e == null ? null : SerializeUtility.SerializeObject(e)).ToArray();
		}
	}

	public class SerializeUtility
	{
		public static string SerializeObject(object obj)
		{
			if (obj == null) return string.Empty;
			var type = obj.GetType();
			if (type == typeof(string)) return obj.ToString();
			if (type == typeof(decimal)) return obj.ToString();
			if (type == typeof(double)) return obj.ToString();
			if (type == typeof(uint)) return obj.ToString();
			if (type == typeof(int)) return obj.ToString();
			if (type == typeof(long)) return obj.ToString();
			if (type == typeof(bool)) return obj.ToString();
			if (type == typeof(DateTime)) return obj.ToString();
			if (type == typeof(Guid) || type == typeof(Guid?)) return obj.ToString();
			if (type.IsEnum) return obj.ToString();

			if (obj.ToString() == "") { return string.Empty; }
			if (type == typeof(int?)) return IsNull(obj) ? string.Empty : ((int)obj).ToString();
			if (type == typeof(long?)) return IsNull(obj) ? string.Empty : ((long)obj).ToString();
			if (type == typeof(decimal?)) return IsNull(obj) ? string.Empty : ((decimal)obj).ToString();
			if (type == typeof(bool?)) return IsNull(obj) ? string.Empty : ((bool)obj).ToString();
			if (type == typeof(DateTime?)) return IsNull(obj) ? string.Empty : ((DateTime)obj).ToString();
			return JsonConvert.SerializeObject(obj);
		}

		static bool IsNull(object value)
		{
			if (value == null) return true;
			if (value.GetType() != typeof(string)) return false;
			return string.IsNullOrEmpty(value.ToString());
		}

		public static object DeserializeObject(string value, Type type)
		{
		
			if (value == null) return null;

		//	value = value.Substring(1, value.Length - 2);

			if (type == typeof(string)) return value.ToString();
			if (type == typeof(decimal)) return Convert.ToDecimal(value);
			if (type == typeof(double)) return Convert.ToDouble(value);
			if (type == typeof(uint)) return Convert.ToUInt32(value);
			if (type == typeof(int)) return Convert.ToInt32(value);
			if (type == typeof(long)) return Convert.ToInt64(value);
			if (type == typeof(bool)) return Convert.ToBoolean(value);
			if (type == typeof(DateTime)) return Convert.ToDateTime(value).ToUniversalTime();
			if (type == typeof(Guid) || type == typeof(Guid?)) return new Guid(value.ToString());
			if (type.IsEnum) return EnumUtility.ValidateEnum(type, Enum.ToObject(type, value));

			if (value.ToString() == "") { return null; }
			if (type == typeof(int?)) return IsNull(value) ? (int?)null : Convert.ToInt32(value);
			if (type == typeof(long?)) return IsNull(value) ? (long?)null : Convert.ToInt64(value);
			if (type == typeof(decimal?)) return IsNull(value) ? (decimal?)null : Convert.ToDecimal(value);
			if (type == typeof(bool?)) return IsNull(value) ? (bool?)null : Convert.ToBoolean(value);
			if (type == typeof(DateTime?)) return IsNull(value) ? (DateTime?)null : Convert.ToDateTime(value).ToUniversalTime();
			return JsonConvert.DeserializeObject(value.ToString(), type);
		}
	}

	public static class EnumUtility
	{
		public static object ValidateEnum(Type type, object value)
		{
			if (Enum.GetValues(type).Cast<object>().Contains(value)) return value;
			throw new NotSupportedException("xxx");
		}
		public static T ValidateEnum<T>(T value)
			=> (T)ValidateEnum(typeof(T), value);
	}
}