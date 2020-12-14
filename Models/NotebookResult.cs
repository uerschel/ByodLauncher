using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByodLauncher.Models
{

    [Serializable]
    public class Rootobject
    {
        public int code { get; set; }
        public string message { get; set; }
        public Result result { get; set; }
        public string daily_hits_left { get; set; }
    }

    [Serializable]
    public class Result
    {
        public _0 _0 { get; set; }
    }

    [Serializable]
    public class _0
    {
        public Model_Info[] model_info { get; set; }
        public Cpu cpu { get; set; }
        public Display display { get; set; }
        public Memory memory { get; set; }
        public Primary_Storage primary_storage { get; set; }
        public Gpu gpu { get; set; }
        public string operating_system { get; set; }
    }


    [Serializable]
    public class Cpu
    {
        public string prod { get; set; }
        public string model { get; set; }
        public string lithography { get; set; }
        public string cache { get; set; }
        public string base_speed { get; set; }
        public string boost_speed { get; set; }
        public string cores { get; set; }
        public string tdp { get; set; }
        public string other_info { get; set; }
        public string rating { get; set; }
        public string integrated_video_id { get; set; }
        public string integrated_video { get; set; }
    }

    [Serializable]
    public class Display
    {
        public string size { get; set; }
        public string horizontal_resolution { get; set; }
        public string vertical_resolution { get; set; }
        public string type { get; set; }
        public string sRGB { get; set; }
        public string touch { get; set; }
        public string other_info { get; set; }
    }

    [Serializable]
    public class Memory
    {
        public string size { get; set; }
        public string speed { get; set; }
        public string type { get; set; }
    }

    [Serializable]
    public class Primary_Storage
    {
        public string model { get; set; }
        public string cap { get; set; }
        public object rpm { get; set; }
        public string read_speed { get; set; }
    }



    [Serializable]
    public class Gpu
    {
        public string prod { get; set; }
        public string model { get; set; }
        public string architecture { get; set; }
        public string lithography { get; set; }
        public string shaders { get; set; }
        public string base_speed { get; set; }
        public string boost_speed { get; set; }
        public string shader_speed { get; set; }
        public string memory_speed { get; set; }
        public string memory_bandwidth { get; set; }
        public string memory_size { get; set; }
        public string memory_type { get; set; }
        public string tdp { get; set; }
        public string other_info { get; set; }
        public string rating { get; set; }
    }


    [Serializable]
    public class Model_Info
    {
        public int id { get; set; }
        public string noteb_name { get; set; }
        public string name { get; set; }
        public string extra_name { get; set; }
        public string[] submodel_info { get; set; }
    }

}
