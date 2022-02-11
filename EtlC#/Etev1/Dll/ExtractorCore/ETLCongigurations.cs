using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractorCore
{
    public class ETLCongigurations
    {

        public int timeThreadSleep { get; set; }

        public string data_dir_base { get; set; }
        public string data_name_base { get; set; }

        public string data_dir_files { get; set; }

        public string data_dir_files_raw { get; set; }



        public int MILISS { get; set; }


        public ETLCongigurations(string data_dir_base, string data_name_base,string data_dir_files,string data_dir_files_raw,int time_second_sleep=6)
        {
            timeThreadSleep = time_second_sleep;
            this.data_dir_base = data_dir_base;
            this.data_name_base = data_name_base;
            this.data_dir_files = data_dir_files;
            this.data_dir_files_raw=data_dir_files_raw; 
            this.MILISS = 1000;


        }
    }
}
