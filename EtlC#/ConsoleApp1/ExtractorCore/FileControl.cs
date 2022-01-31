using ExtractorCore.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
    class FileControl
    {


        private IDataBase contex;
        public FileControl(IDataBase contex)
        {
            this.contex = contex;
        }


        public int BySkNameFile(string filename)
        {
            int artificialkey = 0;
            try
            {
                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT  uuid FROM file_control  Where name_file=@name_file";
                        cmd.Parameters.AddWithValue("@name_file", filename);
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



        public List<FileControlEntity> By(int id)
        {
            List<FileControlEntity> files = new List<FileControlEntity>();
            try
            {
                try
                {
                    
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT  uuid, name_file, procesed_file, created_at, etl_executed_at" +
                                        "  FROM file_control  Where uuid=@uuid ";
                        cmd.Parameters.AddWithValue("@uuid", id);
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            if (sQLiteDataReader.Read())
                            {

                                FileControlEntity file = new FileControlEntity();
                                file.uuid = sQLiteDataReader.GetInt32(0);
                                file.name_file = sQLiteDataReader.GetString(1);
                                file.procesed_file = sQLiteDataReader.GetInt32(2);
                                file.created_at = (!sQLiteDataReader.IsDBNull(3)) ? sQLiteDataReader.GetString(3):"" ;
                                file.etl_executed_at = (!sQLiteDataReader.IsDBNull(4)) ? sQLiteDataReader.GetString(4) : "";
                                files.Add(file);

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
            return files;
        }




        public bool UpdateStatusProcess(int uuid,int status=2)

        {
            bool inserted = false;
            try
            {


                if (this.By(uuid).Count  > 0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {
                        DateTime aDate = DateTime.Now;
                        string datatime = aDate.ToString("MM/dd/yyyy HH:mm:ss");

                        cmd.CommandText = "UPDATE file_control set  procesed_file=@status,etl_executed_at=@datatime where uuid=@uuid";
                        cmd.Parameters.AddWithValue("@uuid", uuid);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@datatime", datatime);


                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            inserted = true;
                        }
                        else
                        {
                            inserted = false;
                        }
                         
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


        public bool Add(string name_file)

        {
            bool inserted = false;
            try
            {


                if (this.BySkNameFile(name_file) == 0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {
                        DateTime aDate = DateTime.Now;
                        string datatime = aDate.ToString("MM/dd/yyyy HH:mm:ss");

                        cmd.CommandText = "INSERT INTO file_control(name_file, procesed_file, created_at ) " +
                                          "values (@name_file, @procesed_file, @created_at)";

                        cmd.Parameters.AddWithValue("@name_file", name_file);
                        cmd.Parameters.AddWithValue("@procesed_file", 1);
                        cmd.Parameters.AddWithValue("@created_at", datatime);
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






    }
}
