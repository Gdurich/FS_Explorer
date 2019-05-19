using FS_Explorer.VievModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FS_Explorer.ViewModels;
using System.IO;

namespace FS_Explorer
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new MainWindowViewModel(this);
        }
        private void TreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            TreeItemViewModel selectedItem = (TreeItemViewModel)treeView.SelectedItem;
            if (selectedItem == null) return;
            if(Directory.Exists(selectedItem.AddressItem))
                _viewModel.ReLoadTree(selectedItem);
        }
        private void TreeV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView treeView = (TreeView)sender;
            TreeItemViewModel selectedItem = (TreeItemViewModel)treeView.SelectedItem;
            _viewModel.ShowObjInfo(selectedItem);
        }
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for(int i = 0; i < TreeV.Items.Count; i++)
            {
                ((TreeViewItem)TreeV.ItemContainerGenerator.ContainerFromIndex(i)).IsExpanded = true;
            }
        }
    }
}
