using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FS_Explorer.Helpers
{
    public class Icons
    {
        private const int SHGFI_ICON = 0x100;
        private const int SHGFI_SMALLICON = 0x1;
        private const int SHGFI_LARGEICON = 0x0;
        struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        [DllImport("Shell32.dll")]
        static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);
        static public ImageSource GetIcon(string Path)
        {
            IntPtr hImgSmall;
            SHFILEINFO shinfo = new SHFILEINFO();
            hImgSmall = SHGetFileInfo(Path, 0, ref shinfo, Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
            System.Drawing.Icon myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(myIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return imageSource;
        }
    }
}
