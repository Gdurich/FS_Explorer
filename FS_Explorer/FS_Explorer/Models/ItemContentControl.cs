using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS_Explorer.Models
{
    class ItemContentControl
    {
        public string Title { get; set; }
        public string FullAdress { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfChange { get; set; }
        public string Size { get; set; }
        public int ObjectCount { get; set; }
    }
}
