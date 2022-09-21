using System;

namespace EasyImageProcessingResultChecker
{
    public class FilterHistory
    {
        public int id { get; set; }
        public String filter_name { get; set; }
        public int kernel_size { get; set; }
        public int threshold1 { get; set; }
        public int threshold2 { get; set; }
        public bool is_selected { get; set; }
    }
}
