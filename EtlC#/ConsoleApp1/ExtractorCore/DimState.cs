using ExtractorCore.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
     class DimState
    {
        private IDataBase contex;
        public DimState(IDataBase contex)
        {
            this.contex = contex;

            this.Add("Entregues".ToUpper());
            this.Add("Entregua Parcial".ToUpper());
            this.Add("Cancelados".ToUpper());
            this.Add("Em Producao".ToUpper());





        }

        public bool Add(string state)
        {
            bool inserted = false;
            try
            {


                if (this.BySkState(state) == 0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "INSERT INTO dim_state(description ) values (@description)";
                        cmd.Parameters.AddWithValue("@description", state);
                    
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


        public int BySkState(string state)
        {
            int artificialkey = 0;


            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT sk_state FROM dim_state Where   description=@description ";
                        cmd.Parameters.AddWithValue("@description", state);
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


        public List<DimStateEntity> allState()
        {
            List<DimStateEntity> states = new List<DimStateEntity>();


            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT * FROM dim_state ";
                         
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();
                        if (sQLiteDataReader.HasRows)

                        {
                            while (sQLiteDataReader.Read())
                            {
                               
                                DimStateEntity dimStateEntity = new DimStateEntity();
                                dimStateEntity.sk_state= sQLiteDataReader.GetInt32(0);
                                dimStateEntity.description = sQLiteDataReader.GetString(1);
                                states.Add(dimStateEntity);

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
            return states;
        }

        public int BySk(int state)
        {
            int artificialkey = 0;


            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT sk_state FROM dim_state Where   sk_state=@sk_state ";
                        cmd.Parameters.AddWithValue("@sk_state", state);
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
