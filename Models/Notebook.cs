using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByodLauncher.Models
{
    public class Notebook
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CpuBrand { get; set; }
        public string CpuModel { get; set; }
        public string GpuBrand { get; set; }
        public string GpuModel { get; set; }
        public string GpuMemorySize { get; set; }
        public string ScreenSize { get; set; }
        public string RamSize { get; set; }
        public string RamType { get; set; }
        public string StorageType { get; set; }
        public string StorageSize { get; set; }

    }
}
