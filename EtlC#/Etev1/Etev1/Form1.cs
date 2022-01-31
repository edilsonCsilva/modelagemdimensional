using ExtractorCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Etev1
{
    public partial class Form1 : Form
    {

        class Response : OnResponse
        {
            public void OnResponse(object obj)
            {
                Console.WriteLine(obj);
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();

            ETLCongigurations config = new ETLCongigurations(
                 @"C:\ModelagemDimencional\base",
                 "teste2.db",
                 @"C:\Users\edi\modelagemdimensional\data"
                 , 3);


            ETLCore EtlApp = new ETLCore(config, new Response());
            EtlApp.Init();
          





        }



        public static void ThreadProc()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadProc: {0}", i);
                // Yield the rest of the time slice.
                Thread.Sleep(0);
            }
        }







    }
}
