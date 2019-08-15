﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Threading;

namespace NoClose {
    public partial class Form1 : Form {
        /// <summary>
        /// 很关键
        /// </summary>
        //protected override CreateParams CreateParams {
        //    get {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }

        //}
        //控制
        private bool isOver=false;  //重启
        private bool isZi=true;     //cg字
        private bool isALi=false;   //cg阿狸
        private bool isBegin=false; //第一次点击
        private int time_zi=100;

        private string[] exitPM = {
            "Nizw",
            "Nzw",
            "NiZuWei",
            "倪祖伟",
            "moqi",
            "azu",
        };
        //范围
        private RectangleF r_bg = new RectangleF(0, 0, 1366f, 768f);
        private RectangleF r_agree = new RectangleF(0, 0, 100, 100);
        private RectangleF r_disagree = new RectangleF(150, 0, 100, 100);
        private RectangleF r_ALi = new RectangleF(1015, 158, 200, 280);
        private RectangleF r_ALi_chat = new RectangleF(700, 180, 350, 100);
        private RectangleF r_Tao_chat = new RectangleF(141, 235, 500, 200);

        private int[] zi_x= {1000,1050,1100,1150,1030,1080,1070,1120};
        private int[] zi_y= { 200, 210, 220, 230, 265, 275, 325,335 };
        private bool[] b_zi = {false, false, false, false, false, false, false, false };
        private int[] w= {0,0,0,0,0,0,0,0 };
        private int[] h= { 0, 0, 0, 0, 0, 0, 0, 0 };
        private RectangleF r_Zi = new RectangleF(1015, 158, 50, 50);


        private Dictionary<string, Image> MyImage = new Dictionary<string, Image>();
        private Dictionary<string, RectangleF> MyRecet = new Dictionary<string, RectangleF>();



        //绘图变
        private Graphics g;
        private Graphics g2;
        private Image im_bg;
        private Image im_bg1;
        private Image im_bgWin;
        private Image im_agree;
        private Image im_disagree;
        private Image im_ALi;
        private Image im_ALi_chat0;
        private Image im_ALi_chat1;
        private Image im_ALi_chatno;
        private Image im_Taozi_chatno;
        private Image im_Taozi_chatxing;


        private Image im_zi_0;
        private Image im_zi_1;
        private Image im_zi_2;
        private Image im_zi_3;
        private Image im_zi_4;
        private Image im_zi_5;
        private Image im_zi_6;
        private Image im_zi_7;
        private Image im_zi_8;
        //2
        private Image im_zi_11;
        private Image im_zi_12;
        private Image im_zi_13;
        private Image im_zi_14;
        private Image im_zi_15;
        private Image im_zi_16;
        private Image im_zi_17;
        private Image im_zi_18;
        //private List<Image> im_zi;

        public Form1() {
            InitializeComponent();
            //采用双缓冲技术的控件必需的设置
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            //监听
            FormClosing += new FormClosingEventHandler(form1_FormClosed);
            //Paint += new PaintEventHandler(Form1_Paint);
            //Paint += new PaintEventHandler(Form1_Paint2);
            MouseDown += new MouseEventHandler(Form1_MouseDown);
            MouseUp += new MouseEventHandler(Form1_MouseUp);
            //MouseMove += new MouseEventHandler(Form1_MouseMove);
            pictureBox1.MouseMove+= new MouseEventHandler(Form1_MouseMove);
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            btn_disagree.MouseMove+=new MouseEventHandler(BtnDisagree_MouseMove);
            //pictureBox1.PreviewKeyDown += new KeyEventHandler(Form1_KeyDown);
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            //pictureBox1.Image= Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\bg\background1.jpg");
            Init_Image();

            
        }

        private void Init_Image() {
            im_bg = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\bg\background2.png");
            im_bg1 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\bg\background1.jpg");
            im_bgWin = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\bg\bg_window.png");
            im_agree = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\agree_big.png");
            im_disagree = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\agree_big.png");

            im_ALi = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\ALi.png");
            im_ALi_chat0 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\chat\chat_ali0.png");
            im_ALi_chat1 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\chat\chat_ali_2.png");
            im_ALi_chatno = im_ALi_chat0;// Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\chat\chat_ali_no.png");
            im_Taozi_chatno = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\chat\chat_tao_no.png");
            im_Taozi_chatxing = im_ALi_chat0;// Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\chat\chat_tao_xing.png");

            im_zi_0 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_0.png");
            im_zi_1 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_0.png");
            im_zi_2 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_0.png");
            im_zi_3 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_0.png");
            im_zi_4 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_4.png");
            im_zi_5 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_5.png");
            im_zi_6 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_6.png");
            im_zi_7 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_7.png");
            im_zi_8 = im_zi_0;//mage.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_8.png");
            //2       im_zi_0;//
            im_zi_11 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_11.png");
            im_zi_12 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_12.png");
            im_zi_13 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_13.png");
            im_zi_14 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_14.png");
            im_zi_15 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_15.png");
            im_zi_16 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_16.png");
            im_zi_17 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_17.png");
            im_zi_18 =im_zi_0;//Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_18.png");
        }

