using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractorCore.Entity
{
    public class FatSalesEntity
    {

        public int uuid { get; set; }
        public int sk_tf_sales_time { get; set; }
        public int sk_tf_category { get; set; }
        public int sk_ft_product { get; set; }
        public int sk_ft_geo_sales { get; set; }
        public double unit_price_sales { get; set; }
        public int quantity_of_items { get; set; }
        public int discount { get; set; }


       

    }
}
