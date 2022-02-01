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
    public partial class Form1 : Form , OnResponse
    {
        ETLCongigurations config;
        ETLCore EtlApp;
        private bool   istarted=false;
        delegate void ReceberMensagemCallback(string msg);

        public Form1()
        {
            InitializeComponent();
            pictureBox2.Visible = false;
            config = new ETLCongigurations(
                     @"C:\ModelagemDimencional\base",
                     "dw.db",
                     @"C:\Users\edi\modelagemdimensional\data",
                     @"C:\ModelagemDimencional\base\raw", 3);

        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {


         
            if (istarted)
            {
                EtlApp.Stop();
                istarted = false;
                pictureBox2.Visible = istarted;
            }
            else
            {
                EtlApp = new ETLCore(config, this);
                EtlApp.Init();
                istarted = true;
                pictureBox2.Visible = istarted;

            }
           




        }

         
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void OnResponse(object obj)
        {
            try
            {
                
                if (InvokeRequired)
                {
                    ReceberMensagemCallback callback = OnResponse;
                    Invoke(callback, (string)obj);
                   
                }
                else
                {
                    output.AppendText(Environment.NewLine + ((string)obj));
                    pictureBox2.Visible = true;


                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
               

            }
        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(linkLabel1.Text);
            }
            catch
            {
                throw;
            }
        }
    }
}