        private void Form1_Paint2(object sender, PaintEventArgs e) {
            

            
            //PlayCg();
        }

        //无用
        //protected override void OnPaint(PaintEventArgs e) {
        //    base.OnPaint(e);
        //    Graphics g = e.Graphics;
        //    g.FillRectangle(Brushes.Black, 0, 0, 200, 200);
        //}

        private void form1_FormClosed(object sender, FormClosingEventArgs e) {
            if (!isOver) {
                reStart();
            }
        }
        //无限循环
        private void reStart() {
            ProcessStartInfo startInfo = new ProcessStartInfo {
                FileName = "wscript.exe",
                //Arguments = @"..\reStart.vbs"
                Arguments = @"E:\邀请函\Invite\NoClose\reStart.vbs"
            };
            Process.Start(startInfo);
        }
        //备用
        //private void button1_Click(object sender, EventArgs e) {
        //    //Form1_Paint();

        //}
        public void DrawControl() {
            Thread.Sleep(100);
            b_zi[0] = true;
            Thread.Sleep(100);
            b_zi[1] = true;
            Thread.Sleep(100);
            b_zi[2] = true;
        }
        Graphics gs;// = CreateGraphics();
        public void Draw1() {
            gs = pictureBox1.CreateGraphics();
            while (true) {
                while (isZi) {
                    //背景
                    gs.DrawImage(im_zi_1, new Rectangle(zi_x[0], zi_y[0], w[0], h[0]));
                    gs.DrawImage(im_zi_2, new Rectangle(zi_x[1], zi_y[1], w[1], h[1]));
                    gs.DrawImage(im_zi_3, new Rectangle(zi_x[2], zi_y[2], w[2], h[2]));
                    gs.DrawImage(im_zi_4, new Rectangle(zi_x[3], zi_y[3], w[3], h[3]));
                    gs.DrawImage(im_zi_5, new Rectangle(zi_x[4], zi_y[4], w[4], h[4]));
                    gs.DrawImage(im_zi_6, new Rectangle(zi_x[5], zi_y[5], w[5], h[5]));
                    gs.DrawImage(im_zi_7, new Rectangle(zi_x[6], zi_y[6], w[6], h[6]));
                    gs.DrawImage(im_zi_8, new Rectangle(zi_x[7], zi_y[7], w[7], h[7]));
                }
                while (isALi) {
                    gs.DrawImage(im_ALi, r_ALi);
                    //gs.DrawImage(im_ALi_chat1, r_ALi_chat);
                    gs.DrawImage(im_ALi_chatno, r_ALi_chat);
                    gs.DrawImage(im_Taozi_chatxing, r_Tao_chat);
                }
            }
            
        }
        public void Draw2() {
            DateTime current = DateTime.Now;
            im_zi_1 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_1.png");
            while (current.AddMilliseconds(time_zi) > DateTime.Now) {
                if (w[0] < 50) {
                    Thread.Sleep(1);
                    w[0] = w[0] + 1;
                    h[0] = h[0] + 1;
                }
            }
            //Thread.Sleep(time_zi);
            
            //Thread.Sleep(time_zi);
            im_zi_2 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_2.png");
            while (current.AddMilliseconds(time_zi*2) > DateTime.Now) {
                if (w[1] < 50) {
                    Thread.Sleep(1);
                    w[1] = w[1] + 1;
                    h[1] = h[1] + 1;
                }
            }
            im_zi_3 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_3.png");
            while (current.AddMilliseconds(time_zi * 3) > DateTime.Now) {
                if (w[2] < 50) {
                    Thread.Sleep(1);
                    w[2] = w[2] + 1;
                    h[2] = h[2] + 1;
                }
            }
            im_zi_4 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_4.png");
            while (current.AddMilliseconds(time_zi * 4) > DateTime.Now) {
                if (w[3] < 50) {
                    Thread.Sleep(1);
                    w[3] = w[3] + 1;
                    h[3] = h[3] + 1;
                }
            }
            w[4] = 50;
            h[4] = 50;
            im_zi_5 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_5.png");
            //while (current.AddMilliseconds(time_zi * 5) > DateTime.Now) {
            //    if (w[4] < 50) {
            //        Thread.Sleep(1);
            //        w[4] = w[4] + 1;
            //        h[4] = h[4] + 1;
            //    }
            //}
            Thread.Sleep(time_zi);
            im_zi_6 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_6.png");
            while (current.AddMilliseconds(time_zi * 6) > DateTime.Now) {
                if (w[5] < 50) {
                    //Thread.Sleep(1);
                    //w[5] = w[2] + 1;
                    //h[5] = h[2] + 1;
                    w[5] = 50;
                    h[5] = 50;
                }
            }
            im_zi_7 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_7.png");
            im_zi_8 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_8.png");
            while (current.AddMilliseconds(time_zi * 7) > DateTime.Now) {
                if (w[6] < 50) {
                    //Thread.Sleep(1);
                    w[6] = 50;//6] + 1;
                    h[6] = 50;//h[6] + 1;
                    w[7] = 50;//6] + 1;
                    h[7] = 50;//h[6] + 1;
                }
            }
            im_zi_1 = im_zi_0;
            im_zi_2 = im_zi_0;
            im_zi_3 = im_zi_0;
            im_zi_4 = im_zi_0;
            im_zi_5 = im_zi_0;
            im_zi_6 = im_zi_0;
            im_zi_7 = im_zi_0;
            im_zi_8 = im_zi_0;
            isZi = false;
            Thread.Sleep(1000);
            
            pictureBox1.Image= Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\bg\background1.jpg");
            gs.Clear(this.BackColor);
            //gs.Dispose();
            //gs = pictureBox1.CreateGraphics();
            isZi = true;
            Thread.Sleep(time_zi);
            im_zi_1 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_11.png");
            Thread.Sleep(time_zi);
            im_zi_2 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_12.png");
            Thread.Sleep(time_zi);
            im_zi_3 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_13.png");
            Thread.Sleep(time_zi);
            im_zi_4 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_14.png");
            Thread.Sleep(time_zi);
            im_zi_5 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_15.png");
            Thread.Sleep(time_zi);
            im_zi_6 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_16.png");
            Thread.Sleep(time_zi);
            im_zi_7 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_17.png");
            Thread.Sleep(time_zi);
            im_zi_8 = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_18.png");
            isZi = false;
            Thread.Sleep(1000);
            pictureBox1.Image = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\bg\background1.jpg");
            gs.Clear(this.BackColor);
            isALi = true;
            Thread.Sleep(500);
            im_ALi_chatno = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\chat\chat_ali_no.png");
            Thread.Sleep(3000);
            im_Taozi_chatxing = Image.FromFile(@"E:\邀请函\Invite\NoClose\NoClose\Properties\chat\chat_tao_xing.png");
            Thread.Sleep(1000);
        }

