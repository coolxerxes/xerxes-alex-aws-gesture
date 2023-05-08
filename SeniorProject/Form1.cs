using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSdkNet;
using GSdkNet.Carrier.Example;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace SeniorProject
{
    public partial class Form1 : Form
    {
        private delegate void SetTextCallback(string text);
        SPstream mystream;
        public Form1()
        {
            InitializeComponent();
            //String folderName = Environment.SpecialFolder.DesktopDirectory.ToString() + "\\" + "CaptoStream";
            String folderName = "C:\\CaptoStream";
            //String folderName = "C:\\Users\\9SAD\\Desktop\\CaptoStream";
            //String folderName = "CaptoStream";
            Directory.CreateDirectory(folderName);
            Directory.SetCurrentDirectory(folderName);
            //textBox3.Text = folderName;
            //mystream = new HindStream1(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {


            //int duration = Int32.Parse(textBox1.Text); 
            textBox3.AppendText(Environment.NewLine + "***New Gestre***" + Environment.NewLine);
            mystream = new SPstream(this, textBox2.Text);
            await mystream.StartAsync();

        }
        public void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox3.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (this != null)
                    this.textBox3.AppendText(text);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Environment.Exit(1);
            await mystream.StopAsync();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

