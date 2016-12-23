namespace daily_recording
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sHOWFORMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTARTRECORDINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sTOPRECORDINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sHOWRECORDINGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "ENABLE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(12, 51);
            this.button2.MaximumSize = new System.Drawing.Size(219, 23);
            this.button2.MinimumSize = new System.Drawing.Size(219, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(219, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "DISABLE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sHOWFORMToolStripMenuItem,
            this.sTARTRECORDINGToolStripMenuItem,
            this.sHOWRECORDINGSToolStripMenuItem,
            this.sTOPRECORDINGToolStripMenuItem,
            this.eXITToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(199, 136);
            // 
            // sHOWFORMToolStripMenuItem
            // 
            this.sHOWFORMToolStripMenuItem.Name = "sHOWFORMToolStripMenuItem";
            this.sHOWFORMToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sHOWFORMToolStripMenuItem.Text = "SHOW FORM";
            this.sHOWFORMToolStripMenuItem.Click += new System.EventHandler(this.sHOWFORMToolStripMenuItem_Click);
            // 
            // sTARTRECORDINGToolStripMenuItem
            // 
            this.sTARTRECORDINGToolStripMenuItem.Name = "sTARTRECORDINGToolStripMenuItem";
            this.sTARTRECORDINGToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sTARTRECORDINGToolStripMenuItem.Text = "START RECORDING";
            this.sTARTRECORDINGToolStripMenuItem.Click += new System.EventHandler(this.sTARTRECORDINGToolStripMenuItem_Click);
            // 
            // sTOPRECORDINGToolStripMenuItem
            // 
            this.sTOPRECORDINGToolStripMenuItem.Name = "sTOPRECORDINGToolStripMenuItem";
            this.sTOPRECORDINGToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sTOPRECORDINGToolStripMenuItem.Text = "STOP RECORDING";
            this.sTOPRECORDINGToolStripMenuItem.Click += new System.EventHandler(this.sTOPRECORDINGToolStripMenuItem_Click);
            // 
            // eXITToolStripMenuItem
            // 
            this.eXITToolStripMenuItem.Name = "eXITToolStripMenuItem";
            this.eXITToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.eXITToolStripMenuItem.Text = "EXIT";
            this.eXITToolStripMenuItem.Click += new System.EventHandler(this.eXITToolStripMenuItem_Click);
            // 
            // sHOWRECORDINGSToolStripMenuItem
            // 
            this.sHOWRECORDINGSToolStripMenuItem.Name = "sHOWRECORDINGSToolStripMenuItem";
            this.sHOWRECORDINGSToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sHOWRECORDINGSToolStripMenuItem.Text = "SHOW RECORDINGS";
            this.sHOWRECORDINGSToolStripMenuItem.Click += new System.EventHandler(this.sHOWRECORDINGSToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 91);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(259, 130);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(259, 130);
            this.Name = "Form1";
            this.Opacity = 0D;
            this.Text = "DAILY RECORDING: STOP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sHOWFORMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sTARTRECORDINGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sTOPRECORDINGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sHOWRECORDINGSToolStripMenuItem;
    }
}

