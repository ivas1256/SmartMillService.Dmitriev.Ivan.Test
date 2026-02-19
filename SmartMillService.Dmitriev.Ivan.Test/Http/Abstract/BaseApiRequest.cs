namespace SmartMillService.Dmitriev.Ivan.Test.Http.Abstract
{
    public abstract class BaseApiRequest<T> where T : class
    {
        public virtual string Command { get; }
        public T CommandParameters { get; set; }
    }
}
