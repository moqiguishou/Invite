using System;
using System.Windows.Forms;

namespace NoClose {
    partial class Form1 {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.btn_agree = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_disagree = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_agree
            // 
            this.btn_agree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_agree.Image = global::NoClose.Properties.Resources.agree_big;
            this.btn_agree.Location = new System.Drawing.Point(411, 399);
            this.btn_agree.Name = "btn_agree";
            this.btn_agree.Size = new System.Drawing.Size(100, 100);
            this.btn_agree.TabIndex = 0;
            this.btn_agree.UseVisualStyleBackColor = true;
            this.btn_agree.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1366, 768);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btn_disagree
            // 
            this.btn_disagree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_disagree.Image = global::NoClose.Properties.Resources.agree_big;
            this.btn_disagree.Location = new System.Drawing.Point(242, 399);
            this.btn_disagree.Name = "btn_disagree";
            this.btn_disagree.Size = new System.Drawing.Size(100, 100);
            this.btn_disagree.TabIndex = 9;
            this.btn_disagree.UseVisualStyleBackColor = true;
            this.btn_disagree.Click += new System.EventHandler(this.btn_disagree_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.btn_disagree);
            this.Controls.Add(this.btn_agree);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_agree;
        private PictureBox pictureBox1;
        private Button btn_disagree;
    }
}

