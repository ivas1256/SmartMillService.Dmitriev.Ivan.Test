using System.IO;
using System.Text.Json;

namespace SmartMillService.Dmitriev.Ivan.Test.WpfApp
{
    public class CommentsService
    {
        private const string FileName = "env-comments.json";

        private Dictionary<string, string> _cache = new();

        public CommentsService()
        {
            LoadFile();
        }

        private void LoadFile()
        {
            if (!File.Exists(FileName))
            {
                _cache = new Dictionary<string, string>();
                SaveFile();
                return;
            }

            var json = File.ReadAllText(FileName);
            _cache = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }

        private void SaveFile()
        {
            var json = JsonSerializer.Serialize(_cache, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FileName, json);
        }

        public string Get(string name)
        {
            return _cache.TryGetValue(name, out var comment)
                ? comment
                : string.Empty;
        }

        public void Set(string name, string comment)
        {
            _cache[name] = comment ?? string.Empty;
            SaveFile();
        }
    }
}
