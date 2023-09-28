namespace ExpressionDataBinder.Binders
{
    /// <summary>
    /// 資料綁定處理
    /// </summary>
    public interface IExptessionBinder
    {
        ExpressionBindResult GetDataByExpression(string expression, object data = null);

        ExpressionBindResult BindData(string text, object data = null);
    }
}
