using MusicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Helpers
{
    public class OpenViewMessage
    {
        public WorkspaceViewModel WorkspaceViewModel { get; set; }

        public OpenViewMessage(WorkspaceViewModel workspaceViewModel)
        {
            WorkspaceViewModel = workspaceViewModel;
        }
    }
}
