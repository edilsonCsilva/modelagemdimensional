using ExtractorCore.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace ExtractorCore
{
     class DimGeo
    {

        private IDataBase contex;
        public DimGeo(IDataBase contex)
        {
            this.contex = contex;
        }


        public bool Add(string latitude,string longitude,string address)

        {
            bool inserted = false;
            try
            {


                if (this.BySk(latitude,longitude) == 0)
                {

                    using (var cmd = this.contex.DbConnection().CreateCommand())
                    {

                        cmd.CommandText = "INSERT INTO dim_geo_sales(latitude, longitude, address ) values (@latitude, @longitude, @address)";
                        cmd.Parameters.AddWithValue("@latitude", latitude);
                        cmd.Parameters.AddWithValue("@longitude",longitude);
                        cmd.Parameters.AddWithValue("@address", address);
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



        public int BySk(string lat,string lng)
        {
            int artificialkey = 0;
            try
            {
                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT  sk_geosales, latitude, longitude, address" +
                                         "  FROM dim_geo_sales  Where latitude=@lat and  longitude=@lng ";
                        cmd.Parameters.AddWithValue("@lat", lat);
                        cmd.Parameters.AddWithValue("@lng", lng);

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


        public List<DimGeoEntity> By(string lat, string lng)
        {
            List<DimGeoEntity> geos = new List<DimGeoEntity>();
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT  sk_geosales, latitude, longitude, address" +
                                        "  FROM dim_geo_sales  Where latitude=@lat and  longitude=@lng ";
                        cmd.Parameters.AddWithValue("@lat", lat);
                        cmd.Parameters.AddWithValue("@lng", lng);

                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            if (sQLiteDataReader.Read())
                            {

                                DimGeoEntity geo = new DimGeoEntity();
                                geo.id = sQLiteDataReader.GetInt32(0);
                                geo.latitude = sQLiteDataReader.GetString(1);
                                geo.logitude = sQLiteDataReader.GetString(2);
                                geo.address = sQLiteDataReader.GetString(3);
                                geos.Add(geo);

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
            return geos;
        }


        public List<DimGeoEntity> All()
        {
            List<DimGeoEntity> geos = new List<DimGeoEntity>();
            try
            {


                try
                {
                    using (var cmd = contex.DbConnection().CreateCommand())
                    {
                        cmd.CommandText = "SELECT  sk_geosales, latitude, longitude, address FROM dim_geo_sales ";
                        SQLiteDataReader sQLiteDataReader = cmd.ExecuteReader();

                        if (sQLiteDataReader.HasRows)

                        {
                            while (sQLiteDataReader.Read())
                            {

                                DimGeoEntity geo = new DimGeoEntity();
                                geo.id = sQLiteDataReader.GetInt32(0);
                                geo.latitude = sQLiteDataReader.GetString(1);
                                geo.logitude = sQLiteDataReader.GetString(2);
                                geo.address = sQLiteDataReader.GetString(3);
                                geos.Add(geo);

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
            return geos;
        }









    }
}
