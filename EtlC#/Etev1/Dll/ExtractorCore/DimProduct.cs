using ExtractorCore.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
     class DimProduct
    {
        private IDataBase contex;
        public DimProduct(IDataBase contex)
        {
            this.contex = contex;
        }


        public int BySk(string idproduction)
        {
            int artificialkey = 0;
            try
            {
                try
                {
            

                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT  sk_product FROM dim_product  Where id=@idproduction ";
                        cmd.Parameters.AddWithValue("@idproduction", idproduction);
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();
                        if (sQLiteDataReader.HasRows)
                        {
                            if (sQLiteDataReader.Read())
                            {
                                artificialkey = sQLiteDataReader.GetInt32(0);

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


        public bool Add(string id, string descricao, string unit_price, string photo_path)

        {
            bool inserted = false;
            try
            {


                if (this.BySk(id) == 0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;

                        cmd.CommandText = "INSERT INTO dim_product(id, descricao, unit_price, photo_path)" +
                            " values (@id, @descricao, @unit_price, @photo_path)";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@descricao", descricao);
                        cmd.Parameters.AddWithValue("@unit_price", unit_price);
                        cmd.Parameters.AddWithValue("@photo_path", photo_path);

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


        public List<DimProducEntity> By(string id)
        {
            List<DimProducEntity> productions = new List<DimProducEntity>();
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT  sk_product, id, descricao, unit_price, photo_path " +
                                        "  FROM dim_product  Where id=@id  ";
                        cmd.Parameters.AddWithValue("@id", id);
          

                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            if (sQLiteDataReader.Read())
                            {

                                DimProducEntity product = new DimProducEntity();
                                product.sk_id = sQLiteDataReader.GetString(0);
                                product.id = sQLiteDataReader.GetInt32(1);
                                product.description = sQLiteDataReader.GetString(2);
                                product.unitPrice = sQLiteDataReader.GetDouble(3);
                                product.photo = sQLiteDataReader.GetString(4);
                                productions.Add(product);

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
            return productions;
        }


        public List<DimProducEntity> All()
        {
            List<DimProducEntity> productions = new List<DimProducEntity>();
            try
            {
                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT  sk_product, id, descricao, unit_price, photo_path " +
                                        "  FROM dim_product    ";
                      


                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            while (sQLiteDataReader.Read())
                            {

                                DimProducEntity product = new DimProducEntity();
                                product.sk_id = sQLiteDataReader.GetString(0);
                                product.id = sQLiteDataReader.GetInt32(1);
                                product.description = sQLiteDataReader.GetString(2);
                                product.unitPrice = sQLiteDataReader.GetDouble(3);
                                product.photo = sQLiteDataReader.GetString(4);
                                productions.Add(product);

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
            return productions;
        }


    }
}
