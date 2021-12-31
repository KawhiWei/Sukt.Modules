namespace Sukt.AspNetCore.ApiResults
{
    /// <summary>
    /// 统一返回数据格式
    /// </summary>
    public class AjaxResult
    {

        public AjaxResult(object? data = null)
        {
            Data = data;
            Success = true;
        }

        public AjaxResult(string message, object? data = null)
        {
            Message = message;
            Success = true;
            Data = data;
        }
        public AjaxResult(string message, bool success, object? data)
        {
            Message = message;
            Data = data;
            Success = success;
        }

        /// <summary>
        /// 消息《如果有错误的时候Message才不为空否则默认是空》
        /// </summary>

        public string? Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>

        public object? Data { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>

        public bool Success { get; set; }
    }
}