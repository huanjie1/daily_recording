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
        StreamWriter logfile;
        StreamReader configfile;
        string [] jijincode = new string[100]; 

        string jijinname;
        string jijindate;
        DateTime reftime = new DateTime(2021, 6, 10, 16, 10, 0);
        DateTime starttime = new DateTime(2021, 8, 9, 9, 10, 0);
        //DateTime lunchstart = new DateTime(2021, 8, 1, 11, 35, 0);
        //DateTime lunchend = new DateTime(2021, 8, 1, 12, 55, 0);
        DateTime endtime = new DateTime(2021, 8, 1, 15, 5, 0);
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
            logfile.Write( dt.ToString("yyyyMMdd") + dt.ToString("HH,mm,ss") + ",ENABLE,recording enabled," +"\n");//hh:12,HH:24
            logfile.Flush();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Text = "DAILY RECORDING: STOP";
            this.Opacity = 1;
            button2.Enabled = false;
            button1.Enabled = true;
            timer1.Enabled = false;
            dt = DateTime.Now;
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss") + ",DISABLE,recording disabled" + "\n");
            logfile.Flush();
            Process.Start("explorer.exe", System.Environment.CurrentDirectory);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dt = DateTime.Now;
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss") + ",END,program terminated" + "\n");
            logfile.Flush();
            logfile.Close();
        }


        //shortcut to C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp for autorunning
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if ( p.ProcessName == "daily_recording" && p.Id!= Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("程序已经打开！");
                    Process.GetCurrentProcess().Kill();
                }
            }

            dt = DateTime.Now;
            logfile = new StreamWriter("recording.LOG", true, System.Text.Encoding.Default);
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss") + ",START,program launched" + "\n");//hh:12,HH:24
            logfile.Flush();

            resetjijincode();

            //获取当前文件夹路径
            string currPath = Application.StartupPath;
            //检查是否存在文件夹
            string subPath = currPath + "/pic/";
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(subPath);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Opacity = 0;

            //button1.PerformClick();

            timer1.Enabled = true;
            dt = DateTime.Now;
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss") + ",ENABLE,recording enabled" + "\n");//hh:12,HH:24
            logfile.Flush();

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
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss") + ",ENABLE,recording enabled" + "\n");//hh:12,HH:24
            logfile.Flush();

            sTARTRECORDINGToolStripMenuItem.Enabled = false;
            sTOPRECORDINGToolStripMenuItem.Enabled = true;
            notifyIcon1.Icon = daily_recording.Properties.Resources.pic1;
            notifyIcon1.ShowBalloonTip(1000, "notice", "recording started", ToolTipIcon.Info);
        }

        private void mANUALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetjijincode();
            getpictures();

        }

        private void sTOPRECORDINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //button2.PerformClick();

            timer1.Enabled = false;
            dt = DateTime.Now;
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss") + ",DISABLE,recording disabled" + "\n");
            logfile.Flush();
            notifyIcon1.Icon = daily_recording.Properties.Resources.pic2;
            //Process.Start("explorer.exe", System.Environment.CurrentDirectory);            

            sTARTRECORDINGToolStripMenuItem.Enabled = true;
            sTOPRECORDINGToolStripMenuItem.Enabled = false;
        }

        

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dt = DateTime.Now;
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss") + ",END,program terminated" + "\n");
            logfile.Flush();
            logfile.Close();
            //Process.Start("explorer.exe", System.Environment.CurrentDirectory);

            Process.GetCurrentProcess().Kill();
        }

        private void sHOWRECORDINGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", System.Environment.CurrentDirectory+ "\\pic\\");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            if ( (dt.DayOfWeek!= DayOfWeek.Saturday) && (dt.DayOfWeek != DayOfWeek.Sunday) &&
                ((dt.TimeOfDay<=starttime.TimeOfDay || dt.TimeOfDay >= endtime.TimeOfDay) ) )
            {
                //updata jijindate
                readresponse("006229");
                DateTime newgztime = new DateTime(
                    Convert.ToInt32(jijindate.Substring(0, 4)),
                    Convert.ToInt32(jijindate.Substring(5, 2)),
                    Convert.ToInt32(jijindate.Substring(8, 2)), 0, 10, 0);
                if (newgztime > reftime)
                {
                    getpictures();
                }
            }
            
            //else if(dt.TimeOfDay > endtime.TimeOfDay)
            //{
            //    logfile.Write("\n");
            //    logfile.Flush();
            //}


        }

        int readresponse(string jijincode)
        {
            HttpWebRequest req;
            HttpWebResponse response;
            Stream stream;
            req = (HttpWebRequest)HttpWebRequest.Create("http://fundgz.1234567.com.cn/js/" + jijincode + ".js?");
            //req.Method = "GET"; //default value
            //req.ProtocolVersion = new Version(1, 1); //default value
            req.Timeout = 1000;
            try 
            { 
                response = req.GetResponse() as HttpWebResponse; 
            }
            catch
            {
                req.Abort();
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
            if (response.ContentLength <= 50)
            {
                req.Abort();
                response.Close();
                return -1;
            }
                
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
            //int fundcodeidx;
            int nameidx;
            int jzrqidx;
            //int dwjzidx;
            //int gszidx;
            //int gszzlidx;
            int gztimeidx;
            string content;
            content = Encoding.UTF8.GetString(bytes);

            //:"2021-04-30","dwjz":"2.7621","gsz":"2.6768","gszzl":"-3.09","gztime":"2021-05-06 15:00"});

            //fundcodeidx = content.IndexOf("\"fundcode\":\"");
            nameidx = content.IndexOf("\"name\":\"");
            jzrqidx = content.IndexOf("\"jzrq\":\"");
            //dwjzidx = content.IndexOf("\"dwjz\":\"");
            //gszidx = content.IndexOf("\"gsz\":\"");
            //gszzlidx = content.IndexOf("\"gszzl\":\"");
            gztimeidx = content.IndexOf("\"gztime\":\"");


            try
            {
                jijinname = content.Substring(nameidx + 8, jzrqidx - nameidx - 10);
                jijindate = content.Substring(gztimeidx + 10, 10);
            }
            catch
            {
                return -1;
            }
            

            stream.Close();
            response.Close();
            req.Abort();

            return 0;
        }

        void resetjijincode()
        {
            try
            {
                configfile = new StreamReader("CODE.CFG");
                string line;
                int idx = 0;
                while ((line = configfile.ReadLine()) != null)
                {
                    jijincode[idx] = line;
                    idx++;
                }
                jijincode[idx] = "END";
                configfile.Close();
            }
            catch
            {
                MessageBox.Show("wrong CODE.CFG");
                Process.GetCurrentProcess().Kill();
            }
        }

        int getpictures()
        {
            string jjcodetemp = jijincode[0];
            int idx = 0;
            int errnum = 0;
            string errname="";
            for (; jjcodetemp != "END"; )
            {
                if(!downloadpic(jjcodetemp))
                {
                    errnum++;
                    errname = errname + jjcodetemp + ", ";
                }
                idx++;
                jjcodetemp = jijincode[idx];
            }
            dt = DateTime.Now;
            logfile.Write(dt.ToString("yyyyMMdd,") + dt.ToString("HH,mm,ss, ") + (idx-errnum).ToString() + " files got; errors: " + errname + ";\n");//hh:12,HH:24
            logfile.Flush();
            int a1 = Convert.ToInt32(jijindate.Substring(0, 4));
            int a2 = Convert.ToInt32(jijindate.Substring(5, 2));
            int a3 = Convert.ToInt32(jijindate.Substring(8, 2));
            reftime = new DateTime(a1, a2, a3, 0, 10, 0);
            return 0;
        }

        bool downloadpic(string jijincode)
        {
            if(readresponse(jijincode)==0)
            {                
                string picname = ".\\pic\\" + jijincode + "_" + jijindate + "_" + jijinname + ".png";
                string guzhiurl = "http://j4.dfcfw.com/charts/pic6/" + jijincode + ".png";
                if(HttpDownload(guzhiurl, picname))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

        bool HttpDownload(string url, string path)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                System.IO.File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
