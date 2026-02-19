using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartMillService.Dmitriev.Ivan.Test.WpfApp
{
    public class MainViewModel
    {
        private readonly EnvironmentService _envService = new();

        public ObservableCollection<EnvVariableItem> Items { get; } = [];

        public MainViewModel()
        {
            var items = _envService.LoadAll();

            foreach(var item in items)
            {
                item.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == nameof(EnvVariableItem.Value))
                    {
                        _envService.Set(item.Name, item.Value);
                        Log.Information("Value changed {Name}={Value}", item.Name, item.Value);
                    }

                    if (e.PropertyName == nameof(EnvVariableItem.Comment))
                    {
                        _envService.SaveComment(item.Name, item.Comment);
                        Log.Information("Comment changed {Name}={Comment}", item.Name, item.Comment);
                    }
                };

                Items.Add(item);
            }
        }
    }
}
