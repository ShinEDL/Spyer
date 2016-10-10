namespace Spy.UI
{
    partial class FeedbackPage
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.button_add = new System.Windows.Forms.Button();
            this.button_de1 = new System.Windows.Forms.Button();
            this.button_de2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_de3 = new System.Windows.Forms.Button();
            this.button_de4 = new System.Windows.Forms.Button();
            this.buttonfast = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonsubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(33, 195);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(33, 26);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(496, 148);
            this.textBox1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(139, 195);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 76);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(245, 195);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 76);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(351, 195);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(100, 76);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(470, 317);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(75, 23);
            this.button_add.TabIndex = 5;
            this.button_add.Text = "增加图片";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_de1
            // 
            this.button_de1.Location = new System.Drawing.Point(45, 277);
            this.button_de1.Name = "button_de1";
            this.button_de1.Size = new System.Drawing.Size(75, 23);
            this.button_de1.TabIndex = 6;
            this.button_de1.Text = "删除";
            this.button_de1.UseVisualStyleBackColor = true;
            this.button_de1.Visible = false;
            this.button_de1.Click += new System.EventHandler(this.button_de1_Click);
            // 
            // button_de2
            // 
            this.button_de2.Location = new System.Drawing.Point(150, 277);
            this.button_de2.Name = "button_de2";
            this.button_de2.Size = new System.Drawing.Size(75, 23);
            this.button_de2.TabIndex = 7;
            this.button_de2.Text = "删除";
            this.button_de2.UseVisualStyleBackColor = true;
            this.button_de2.Visible = false;
            this.button_de2.Click += new System.EventHandler(this.button_de2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "JPG文件|*.jpg|PNG图片|*.png";
            // 
            // button_de3
            // 
            this.button_de3.Location = new System.Drawing.Point(259, 277);
            this.button_de3.Name = "button_de3";
            this.button_de3.Size = new System.Drawing.Size(75, 23);
            this.button_de3.TabIndex = 8;
            this.button_de3.Text = "删除";
            this.button_de3.UseVisualStyleBackColor = true;
            this.button_de3.Visible = false;
            this.button_de3.Click += new System.EventHandler(this.button_de3_Click);
            // 
            // button_de4
            // 
            this.button_de4.Location = new System.Drawing.Point(362, 277);
            this.button_de4.Name = "button_de4";
            this.button_de4.Size = new System.Drawing.Size(75, 23);
            this.button_de4.TabIndex = 9;
            this.button_de4.Text = "删除";
            this.button_de4.UseVisualStyleBackColor = true;
            this.button_de4.Visible = false;
            this.button_de4.Click += new System.EventHandler(this.button_de4_Click);
            // 
            // buttonfast
            // 
            this.buttonfast.Location = new System.Drawing.Point(470, 288);
            this.buttonfast.Name = "buttonfast";
            this.buttonfast.Size = new System.Drawing.Size(75, 23);
            this.buttonfast.TabIndex = 10;
            this.buttonfast.Text = "截屏上传";
            this.buttonfast.UseVisualStyleBackColor = true;
            this.buttonfast.Click += new System.EventHandler(this.buttonfast_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1200;
            this.timer1.Tick += new System.EventHandler(this.enban);
            // 
            // buttonsubmit
            // 
            this.buttonsubmit.Location = new System.Drawing.Point(470, 350);
            this.buttonsubmit.Name = "buttonsubmit";
            this.buttonsubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonsubmit.TabIndex = 11;
            this.buttonsubmit.Text = "提交";
            this.buttonsubmit.UseVisualStyleBackColor = true;
            this.buttonsubmit.Click += new System.EventHandler(this.buttonsubmit_Click);
            // 
            // FeedbackPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 385);
            this.Controls.Add(this.buttonsubmit);
            this.Controls.Add(this.buttonfast);
            this.Controls.Add(this.button_de4);
            this.Controls.Add(this.button_de3);
            this.Controls.Add(this.button_de2);
            this.Controls.Add(this.button_de1);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FeedbackPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FeedbackPage";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_de1;
        private System.Windows.Forms.Button button_de2;
        private System.Windows.Forms.Button button_de3;
        private System.Windows.Forms.Button button_de4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonfast;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonsubmit;
    }
}