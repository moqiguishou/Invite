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
            this.btn_disagree = new System.Windows.Forms.Button();
            this.btn_agree = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_PM = new System.Windows.Forms.TextBox();
            this.lab_mes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_disagree
            // 
            this.btn_disagree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_disagree.Location = new System.Drawing.Point(242, 399);
            this.btn_disagree.Name = "btn_disagree";
            this.btn_disagree.Size = new System.Drawing.Size(100, 100);
            this.btn_disagree.TabIndex = 9;
            this.btn_disagree.UseVisualStyleBackColor = true;
            this.btn_disagree.Click += new System.EventHandler(this.btn_disagree_Click);
            // 
            // btn_agree
            // 
            this.btn_agree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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
            // txt_PM
            // 
            this.txt_PM.Location = new System.Drawing.Point(295, 463);
            this.txt_PM.Name = "txt_PM";
            this.txt_PM.Size = new System.Drawing.Size(447, 21);
            this.txt_PM.TabIndex = 10;
            this.txt_PM.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lab_mes
            // 
            this.lab_mes.AutoSize = true;
            this.lab_mes.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_mes.Location = new System.Drawing.Point(381, 52);
            this.lab_mes.Name = "lab_mes";
            this.lab_mes.Size = new System.Drawing.Size(0, 19);
            this.lab_mes.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.lab_mes);
            this.Controls.Add(this.btn_disagree);
            this.Controls.Add(this.txt_PM);
            this.Controls.Add(this.btn_agree);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_agree;
        private PictureBox pictureBox1;
        private Button btn_disagree;
        private TextBox txt_PM;
        private Label lab_mes;
    }
}

