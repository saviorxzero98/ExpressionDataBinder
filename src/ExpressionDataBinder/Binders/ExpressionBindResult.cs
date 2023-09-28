using Newtonsoft.Json.Linq;

namespace ExpressionDataBinder.Binders
{
    public class ExpressionBindResult
    {
        public ExpressionBindResult()
        {

        }
        public ExpressionBindResult(JToken value, bool hasForcedBind = false)
        {
            IsSuccess = (value != null);
            HasForcedBind = hasForcedBind;
            Value = value;
            ValueType = (value == null) ? JTokenType.Null : value.Type;
        }
        public ExpressionBindResult(bool isSuccess, JToken value, bool hasForcedBind = false)
        {
            IsSuccess = isSuccess;
            HasForcedBind = hasForcedBind;
            Value = value;
            ValueType = (value == null) ? JTokenType.Null : value.Type;
        }
        public ExpressionBindResult(Exception e, bool hasForcedBind = false)
        {
            IsSuccess = false;
            HasForcedBind = hasForcedBind;
            Value = default(JToken);
            ValueType = JTokenType.Null;
            Error = e;
        }
        public ExpressionBindResult(Exception e, JToken value, bool hasForcedBind = false)
        {
            IsSuccess = false;
            HasForcedBind = hasForcedBind;
            Error = e;
            Value = value;
            ValueType = (value == null) ? JTokenType.Null : value.Type;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 是否已經被強制處理
        /// </summary>
        public bool HasForcedBind { get; set; }

        /// <summary>
        /// 錯誤
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// 輸出值
        /// </summary>
        public JToken Value { get; set; }

        /// <summary>
        /// 輸出值型態
        /// </summary>
        public JTokenType ValueType { get; set; }
    }
}
