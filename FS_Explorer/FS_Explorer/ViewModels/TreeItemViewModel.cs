using FS_Explorer.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace FS_Explorer.ViewModels
{
    public class TreeItemViewModel : VievModels.Base.ViewModel
    {
        public string Title { get; set; }
        public ImageSource Ico { get; set; }
        public string AddressItem { get; set; }
        public int Layer { get; set; }
        ObservableCollection<TreeItemViewModel> treeItems;
        public ObservableCollection<TreeItemViewModel> TreeItems
        {
            get
            {
                return treeItems;
            }
            set
            {
                treeItems = value;
                OnPropertyChanged(nameof(TreeItems));
            }
        }
        public ObservableCollection<ContextMenuItemsViewModel> ContextItems { get; set; }
        public TreeItemViewModel(string Title, string AddressItem, int Layer)
        {
            this.Title = Title;
            this.AddressItem = AddressItem;
            this.Layer = Layer;
            try
            {
                Ico = Icons.GetIcon(AddressItem);
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
            treeItems = new ObservableCollection<TreeItemViewModel>();
            ContextItems = new ObservableCollection<ContextMenuItemsViewModel>() {
                new ContextMenuItemsViewModel()
                {
                    TextItem = "Открыть",
                    ContextCommandItem = new Command(() =>
                    {
                        Process.Start(AddressItem);
                    })
                } };
        }
        public void UbdateTreeItem(TreeItemViewModel Item)
        {
            if (Layer == Item.Layer)
            {
                ObservableCollection<TreeItemViewModel> TmpList = new ObservableCollection<TreeItemViewModel>();
                try
                {
                    foreach (var item in Directory.GetDirectories(Item.AddressItem))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(item);
                        TmpList.Add(new TreeItemViewModel(dirInfo.Name, dirInfo.FullName, Layer + 1));
                    }
                    foreach (var item in Directory.GetFiles(Item.AddressItem))
                    {
                        FileInfo fileInfo = new FileInfo(item);
                        TmpList.Add(new TreeItemViewModel(fileInfo.Name, fileInfo.FullName, Layer + 1));
                    }
                }
                catch (Exception ex){ MessageBox.Show(ex.Message); }
                TreeItems = TmpList;
            }
            else
            {
                string title = Item.AddressItem.Split('\\')[Layer + 1];
                foreach (var item in TreeItems)
                {
                    if (title == item.Title)
                    {
                        item.UbdateTreeItem(Item);
                        return;
                    }
                }
            }
        }
    }
}
