using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartMillService.Dmitriev.Ivan.Test.WpfApp
{
    /// <summary>
    /// по идее нужно завести отдельную модель для данных, отдельную для вьюхи 
    /// но для упрощения сделал все в одной
    /// </summary>
    public class EnvVariableItem : INotifyPropertyChanged
    {
        private string _value;
        private string _comment;

        public string Name { get; }

        public string Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment == value) return;
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public EnvVariableItem(string name, string value, string comment)
        {
            Name = name;
            _value = value;
            _comment = comment;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
