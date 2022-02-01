using ExtractorCore.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
    class DimTime
    {

        private IDataBase contex;
        public DimTime(IDataBase contex)
        {
            this.contex = contex;
        }


        public bool Add(int day,int month,int year,int hours,int minute)
        {
            bool inserted = false;
            try
            {


                if(this.BySk(day,month,year,hours,minute)==0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "INSERT INTO dim_time(day, month, year,hours,minute ) values (@day, @month, @year,@hours,@minute)";
                        cmd.Parameters.AddWithValue("@day", day);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@hours", hours);
                        cmd.Parameters.AddWithValue("@minute", minute);
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                inserted = false;
            }
            return inserted; 
        }



        public int BySk(int day, int month, int year, int hours, int minute)
        {
            int artificialkey = 0;


            try
            {
             
               
                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT sk_time , day, month, year,hours,minute FROM dim_time Where" +
                                        " day=@day and  month=@month and year=@year and hours=@hours and minute=@minute ";
                        cmd.Parameters.AddWithValue("@day", day);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@hours", hours);
                        cmd.Parameters.AddWithValue("@minute", minute);
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


        public List<DimTimeEntity> By(int day, int month, int year, int hours, int minute)
        {
            List<DimTimeEntity> times = new List<DimTimeEntity>();
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT sk_time , day, month, year,hours,minute FROM dim_time Where" +
                                        " day=@day and  month=@month and year=@year and hours=@hours and minute=@minute ";
                        cmd.Parameters.AddWithValue("@day", day);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@hours", hours);
                        cmd.Parameters.AddWithValue("@minute", minute);
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            if(sQLiteDataReader.Read())
                            {

                                DimTimeEntity time = new DimTimeEntity();
                                time.id = sQLiteDataReader.GetInt32(0);
                                time.day = sQLiteDataReader.GetInt32(1);
                                time.month = sQLiteDataReader.GetInt32(2);
                                time.year = sQLiteDataReader.GetInt32(3);
                                time.hours = sQLiteDataReader.GetInt32(4);
                                time.minute = sQLiteDataReader.GetInt32(5);
                                times.Add(time);
                                
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
            return times;
        }


        public List<DimTimeEntity> All()
        {
            List<DimTimeEntity> times = new List<DimTimeEntity>();
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SELECT sk_time , day, month, year,hours,minute FROM dim_time ";
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            while (sQLiteDataReader.Read())
                            {

                                DimTimeEntity time = new DimTimeEntity();
                                time.id = sQLiteDataReader.GetInt32(0);
                                time.day = sQLiteDataReader.GetInt32(1);
                                time.month = sQLiteDataReader.GetInt32(2);
                                time.year = sQLiteDataReader.GetInt32(3);
                                time.hours = sQLiteDataReader.GetInt32(4);
                                time.minute = sQLiteDataReader.GetInt32(5);
                                times.Add(time);

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
            return times;
        }







    }
}
