namespace SmartMillService.Dmitriev.Ivan.Test.Http
{
    public class HttpApiConfig
    {
        public string ApiUrl { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } // пароль не шифруем но в реальности будем
        public string BaseAddress { get; set; }
    }
}
