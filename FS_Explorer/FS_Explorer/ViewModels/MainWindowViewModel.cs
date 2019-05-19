using FS_Explorer.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.IO;
using FS_Explorer.ViewModels;

namespace FS_Explorer.VievModels
{
    class MainWindowViewModel: Base.ViewModel
    {
        public ObservableCollection<DriveModel> DriveItems { get; set; }
        DriveModel driveItem;
        public DriveModel DriveItem {
            get
            {
                return driveItem;
            }
            set
            {
                VisibilityInfo = Visibility.Hidden;
                OnPropertyChanged(nameof(VisibilityInfo));
                driveItem = value;
                OnPropertyChanged(nameof(DriveItem));
                TreeLoad(value.Title);
            }
        }
        public ObservableCollection<TreeItemViewModel> TreeItems { get; set; }
        public ItemContentControl contentControl { get; set; }
        public string imgSource { get; set; }
        public Visibility VisibilityInfo { get; set; } = Visibility.Hidden;
        public MainWindowViewModel(Window window)
        {
            DriveItems = new ObservableCollection<DriveModel>();
            TreeItems = new ObservableCollection<TreeItemViewModel>();
            GetDrivers();
        }
        void GetDrivers()
        {
            foreach (var item in DriveInfo.GetDrives())
            {
                DriveItems.Add(new DriveModel { Title = item.Name });
            }
        }
        void TreeLoad(string drive)
        {
            TreeItems.Clear();
            try
            {
                foreach (var item in Directory.GetDirectories(drive))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(item);
                    TreeItems.Add(new TreeItemViewModel(dirInfo.Name, dirInfo.FullName, 1));
                }
                foreach (var item in Directory.GetFiles(drive))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    TreeItems.Add(new TreeItemViewModel(fileInfo.Name, fileInfo.FullName, 1));
                }
            }catch(Exception ex){ MessageBox.Show(ex.Message); }
        }
        public void ReLoadTree(TreeItemViewModel treeItem)
        {
            string Title = treeItem.AddressItem.Split('\\')[1];
            foreach (var Item in TreeItems)
            {
                if(Item.Title == Title)
                {
                    Item.UbdateTreeItem(treeItem);
                    return;
                }
            }
        }
        public void ShowObjInfo(TreeItemViewModel treeItem)
        {
            try
            {
                imgSource = null;
                if (Directory.Exists(treeItem.AddressItem))
                {
                    contentControl = new ItemContentControl();
                    DirectoryInfo directoryInfo = new DirectoryInfo(treeItem.AddressItem);
                    contentControl.Title = directoryInfo.Name;
                    contentControl.FullAdress = directoryInfo.FullName;
                    contentControl.DateOfCreation = directoryInfo.CreationTime;
                    contentControl.DateOfChange = directoryInfo.LastWriteTime;
                   // contentControl.Size = directoryInfo.
                    contentControl.ObjectCount += directoryInfo.GetDirectories().Count();
                    contentControl.ObjectCount += directoryInfo.GetFiles().Count();
                } else if (File.Exists(treeItem.AddressItem))
                {
                    contentControl = new ItemContentControl();
                    FileInfo fileInfo = new FileInfo(treeItem.AddressItem);
                    contentControl.Title = fileInfo.Name;
                    contentControl.FullAdress = fileInfo.FullName;
                    contentControl.DateOfCreation = fileInfo.CreationTime;
                    contentControl.DateOfChange = fileInfo.LastWriteTime;
                    contentControl.Size = fileInfo.Length + " б/ "
                        + (double)fileInfo.Length/1024 + " Кб/ "
                        + (double)fileInfo.Length/1024/1024 + " Mб/ "
                        + (double)fileInfo.Length/1024/1024/1024 + " Гб";
                    string FileFormat = treeItem.Title.Split('.')[treeItem.Title.Split('.').Length - 1].ToLower();
                    if (FileFormat == "png" || FileFormat == "jpeg"|| FileFormat == "bmp" || FileFormat == "jpg")
                    {
                        imgSource = treeItem.AddressItem;
                    }
                }
                OnPropertyChanged(nameof(imgSource));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            VisibilityInfo = Visibility.Visible;
            OnPropertyChanged(nameof(VisibilityInfo));
            OnPropertyChanged(nameof(contentControl));
        }
    }
}
