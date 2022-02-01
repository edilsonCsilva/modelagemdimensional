using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
     class FactSales
    {

        private IDataBase contex;
        public FactSales(IDataBase contex)
        {
            this.contex = contex;
        }

        public bool Add(int sk_tf_sales_time, int sk_tf_category, int sk_ft_product, int sk_ft_geo_sales,
                        double unit_price_sales, int quantity_of_items, int discount)

        {
            bool inserted = false;
            try
            {


           
                using (var cmd = this.contex.DbConnection().CreateCommand())
                    {


                    cmd.CommandTimeout = 0;

                    cmd.CommandText = "INSERT INTO ft_sales(sk_tf_sales_time, sk_tf_category, sk_ft_product," +
                                            " sk_ft_geo_sales, unit_price_sales, quantity_of_items, discount )" +
                                            " values(@sk_tf_sales_time,@sk_tf_category,@sk_ft_product," +
                                            "@sk_ft_geo_sales,@unit_price_sales,@quantity_of_items,@discount)";


                        cmd.Parameters.AddWithValue("@sk_tf_sales_time", sk_tf_sales_time);
                        cmd.Parameters.AddWithValue("@sk_tf_category", sk_tf_category);
                        cmd.Parameters.AddWithValue("@sk_ft_product", sk_ft_product);
                        cmd.Parameters.AddWithValue("@sk_ft_geo_sales", sk_ft_geo_sales);
                        cmd.Parameters.AddWithValue("@unit_price_sales", unit_price_sales);
                        cmd.Parameters.AddWithValue("@quantity_of_items", quantity_of_items);
                        cmd.Parameters.AddWithValue("@discount", discount);
                       // Console.WriteLine(cmd.CommandText.ToString());


                    if (cmd.ExecuteNonQuery() > 0)
                        {
                            inserted = true;
                        
                    }
                        else
                        {
                            inserted = false;
                        }
                        
                    cmd.Connection.Close();
                        
                    }
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                inserted = false;
            }
            return inserted;
        }

















    }
}
