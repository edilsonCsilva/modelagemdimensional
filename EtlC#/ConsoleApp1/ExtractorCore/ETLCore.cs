using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ExtractorCore
{


    public class ETLCore
    {
        private OnResponse onResponse;
        private ETLCongigurations configurations;
        private Thread App = null;


        public ETLCore(ETLCongigurations configurations,OnResponse onResponse)
        {
            this.onResponse = onResponse;
            this.configurations = configurations;
        }

        public void Init()
        {
          
            try
            {
                this.App = new Thread(new ThreadStart(this.ThreadTask));
                this.App.IsBackground = true;
                this.App.Start();
                this.onResponse.OnResponse("Start");
            }
            catch
            {

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

            }
        }


        private void ThreadTask()
        {
           
             

            while (true)
            {



                List<string> filesDir = Uteis.getFilesDir(configurations.data_dir_files);




                this.onResponse.OnResponse("to Vivo");
                Thread.Sleep(this.configurations.timeThreadSleep * this.configurations.MILISS );
            }
        }

    }
}
