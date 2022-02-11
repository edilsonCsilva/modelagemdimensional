using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace ExtractorCore
{
    

    class SQliteBaseConection : IDataBase
    {
        private SQLiteConnection sqliteConnection;
        private string baseName, pathDir,baseData;
        private bool isCreatedBase = false;

        public SQliteBaseConection(string pathDir,string baseName)
        {

            try
            {
                if (!Directory.Exists(pathDir))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathDir);
                }

                this.pathDir = pathDir;
                this.baseName = baseName;
                this.baseData =string.Format(@"{0}\{1}",this.pathDir,this.baseName);
                this.CriarBancoSQLite();
            }
            catch
            {
                throw;
            }

        }


        public  SQLiteConnection DbConnection()
        {
            this.sqliteConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", this.baseData));
            this.sqliteConnection.Open();
            return sqliteConnection;
        }

        private  void CriarBancoSQLite()
        {
            try
            {
                if (!File.Exists(this.baseData))
                {
                    SQLiteConnection.CreateFile(this.baseData);
                }

                isCreatedBase = true;
                this.CriarTabelaSQlite();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                this.isCreatedBase = false;
            }
        }


        private  void CriarTabelaSQlite()
        {
            try
            {
                createdTableFileControl();
                createdTableDimCategory();
                createdTableDimGeoSales();
                createdTableDimProduct();
                createdTableDimTime();
                createdTableDimSales();
                createdTableDimStatus();
                createdTableFtSales();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 

        private void createdTableDimCategory()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS dim_category (" +
                        " sk_category INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " descricao TEXT"+
                        ");";


                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }


        private void createdTableDimSales()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS dim_sales(" +
                        "  sk_sales    INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "  uuid_sales TEXT," +
                         " description TEXT" +
                        ");";


                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }


        private void createdTableDimStatus()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS dim_state(" +
                        " sk_state    INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " description TEXT" +
                        ");";


                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }






        private void createdTableFileControl()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS file_control (" +
                        " uuid INTEGER PRIMARY KEY AUTOINCREMENT  UNIQUE  NOT NULL," +
                        " name_file       TEXT," +
                        " procesed_file   INT," +
                        " created_at      TEXT," +
                        " etl_executed_at TEXT" +
                        ");";

                     
                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }


        private void createdTableDimGeoSales()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS dim_geo_sales (" +
                        "sk_geosales INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "  latitude    TEXT," +
                        "longitude   TEXT," +
                        "address     TEXT" +
                        ");";


                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }

        private void createdTableDimProduct()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS dim_product  (" +
                        " sk_product INTEGER PRIMARY KEY AUTOINCREMENT," +
                        " id         INTEGER NOT NULL," +
                        " descricao  TEXT," +
                        " unit_price DOUBLE," +
                        " photo_path TEXT" +
                        ");";


                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }


        private void createdTableDimTime()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS dim_time   (" +
                        "sk_time INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "day     INTEGER," +
                        "month   INTEGER," +
                        "year    INTEGER," +
                        "hours   INTEGER," +
                        "minute  INTEGER" +
                        ");";


                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }



        private void createdTableFtSales()
        {
            try
            {
                using (var cmd = this.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE  IF NOT EXISTS ft_sales    (" +
                        "uuid                INTEGER PRIMARY KEY AUTOINCREMENT  UNIQUE," +
                        "sk_tf_sales_time    INTEGER REFERENCES dim_time (sk_time)," +
                        "sk_tf_category      INTEGER REFERENCES dim_category (sk_category)," +
                        "sk_ft_product       INTEGER REFERENCES dim_product (sk_product)," +
                        "sk_ft_geo_sales     INTEGER REFERENCES dim_geo_sales (sk_geosales)," +
                        "sk_sales            INTEGER REFERENCES dim_sales (sk_sales)," +
                        "sk_state            INTEGER REFERENCES dim_state (sk_state)," +
                        "unit_price_sales    DOUBLE," +
                        "quantity_of_items   INTEGER," +
                        "discount            INTEGER" +
                        ");";


                    cmd.ExecuteNonQuery();
                    this.isCreatedBase = isCreatedBase && true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                this.isCreatedBase = isCreatedBase && false;
            }
        }


        public bool hasConnection()
        {
            return this.isCreatedBase;
        }






    }
}
