using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractorCore.Entity
{
    public class FileControlEntity
    {
        //uuid, name_file, procesed_file, created_at, etl_executed_at
        public int uuid { get; set; }
        public string name_file { get; set; }
        public int procesed_file { get; set; }
        public string created_at { get; set; }

        public string etl_executed_at { get; set; }

    }
}
