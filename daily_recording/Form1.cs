using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace daily_recording
{
    public partial class Form1 : Form
    {
        DateTime dt = new DateTime();
        string filename;
        StreamWriter recordfilesw;

        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "DAILY RECORDING: ONGOING";
            this.Opacity = 0.5;
            button2.Enabled = true;
            button1.Enabled = false;
            timer1.Enabled = true;
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",ENABLE,recording enabled," + dt.ToString("yyyyMMdd") + "\n");//hh:12,HH:24
            recordfilesw.Flush();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Text = "DAILY RECORDING: STOP";
            this.Opacity = 1;
            button2.Enabled = false;
            button1.Enabled = true;
            timer1.Enabled = false;
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",DISABLE,recording disabled," + dt.ToString("yyyyMMdd") + "\n");
            recordfilesw.Flush();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",END,program terminated," + dt.ToString("yyyyMMdd") + "\n");
            recordfilesw.Flush();
            recordfilesw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            //new day: after 4 a.m.
            if (dt.Hour > 4)
            {
                filename = "record" + dt.ToString("yyyyMMdd") + ".csv";
            }
            else
            {
                DateTime dt2 = dt.AddDays(-1);
                filename = "record" + dt2.ToString("yyyyMMdd") + ".csv";
            }
            //recordfile = new FileStream(filename, FileMode.OpenOrCreate);
            recordfilesw = new StreamWriter(filename, true);
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",START,program launched," + dt.ToString("yyyyMMdd") + "\n");//hh:12,HH:24
            recordfilesw.Flush();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",window name,description,move\n");
            recordfilesw.Flush();
        }
    }
}
