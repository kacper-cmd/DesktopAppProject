using MusicApp.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.ViewModels
{
    public class BaseDBViewModel : WorkspaceViewModel
    {
        public DatabaseContext Database { get; set; }
        public BaseDBViewModel(string displayName) : base(displayName)
        {
            Database = new();
        }
    }
}
