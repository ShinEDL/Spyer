namespace Spy.UI
{
    partial class Network
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
            this.ListAdapters = new System.Windows.Forms.ListBox();
            this.label_uptext = new System.Windows.Forms.Label();
            this.label_downtext = new System.Windows.Forms.Label();
            this.label_upload = new System.Windows.Forms.Label();
            this.label_download = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_totalup = new System.Windows.Forms.Label();
            this.label_totaldown = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListAdapters
            // 
            this.ListAdapters.FormattingEnabled = true;
            this.ListAdapters.ItemHeight = 12;
            this.ListAdapters.Location = new System.Drawing.Point(12, 12);
            this.ListAdapters.Name = "ListAdapters";
            this.ListAdapters.Size = new System.Drawing.Size(441, 124);
            this.ListAdapters.TabIndex = 0;
            this.ListAdapters.SelectedIndexChanged += new System.EventHandler(this.ListAdapters_SelectedIndexChanged);
            // 
            // label_uptext
            // 
            this.label_uptext.AutoSize = true;
            this.label_uptext.Location = new System.Drawing.Point(70, 175);
            this.label_uptext.Name = "label_uptext";
            this.label_uptext.Size = new System.Drawing.Size(59, 12);
            this.label_uptext.TabIndex = 1;
            this.label_uptext.Text = "上传速度:";
            this.label_uptext.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_downtext
            // 
            this.label_downtext.AutoSize = true;
            this.label_downtext.Location = new System.Drawing.Point(70, 233);
            this.label_downtext.Name = "label_downtext";
            this.label_downtext.Size = new System.Drawing.Size(59, 12);
            this.label_downtext.TabIndex = 2;
            this.label_downtext.Text = "下载速度:";
            // 
            // label_upload
            // 
            this.label_upload.Location = new System.Drawing.Point(135, 175);
            this.label_upload.Name = "label_upload";
            this.label_upload.Size = new System.Drawing.Size(103, 23);
            this.label_upload.TabIndex = 3;
            // 
            // label_download
            // 
            this.label_download.Location = new System.Drawing.Point(138, 233);
            this.label_download.Name = "label_download";
            this.label_download.Size = new System.Drawing.Size(100, 23);
            this.label_download.TabIndex = 4;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.TimerCounter_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "上传合计";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "下载合计";
            // 
            // label_totalup
            // 
            this.label_totalup.Location = new System.Drawing.Point(303, 175);
            this.label_totalup.Name = "label_totalup";
            this.label_totalup.Size = new System.Drawing.Size(100, 23);
            this.label_totalup.TabIndex = 7;
            // 
            // label_totaldown
            // 
            this.label_totaldown.Location = new System.Drawing.Point(303, 233);
            this.label_totaldown.Name = "label_totaldown";
            this.label_totaldown.Size = new System.Drawing.Size(100, 23);
            this.label_totaldown.TabIndex = 8;
            // 
            // Network
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(465, 362);
            this.Controls.Add(this.label_totaldown);
            this.Controls.Add(this.label_totalup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_download);
            this.Controls.Add(this.label_upload);
            this.Controls.Add(this.label_downtext);
            this.Controls.Add(this.label_uptext);
            this.Controls.Add(this.ListAdapters);
            this.Name = "Network";
            this.Text = "Network";
            this.Load += new System.EventHandler(this.Network_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_uptext;
        private System.Windows.Forms.Label label_downtext;
        private System.Windows.Forms.Label label_upload;
        private System.Windows.Forms.Label label_download;
        public System.Windows.Forms.ListBox ListAdapters;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_totalup;
        private System.Windows.Forms.Label label_totaldown;
    }
}