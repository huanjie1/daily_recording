using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace daily_recording
{
    public partial class Form1 : Form
    {
        DateTime dt = new DateTime();
        string filename;
        string windowdescription,windowname;
        StreamWriter recordfilesw;
        IntPtr windowhandle;
        StringBuilder windowname0 = new StringBuilder(250);
        int maxnamelength = 250;
        int namelength;
        int threadid;
        int processid;
        Point mouse = new Point(0, 0);
        Point mouse0 = new Point(0, 0);
        int mousestop;
        string mousestate;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        //根据窗体标题查找窗口句柄（支持模糊匹配）
        public static IntPtr FindWindow(string title)
        {
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (p.MainWindowTitle.IndexOf(title) != -1)
                {
                    return p.MainWindowHandle;
                }
            }
            return IntPtr.Zero;
        }

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
                    Process.GetCurrentProcess().Kill();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            windowhandle = GetForegroundWindow();
            namelength = GetWindowText(windowhandle, windowname0, maxnamelength);
            threadid = GetWindowThreadProcessId(windowhandle, out processid);//引用输出为进程ID，返回值为线程ID
            try
            {
                windowdescription = Process.GetProcessById(processid).MainModule.FileVersionInfo.FileDescription.ToString();
            }
            catch { }
            windowname = windowname0.ToString().Replace(',',' ');

            mouse = Control.MousePosition;           
            
            if (mouse == mouse0)
                mousestop++;
            else
                mousestop = 0;

            if (mousestop >= 5)
                mousestate = "stop";
            else
                mousestate = "move";

            mouse0 = mouse;

            dt = DateTime.Now;
            recordfilesw.Write(
                dt.ToString("HH,mm,ss") + "," +
                windowname + "," +
                windowdescription + "," +
                mousestate + "\n"
                );
            recordfilesw.Flush();
        }
    }
}
