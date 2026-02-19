namespace SmartMillService.Dmitriev.Ivan.Test.WpfApp
{
    public class EnvironmentService
    {
        private readonly EnvironmentVariableTarget _target = EnvironmentVariableTarget.User;
        private readonly ConfigurationService _configService = new();
        private readonly CommentsService _commentsService = new();

        public void Set(string name, string value)
        {
            Environment.SetEnvironmentVariable(name, value, _target);
        }

        public IEnumerable<EnvVariableItem> LoadAll()
        {
            var names = _configService.GetVariableNames();

            foreach(var name in names)
            {
                var value = Environment.GetEnvironmentVariable(name, _target) ?? string.Empty;
                if (string.IsNullOrEmpty(value))
                    Set(name, string.Empty);

                var comment = _commentsService.Get(name);

                yield return new EnvVariableItem(name, value, comment);
            }
        }

        public void SaveComment(string name, string comment)
        {
            _commentsService.Set(name, comment);
        }
    }
}
