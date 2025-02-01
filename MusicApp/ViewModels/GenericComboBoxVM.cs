using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.ViewModels
{
    public class GenericComboBoxVM<KeyType>
    {
        /// <summary>
        /// Display to the user
        /// </summary>
        public string Value { get; set; }
        public KeyType Key { get; set; }

        public GenericComboBoxVM(string value, KeyType key)
        {
            Value = value;
            Key = key;
        }
    }
}