        private void button1_Click(object sender, EventArgs e) {

            //txt_PM.Text = "enter222";
        }


        public static void Delay(int mm) {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now) {
                Application.DoEvents();
            }
            return;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) {

        }
        private void Form1_MouseUp(object sender, MouseEventArgs e) {

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            if (true) {
                //r_Tao_chat = new RectangleF(e.X, e.Y, 300,100);
            }
            btn_agree.Text = e.X+","+e.Y;
        }
        private void BtnDisagree_MouseMove(object sender, MouseEventArgs e) {
            Random random = new Random();
            int x = random.Next(1, 1266);
            int y = random.Next(1, 668);
            btn_disagree.Location = new Point(x, y);
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            //if (!isBegin) {
            //    isBegin = true;
            //    Thread t_Draw1 = new Thread(new ThreadStart(Draw1));
            //    t_Draw1.Start();
            //    Thread t_Draw2 = new Thread(new ThreadStart(Draw2));
            //    t_Draw2.Start();
            //}
            
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (!isBegin) {
                isBegin = true;
                //Thread t_Draw1 = new Thread(new ThreadStart(Draw1));
                //t_Draw1.Start();
                //Thread t_Draw2 = new Thread(new ThreadStart(Draw2));
                //t_Draw2.Start();
            }
            if (isBegin&&e.X >= 1015 && e.X <= 1215 && e.Y >= 158 && e.Y <= 438) {
                //点中Ali
                btn_disagree.Text = "ALi";
            }
        }

        private void btn_disagree_Click(object sender, EventArgs e) {
            btn_disagree.Text = "zhong";
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        //回车键
        private void Form1_KeyDown(object sender, KeyEventArgs e){

            if (e.KeyCode == Keys.Enter){
                string strPM = txt_PM.Text.ToLower();
                for (int i = 0; i < exitPM.Length; i++) {
                    if (exitPM[i].ToLower().Equals(strPM)) {
                        lab_mes.Text = "saydysdfbashfioajsfiojasdofijdsofndonfoidnsofinsdofjsodjfiosdjfoisdjfnoisdnfu";
                        isOver = true;
                    }
                }
            }
        }
    }
}
