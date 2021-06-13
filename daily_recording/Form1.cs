using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace daily_recording
{
    public partial class Form1 : Form
    {
        DateTime dt = new DateTime();
        string filename;        
        StreamWriter recordfilesw;
        HttpWebRequest req;
        HttpWebResponse response;
        Stream stream;
        int fundcodeidx;
        int nameidx;
        int jzrqidx;
        int dwjzidx;
        int gszidx;
        int gszzlidx;
        int gztimeidx;
        string content;
        DateTime starttime = new DateTime(2021, 8, 9, 9, 15, 0);
        DateTime lunchstart = new DateTime(2021, 8, 1, 11, 35, 0);
        DateTime lunchend = new DateTime(2021, 8, 1, 12, 55, 0);
        DateTime endtime = new DateTime(2021, 8, 1, 15, 5, 0);
        int refminute = 14;

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
            Process.Start("explorer.exe", System.Environment.CurrentDirectory);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",END,program terminated," + dt.ToString("yyyyMMdd") + "\n");
            recordfilesw.Flush();
            recordfilesw.Close();
        }


        //shortcut to C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp for autorunning
        private void Form1_Load(object sender, EventArgs e)
        {
            //if(IntPtr.Zero!=FindWindow("DAILY RECORDING"))//避免多开
            //{
            //    MessageBox.Show("程序已经打开！");
            //    Process.GetCurrentProcess().Kill();
            //}

            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if ( p.ProcessName == "daily_recording" && p.Id!= Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("程序已经打开！");
                    //Process.GetCurrentProcess().Kill();
                }
            }

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
            recordfilesw = new StreamWriter(filename, true,System.Text.Encoding.Default);
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",START,program launched," + dt.ToString("yyyyMMdd") + "\n");//hh:12,HH:24
            recordfilesw.Flush();

            

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Opacity = 0;

            //button1.PerformClick();

            timer1.Enabled = true;
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",ENABLE,recording enabled," + dt.ToString("yyyyMMdd") + "\n");//hh:12,HH:24
            recordfilesw.Flush();

            sTARTRECORDINGToolStripMenuItem.Enabled = false;
            sHOWFORMToolStripMenuItem.Enabled = false;
            notifyIcon1.ShowBalloonTip(1000, "notice", "recording started", ToolTipIcon.Info);
        }

        private void sHOWFORMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Opacity = 1;
        }

        private void sTARTRECORDINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //button1.PerformClick();

            timer1.Enabled = true;
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",ENABLE,recording enabled," + dt.ToString("yyyyMMdd") + "\n");//hh:12,HH:24
            recordfilesw.Flush();

            sTARTRECORDINGToolStripMenuItem.Enabled = false;
            sTOPRECORDINGToolStripMenuItem.Enabled = true;
            notifyIcon1.Icon = daily_recording.Properties.Resources.pic1;
            notifyIcon1.ShowBalloonTip(1000, "notice", "recording started", ToolTipIcon.Info);
        }

        private void sTOPRECORDINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //button2.PerformClick();

            timer1.Enabled = false;
            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",DISABLE,recording disabled," + dt.ToString("yyyyMMdd") + "\n");
            recordfilesw.Flush();
            notifyIcon1.Icon = daily_recording.Properties.Resources.pic2;
            Process.Start("explorer.exe", System.Environment.CurrentDirectory);            

            sTARTRECORDINGToolStripMenuItem.Enabled = true;
            sTOPRECORDINGToolStripMenuItem.Enabled = false;
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dt = DateTime.Now;
            recordfilesw.Write(dt.ToString("HH,mm,ss") + ",END,program terminated," + dt.ToString("yyyyMMdd") + "\n");
            recordfilesw.Flush();
            recordfilesw.Close();
            Process.Start("explorer.exe", System.Environment.CurrentDirectory);

            Process.GetCurrentProcess().Kill();
        }

        private void sHOWRECORDINGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", System.Environment.CurrentDirectory);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            bool netstate = true;
            if ( (dt.DayOfWeek!= DayOfWeek.Saturday) && (dt.DayOfWeek != DayOfWeek.Sunday) &&
                ((dt.TimeOfDay>=starttime.TimeOfDay && dt.TimeOfDay <= lunchstart.TimeOfDay) ||
                    (dt.TimeOfDay >= lunchend.TimeOfDay && dt.TimeOfDay <= endtime.TimeOfDay))
                    )
            {
                if (dt.Minute!=refminute)
                {
                    for(int idx=1;idx<=30;idx++)
                    {
                        if (readresponse("006229") == 0 && gztimeidx - gszzlidx - 11>0)
                        {
                            netstate = true;
                            break;
                        }
                        System.Threading.Thread.Sleep(1000);
                        netstate = false;
                    }
                    if (netstate)
                    {
                        recordfilesw.Write (
                                dt.ToString("HH:mm:ss, ") + "guzhizhangfu:"+
                                content.Substring(gszzlidx + 9, gztimeidx - gszzlidx - 11) +
                                ", guzhitime:" +
                                content.Substring(gztimeidx + 10, 16) +
                                "\n");
                    }
                    else
                    {
                        recordfilesw.Write(dt.ToString("HH:mm:ss,") + "bad net \n");
                    }
                    recordfilesw.Flush();
                    refminute = dt.Minute;
                }         
            }
            //else if(dt.TimeOfDay > endtime.TimeOfDay)
            //{
            //    recordfilesw.Write("\n");
            //    recordfilesw.Flush();
            //}
                

        }

        int readresponse(string jijincode)
        {
            req = (HttpWebRequest)HttpWebRequest.Create("http://fundgz.1234567.com.cn/js/" + jijincode + ".js?");
            //req.Method = "GET"; //default value
            //req.ProtocolVersion = new Version(1, 1); //default value
            try 
            { 
                response = req.GetResponse() as HttpWebResponse; 
            }
            catch
            {
                return -1;
            }
            
            ////Header
            //foreach (var item in response.Headers)
            //{
            //    this.label1.Text += item.ToString() + ": " +
            //    response.GetResponseHeader(item.ToString())
            //    + System.Environment.NewLine;
            //}
            //如果主体信息不为空，则接收主体信息内容
            if (response.ContentLength <= 0)
                return -1;
            //接收响应主体信息
            stream = response.GetResponseStream();

            int totalLength = (int)response.ContentLength;
            int numBytesRead = 0;
            byte[] bytes = new byte[totalLength + 1024];
            //通过一个循环读取流中的数据，读取完毕，跳出循环
            while (numBytesRead < totalLength)
            {
                int num = stream.Read(bytes, numBytesRead, 1024);  //每次希望读取1024字节
                if (num == 0)   //说明流中数据读取完毕
                    break;
                numBytesRead += num;
            }

            //将接收到的主体数据显示到界面
            content = Encoding.UTF8.GetString(bytes);

            //:"2021-04-30","dwjz":"2.7621","gsz":"2.6768","gszzl":"-3.09","gztime":"2021-05-06 15:00"});

            fundcodeidx = content.IndexOf("\"fundcode\":\"");
            nameidx = content.IndexOf("\"name\":\"");
            jzrqidx = content.IndexOf("\"jzrq\":\"");
            dwjzidx = content.IndexOf("\"dwjz\":\"");
            gszidx = content.IndexOf("\"gsz\":\"");
            gszzlidx = content.IndexOf("\"gszzl\":\"");
            gztimeidx = content.IndexOf("\"gztime\":\"");

            return 0;
        }
    }
}
