namespace SmartMillService.Dmitriev.Ivan.Test.Model
{
    public abstract class BaseResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
