using System;
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
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }

        }
        //控制
        private bool isOver=false;
        private bool isALi=false;
        //范围
        private RectangleF r_bg = new RectangleF(0, 0, 1366f, 768f);
        private RectangleF r_agree = new RectangleF(0, 0, 100, 100);
        private RectangleF r_disagree = new RectangleF(150, 0, 100, 100);
        private RectangleF r_ALi = new RectangleF(1015, 158, 200, 280);

        private int[] zi_x= {1000,1050,1100,1150,1030,1080,1070,1120};
        private int[] zi_y= { 200, 210, 220, 230, 265, 275, 325,335 };
        private int[] zi_width;
        private int[] zi_hight;
        private RectangleF r_Zi = new RectangleF(1015, 158, 50, 50);



        //绘图变
        private Graphics g;
        private Graphics g2;
        private Image im_bg;
        private Image im_bg1;
        private Image im_bgWin;
        private Image im_agree;
        private Image im_disagree;
        private Image im_ALi;
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
            //SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            //监听
            FormClosing += new FormClosingEventHandler(form1_FormClosed);
            //Paint += new PaintEventHandler(Form1_Paint);
            //Paint += new PaintEventHandler(Form1_Paint2);
            MouseDown += new MouseEventHandler(Form1_MouseDown);
            MouseUp += new MouseEventHandler(Form1_MouseUp);
            MouseMove += new MouseEventHandler(Form1_MouseMove);

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
                Arguments = @"F:\邀请函\Invite\NoClose\reStart.vbs"
            };
            Process.Start(startInfo);
        }
        //备用
        //private void button1_Click(object sender, EventArgs e) {
        //    //Form1_Paint();

        //}
        public void DrawLine() {
            Graphics gs = pictureBox1.CreateGraphics();
            SolidBrush brush_1 = new SolidBrush(Color.Red);
            Pen pen1 = new Pen(brush_1, 10);
            gs.DrawLine(pen1, new Point(10, 50), new Point(50, 50));
            im_disagree = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\agree_big.png");
            while (true) {
                while (isALi) {
                    Thread.Sleep(1000);
                    while (true) {
                        gs.DrawImage(im_disagree, r_disagree);
                    }
                }
            }
        }
        public void DrawLine1() {
            Graphics gs = pictureBox1.CreateGraphics();
            SolidBrush brush_1 = new SolidBrush(Color.Red);
            Pen pen1 = new Pen(brush_1, 10);
            gs.DrawLine(pen1, new Point(50, 50), new Point(100, 100));
            im_agree = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\agree_big.png");
            
            while (true) {
                gs.DrawImage(im_agree, r_agree);
                
            }
        }
        private void button1_Click(object sender, EventArgs e) {
            //Thread thread = new Thread(new ThreadStart(DrawLine));
            //thread.Start();
            Thread thread1 = new Thread(new ThreadStart(DrawLine1));
            thread1.Start();
            Thread thread = new Thread(new ThreadStart(DrawLine));
            thread.Start();
            isALi = true;

        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
        //private void Form1_Paint() {
            
            g2 = this.CreateGraphics();
            im_bg = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\bg\background2.png");
            im_bg1 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\bg\background1.jpg");
            im_bgWin = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\bg\bg_window.png");
            im_agree = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\agree_big.png");
            im_disagree = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\agree_big.png");

            im_ALi = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\ALi.png");

            im_zi_1 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_1.png");
            im_zi_2 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_2.png");
            im_zi_3 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_3.png");
            im_zi_4 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_4.png");
            im_zi_5 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_5.png");
            im_zi_6 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_6.png");
            im_zi_7 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_7.png");
            im_zi_8 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_8.png");
            //2
            im_zi_11 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_11.png");
            im_zi_12 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_12.png");
            im_zi_13 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_13.png");
            im_zi_14 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_14.png");
            im_zi_15 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_15.png");
            im_zi_16 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_16.png");
            im_zi_17 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_17.png");
            im_zi_18 = Image.FromFile(@"F:\邀请函\Invite\NoClose\NoClose\Properties\zi\zi_18.png");

            //g.DrawImage(im_ALi, r_ALi);
            g2.DrawImage(im_bg1, r_bg);
            g2.DrawImage(im_bg, r_bg);
            g2.Dispose();

            Delay(10);
            
            g = this.CreateGraphics();
            g.DrawImage(im_agree, r_agree);
            g.Dispose();
            //Thread cg = new Thread(new ThreadStart(PlayCg));
            //cg.Start();
        }

        public static void Delay(int mm) {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now) {
                Application.DoEvents();
            }
            return;
        }
        int zi_time = 50;
        private void PlayCg() {
            g = this.CreateGraphics();
            g.DrawImage(im_zi_1, new Rectangle(zi_x[0], zi_y[0], 50, 50));
            g.DrawImage(im_zi_2, new Rectangle(zi_x[1], zi_y[1], 50, 50));
            g.DrawImage(im_zi_3, new Rectangle(zi_x[2], zi_y[2], 50, 50));
            g.DrawImage(im_zi_4, new Rectangle(zi_x[3], zi_y[3], 50, 50));
            g.DrawImage(im_zi_5, new Rectangle(zi_x[4], zi_y[4], 50, 50));
            g.DrawImage(im_zi_6, new Rectangle(zi_x[5], zi_y[5], 50, 50));
            g.DrawImage(im_zi_7, new Rectangle(zi_x[6], zi_y[6], 50, 50));
            g.DrawImage(im_zi_8, new Rectangle(zi_x[7], zi_y[7], 50, 50));

            //g.DrawImage(im_bgWin,r_bg);
            //g.DrawImage(im_zi_11, new Rectangle(zi_x[0], zi_y[0], 50, 50));
            //Thread.Sleep(zi_time);
            //g.DrawImage(im_zi_12, new Rectangle(zi_x[1], zi_y[1], 50, 50));
            //Thread.Sleep(zi_time);
            //g.DrawImage(im_zi_13, new Rectangle(zi_x[2], zi_y[2], 50, 50));
            //Thread.Sleep(zi_time);
            //g.DrawImage(im_zi_14, new Rectangle(zi_x[3], zi_y[3], 50, 50));
            //Thread.Sleep(zi_time);
            //g.DrawImage(im_zi_15, new Rectangle(zi_x[4], zi_y[4], 50, 50));
            //Thread.Sleep(zi_time);
            //g.DrawImage(im_zi_16, new Rectangle(zi_x[5], zi_y[5], 50, 50));
            //Thread.Sleep(zi_time);
            //g.DrawImage(im_zi_17, new Rectangle(zi_x[6], zi_y[6], 50, 50));
            //Thread.Sleep(zi_time);
            //g.DrawImage(im_zi_18, new Rectangle(zi_x[7], zi_y[7], 50, 50));
            //Thread.Sleep(1000);
            //g.DrawImage(im_bgWin, r_bg);
            //g.DrawImage(im_ALi, r_ALi);
            g.Dispose();
        }

        bool isMove = false;
        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            isMove = true;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            isMove = false;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            if (isMove) {
                r_Zi = new RectangleF(e.X, e.Y, 50, 50);
            }
            button1.Text = e.X+","+e.Y;
            //r2 = new RectangleF(e.X,e.Y,100,100);
        }
    }
}
