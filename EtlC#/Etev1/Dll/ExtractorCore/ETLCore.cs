using ExtractorCore.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ExtractorCore
{
    public class ETLCore
    {
        private OnResponse onResponse;
        private ETLCongigurations configurations;
        private Thread App = null;
        private List<ErrorInfo> stask = new List<ErrorInfo>();   
        private string fileAlreadyProcessed="";
        private SQliteBaseConection contex;
        private DimTime DimTime = null;
        private DimCategory DimCategory = null;
        private DimGeo DimGeo = null;
        private DimProduct DimProduct = null;
        private FactSales FactSales = null; 
        private FileControl FileControl = null;
        private DimState DimState = null;
        private DimSale DimSale = null;
        List<DimStateEntity> dimStateEntities = null;
        private bool isDirCopyRaw=false;




        public ETLCore(ETLCongigurations configurations,OnResponse onResponse)
        {
            this.onResponse = onResponse;
            this.configurations = configurations;
            this.contex = new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base);
        }


        public void Init()
        {
          
            try
            {

                this.DimProduct = new DimProduct(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));
                this.DimTime = new DimTime(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));
                this.DimGeo=new DimGeo(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));
                this.FileControl=new FileControl(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));  
                this.FactSales = new FactSales(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));    
                this.DimCategory=new DimCategory(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));
                this.DimState = new DimState(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));
                this.DimSale = new DimSale(new SQliteBaseConection(configurations.data_dir_base, configurations.data_name_base));

                this.isDirCopyRaw=Uteis.createdDir(configurations.data_dir_files_raw);
                this.App = new Thread(new ThreadStart(this.ThreadTask));
                this.App.IsBackground = true;
                this.App.Start();
                this.onResponse.OnResponse("Start");
            }
            catch(Exception ex) 
            {
                stask.Add(new ErrorInfo("Error ao Iniciar Processo :"+ex.Message.ToString()));
            }


        }


        public void Stop()
        {
            try
            {
                this.App.Abort();
                this.onResponse.OnResponse("Stop");
            }
            catch{
                stask.Add(new ErrorInfo("Error ao Parar Processo"));
            }
        }


        private void ThreadTask()
        {
            Random randNum = new Random();
            dimStateEntities = DimState.allState();
            fileAlreadyProcessed = FileControl.AllString();
            while (true)
            {

                try
                {
                    String line;
                    int SurrogateTime = 0, SurrogateCategory = 0, SurrogateGeo = 0, SurrogateProduct = 0, SurrogateFile=0, SurrogateSales=0, SurrogateState=0; 
                   
                    List<string> filesDir = Uteis.getFilesDir(configurations.data_dir_files);
                    foreach (string file in filesDir)
                    {
                        // Retira o diretório iformado inicialmente
                        if (!fileAlreadyProcessed.Contains(file)){
                         
                            StreamReader sr = new StreamReader(configurations.data_dir_files + file);
                            line = sr.ReadLine();
                            while (line != null)
                            {
                                string [] rows = line.Split(';');
                                if(rows.Length == 11)
                                {

                                    rows[10] = rows[10].Replace('[', ' ');
                                    rows[10] = rows[10].Replace(']', ' ');
                                    string [] dataParse = rows[1].Split(' ');
                                    string [] datas = dataParse[0].Split('-');
                                    string [] times = dataParse[1].Split(':');
                                    SurrogateTime = DimTime.BySk(
                                        Int32.Parse(datas[0]), Int32.Parse(datas[1]), Int32.Parse(datas[2]),
                                        Int32.Parse(times[0]), Int32.Parse(times[1])
                                        );
                                    if (SurrogateTime == 0)
                                    {
                                        if(DimTime.Add(
                                        Int32.Parse(datas[0]), Int32.Parse(datas[1]), Int32.Parse(datas[2]),
                                        Int32.Parse(times[0]), Int32.Parse(times[1])
                                        ))
                                        {
                                            SurrogateTime = DimTime.BySk(
                                                Int32.Parse(datas[0]), Int32.Parse(datas[1]), Int32.Parse(datas[2]),
                                                Int32.Parse(times[0]), Int32.Parse(times[1])
                                                );
                                        }

                                    }

                                    SurrogateCategory = DimCategory.BySk(rows[4].ToUpper());
                                    if (SurrogateCategory == 0)
                                    {
                                        if (DimCategory.Add(rows[4].ToUpper()))
                                        {
                                            SurrogateCategory = DimCategory.BySk(rows[4]);
                                        }

                                    }
                                    string[] GeoAddress = rows[10].Split(',');
                                    string[] geo = GeoAddress[0].Split('|');
                                    string address = "";
                                    int index = 0;
                                    foreach(string st in GeoAddress)
                                    {
                                        if(index > 0)
                                            address += st;

                                        index++;    
                                    }

                                   
                                    SurrogateGeo = DimGeo.BySk(geo[0], geo[1]);
                                    if (SurrogateGeo == 0)
                                    {
                                        if (DimGeo.Add(geo[0], geo[1],address.ToUpper()))
                                        {
                                            SurrogateGeo = DimGeo.BySk(geo[0], geo[1]);
                                        }

                                    }

                                    SurrogateProduct = DimProduct.BySk(rows[3]);
                                    if (SurrogateProduct == 0)
                                    {
                                        if (DimProduct.Add(rows[3], rows[2], rows[6], rows[5]))
                                        {
                                            SurrogateProduct = DimProduct.BySk(rows[3]);
                                        }
                                    }


                                    SurrogateSales = DimSale.BySkUuidSales(rows[0]);
                                    if (SurrogateSales == 0)
                                    {
                                       
                                        string descript = "Minha Venda - " + randNum.Next(1, 9999).ToString();

                                        if (DimSale.Add(rows[0],descript))
                                        {
                                            SurrogateSales = DimSale.BySkUuidSales(rows[0]);
                                        }
                                    }
                                    SurrogateState = dimStateEntities[(randNum.Next(0, dimStateEntities.Count - 1))].sk_state;

                                    if (SurrogateTime > 0 && SurrogateCategory > 0 && SurrogateGeo > 0 && SurrogateProduct > 0
                                        && SurrogateSales >0 && SurrogateState > 0)
                                    {
  
                                        if(FactSales.Add(SurrogateTime, SurrogateCategory, SurrogateProduct, SurrogateGeo, SurrogateSales, SurrogateState,
                                                        Double.Parse(rows[6]),Int32.Parse(rows[8]),Int32.Parse(rows[9])))
                                        {
                                            SurrogateFile = FileControl.BySkNameFile(file);
                                            if (SurrogateFile==0)
                                            {
                                                FileControl.Add(file);
                                                SurrogateFile = FileControl.BySkNameFile(file);
                                                FileControl.UpdateStatusProcess(SurrogateFile);
                                                fileAlreadyProcessed += string.Format("{0}|", file);
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    fileAlreadyProcessed += string.Format("{0}|", file);

                                }
                                line = sr.ReadLine();
                            }
                            //close the file
                            sr.Close();
                        }
                        string [] fts=file.Split('.');
                        string fileRaw=fts[0]+"_"+Uteis.getNumbers(Uteis.getDateTime())+"_OK."+fts[1]; 
                        File.Move(configurations.data_dir_files + file, configurations.data_dir_files_raw + fileRaw);
                        string resumo = string.Format("{0} - {1}",Uteis.getDateTime(),fileRaw);
                        this.onResponse.OnResponse(resumo);

                    }

                   
                }
                catch (Exception ex)
                {
                    stask.Add(new ErrorInfo("Error ao Procesar Arquivo Processo :" + ex.Message.ToString()));
                }
                Thread.Sleep(this.configurations.timeThreadSleep * this.configurations.MILISS );
            }
        }

    }
}
