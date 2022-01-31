using ExtractorCore.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
    class DimCategory
    {

        private IDataBase contex;
        public DimCategory(IDataBase contex)
        {
            this.contex = contex;
        }




        public bool Add(string category)

        {
            bool inserted = false;
            try
            {


                if (this.BySk(category)==0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {

                        cmd.CommandText = "INSERT INTO dim_category(descricao ) values (@descricao)";
                        cmd.Parameters.AddWithValue("@descricao", category);
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



        public int BySk(string category)
        {
            int artificialkey = 0;
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT sk_category,descricao  FROM dim_category  Where descricao=@description";
                        cmd.Parameters.AddWithValue("@description", category);

                       
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            if (sQLiteDataReader.Read())
                            {
                                artificialkey = sQLiteDataReader.GetInt32(0);
                               
                            }
                        }
                        sQLiteDataReader.Close();

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


        public List<DimCategoryEntity> By(string category)
        {
            List<DimCategoryEntity> categorys = new List<DimCategoryEntity>();
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT sk_category  , descricao  FROM dim_category  Where  descricao=@description ";
                        cmd.Parameters.AddWithValue("@description", category);
                      
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            if (sQLiteDataReader.Read())
                            {

                                DimCategoryEntity categoria = new DimCategoryEntity();
                                categoria.id = sQLiteDataReader.GetInt32(0);
                                categoria.descricao = sQLiteDataReader.GetString(1);
                                categorys.Add(categoria);

                            }
                        }
                        sQLiteDataReader.Close();

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
            return categorys;
        }


        public List<DimCategoryEntity> All()
        {
            List<DimCategoryEntity> categorys = new List<DimCategoryEntity>();
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT sk_category  , descricao  FROM dim_category   ";
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            while (sQLiteDataReader.Read())
                            {

                                DimCategoryEntity categoria = new DimCategoryEntity();
                                categoria.id = sQLiteDataReader.GetInt32(0);
                                categoria.descricao = sQLiteDataReader.GetString(1);
                                categorys.Add(categoria);

                            }
                        }
                        sQLiteDataReader.Close();

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
            return categorys;
        }










    }
}
