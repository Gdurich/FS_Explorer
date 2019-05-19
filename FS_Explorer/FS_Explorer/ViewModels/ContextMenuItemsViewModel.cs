using System.Windows.Input;
using FS_Explorer.VievModels.Base;

namespace FS_Explorer.ViewModels
{
    public class ContextMenuItemsViewModel : ViewModel
    {
        public string TextItem { get; set; }
        public ICommand ContextCommandItem { get; set; }
    }
}
