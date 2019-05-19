using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FS_Explorer.Helpers
{
    class Icon
    {
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                IntPtr.Zero, Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }

        public ImageSource IconFromFile(string fileName)
        {
            var icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
            var bmp = icon.ToBitmap();
            return loadBitmap(bmp);
        }

        //private void cmdBrowse_Click(object sender, RoutedEventArgs e)
        //{
        //    var dlg = new OpenFileDialog();
        //    if (dlg.ShowDialog() == true)
        //    {
        //        img.Source = IconFromFile(dlg.FileName);
        //    }
        //}
    }
}
