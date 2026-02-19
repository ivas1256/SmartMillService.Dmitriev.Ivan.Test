namespace SmartMillService.Dmitriev.Ivan.Test.Http.Abstract
{
    public abstract class BaseApiResponse<T> where T: class
    {
        public string Command { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
