namespace Spy
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.工作任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反馈ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.截图保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.userName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Mac = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.IPadd = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerSocket = new System.Windows.Forms.Timer(this.components);
            this.timerprocess = new System.Windows.Forms.Timer(this.components);
            this.timerhttp = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工作任务ToolStripMenuItem,
            this.反馈ToolStripMenuItem,
            this.任务表ToolStripMenuItem,
            this.截图保存ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(465, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 工作任务ToolStripMenuItem
            // 
            this.工作任务ToolStripMenuItem.Name = "工作任务ToolStripMenuItem";
            this.工作任务ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工作任务ToolStripMenuItem.Text = "主页";
            this.工作任务ToolStripMenuItem.Click += new System.EventHandler(this.主页ToolStripMenuItem_Click);
            // 
            // 反馈ToolStripMenuItem
            // 
            this.反馈ToolStripMenuItem.Checked = true;
            this.反馈ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.反馈ToolStripMenuItem.Name = "反馈ToolStripMenuItem";
            this.反馈ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.反馈ToolStripMenuItem.Text = "反馈";
            this.反馈ToolStripMenuItem.Click += new System.EventHandler(this.反馈ToolStripMenuItem_Click);
            // 
            // 任务表ToolStripMenuItem
            // 
            this.任务表ToolStripMenuItem.Name = "任务表ToolStripMenuItem";
            this.任务表ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.任务表ToolStripMenuItem.Text = "推送消息记录";
            this.任务表ToolStripMenuItem.Click += new System.EventHandler(this.任务表ToolStripMenuItem_Click);
            // 
            // 截图保存ToolStripMenuItem
            // 
            this.截图保存ToolStripMenuItem.Name = "截图保存ToolStripMenuItem";
            this.截图保存ToolStripMenuItem.ShowShortcutKeys = false;
            this.截图保存ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.截图保存ToolStripMenuItem.Text = "截图保存";
            this.截图保存ToolStripMenuItem.Click += new System.EventHandler(this.截图保存ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.userName,
            this.toolStripStatusLabel2,
            this.Mac,
            this.toolStripStatusLabel3,
            this.IPadd});
            this.statusStrip1.Location = new System.Drawing.Point(0, 329);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(465, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "当前用户：";
            // 
            // userName
            // 
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(107, 17);
            this.userName.Text = "username111111";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel2.Text = "主机编号";
            // 
            // Mac
            // 
            this.Mac.Name = "Mac";
            this.Mac.Size = new System.Drawing.Size(81, 17);
            this.Mac.Text = " Mac           ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(19, 17);
            this.toolStripStatusLabel3.Text = "IP";
            // 
            // IPadd
            // 
            this.IPadd.Name = "IPadd";
            this.IPadd.Size = new System.Drawing.Size(47, 17);
            this.IPadd.Text = "Ip add";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 180000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerSocket
            // 
            this.timerSocket.Enabled = true;
            this.timerSocket.Tick += new System.EventHandler(this.timerSocket_Tick);
            // 
            // timerprocess
            // 
            this.timerprocess.Enabled = true;
            this.timerprocess.Interval = 5000;
            this.timerprocess.Tick += new System.EventHandler(this.timerprocss_Tick);
            // 
            // timerhttp
            // 
            this.timerhttp.Enabled = true;
            this.timerhttp.Interval = 30000;
            this.timerhttp.Tick += new System.EventHandler(this.timerhttp_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "监控软件";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 351);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel userName;
        private System.Windows.Forms.ToolStripMenuItem 工作任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反馈ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任务表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 截图保存ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel Mac;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel IPadd;
        private System.Windows.Forms.Timer timerSocket;
        private System.Windows.Forms.Timer timerprocess;
        private System.Windows.Forms.Timer timerhttp;
        private System.Windows.Forms.NotifyIcon notifyIcon1;

        
    }
}