using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
     class DimSale
    {

        private IDataBase contex;
        public DimSale(IDataBase contex)
        {
            this.contex = contex;

        }





        public bool Add(string uudSales,string description)
        {
            bool inserted = false;
            try
            {


                if (this.BySkUuidSales(uudSales) == 0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "INSERT INTO dim_sales(uuid_sales,description ) values (@uudSales,@description)";
                        cmd.Parameters.AddWithValue("@uudSales", uudSales);
                        cmd.Parameters.AddWithValue("@description", description.ToUpper());

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                inserted = false;
            }
            return inserted;
        }


        public int BySkUuidSales(string uuidSales)
        {
            int artificialkey = 0;
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT sk_sales FROM dim_sales Where   uuid_sales=@uuid_sales ";
                        cmd.Parameters.AddWithValue("@uuid_sales", uuidSales);
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();
                        if (sQLiteDataReader.HasRows)

                        {
                            while (sQLiteDataReader.Read())
                            {
                                artificialkey = sQLiteDataReader.GetInt32(0);
                                break;
                            }
                        }
                        sQLiteDataReader.Close();
                        cmd.Connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());

            }
            return artificialkey;
        }


        public int BySk(int id)
        {
            int artificialkey = 0;
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT sk_sales FROM dim_sales Where   sk_sales=@sk_sales ";
                        cmd.Parameters.AddWithValue("@sk_sales", id);
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();
                        if (sQLiteDataReader.HasRows)

                        {
                            while (sQLiteDataReader.Read())
                            {
                                artificialkey = sQLiteDataReader.GetInt32(0);
                                break;
                            }
                        }
                        sQLiteDataReader.Close();
                        cmd.Connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());

            }
            return artificialkey;
        }


    }
}
