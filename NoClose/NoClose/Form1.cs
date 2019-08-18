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
using System.Net;
using System.Net.Sockets;

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
        //不让退出FormClosing += new FormClosingEventHandler(form1_FormClosed);       //重启监听;//524行
        //不让点不同意监听，btn_disagree.MouseMove+=new MouseEventHandler(BtnDisagree_MouseMove);//行
        //}
        #region 变量
        //控制
        private bool isOver = false;  //重启
        private bool isConnectSucced = false;
        private bool isTishi = false;//是否提示主动弹出PM框
        private bool isAgree = false;  //是否答应
        private bool isPlayagreecg = false;  //是否答应
        private bool isCGOver = false;  //cg结束
        private bool isBegin = false; //第一次点击
        private bool isFirst = true; //第一次进入
        private bool isDisRun = false; //不同意进入逃跑状态
        private bool isShowMes = false; //显示提示
        private int speed = 20;//不同意逃跑速度
        private int nLastTime = 0;
        private int nNowTime = 0;
        private bool isExit = false;
        private bool[] exitbools = {false,false,false,false,false};
        private int numIndex = 5;


        //PM框 控制
        private bool hasText = false;
        private string tishi = "你的名字";
        //阶段
        private int JieDuan = 0;

        private string[] exitPM = {
            "Nizw",
            "Nzw",
            "NiZuWei",
            "倪祖伟",
            "moqi",
            "azu",
            "ani",
            "sumi",
            "sly",
            "苏亚玲",
            "suyaling",
        };
        private string[] exit2PM = {
            "shutdown",
            "tuichu",
            "exit",
            "e",
        };
        //范围
        private RectangleF bg = new RectangleF(0, 0, 1366f, 768f);
        //private RectangleF r_agree = new RectangleF(1015, 158, 100, 100);
        //private RectangleF r_disagree = new RectangleF(0, 0, 100, 100);

        private int[] zi_x = { 1000, 1050, 1100, 1150, 1030, 1080, 1070, 1120 };
        private int[] zi_y = { 200, 210, 220, 230, 265, 275, 325, 335 };
        //private bool[] b_zi = {false, false, false, false, false, false, false, false };
        private int[] w = { 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] h = { 0, 0, 0, 0, 0, 0, 0, 0 };



        //绘图变
        private Graphics gs;
        private Image im_bgagree;
        private Image[] BgDraws = new Image[25];
        private Image[] im_disagreeL = new Image[5];
        private Image[] im_disagreeR = new Image[5];
        private Image[] im_Agree = new Image[19];

        //路径
        private string Path_Ini = "../../Ini.ini";
        private string Path_Project = "";
        private string Path_Client = "";
        private string Path_VBS_restart = "";
        //文字
        private string[] donotit = {
            "直接退出是不行的",
            "你可以问问阿狸怎么退出",
            "如果你知道特殊的命令，那你就能直接退出去了",
            "好期待~",
        };
        //Client
        private bool isClientFlash = false;
        private IPEndPoint ipe;
        private Socket client_socket;
        private string resiveMesg = "";
        #endregion
        public Form1() {
            InitializeComponent();
            ClientInit();
            PathInit();
            Init_Image();
            TimeCheck();

            //txt初始
            txt_PM.Hide();
            txt_PM.Text = "你的名字";
            txt_PM.ForeColor = Color.LightGray;
            //监听
            FormClosing += new FormClosingEventHandler(form1_FormClosed);       //重启监听
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);//鼠标移动
            btn_agree.MouseUp += new MouseEventHandler(Btnagree_MouseUp);
            btn_agree.MouseDown += new MouseEventHandler(Btnagree_MouseDown);
            btn_disagree.MouseMove += new MouseEventHandler(BtnDisagree_MouseMove);//危险，选中不同意
            KeyDown += new KeyEventHandler(Form1_KeyDown);                      //回车键
            txt_PM.Leave += new EventHandler(txt_PM_Leave);
            txt_PM.Enter += new EventHandler(txt_PM_Enter);

            Thread mesThread = new Thread(Mes);
            mesThread.Start();
        }

        
        #region 资源加载
        /// <summary>
        /// 路径获取
        /// </summary>
        private void PathInit() {
            Path_Client = MyIni.ReadIniData("Path", "Client", "", Path_Ini);
            Path_Project = MyIni.ReadIniData("Path", "Project", "", Path_Ini);
            Path_VBS_restart = MyIni.ReadIniData("Path", "ReStar", "", Path_Ini);
        }

        private void Init_Image() {
            BgDraws[0] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_0.jpg");
            //字
            BgDraws[1] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_z1.jpg");
            BgDraws[2] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_z2.jpg");
            BgDraws[3] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_z3.jpg");
            BgDraws[4] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_z4.jpg");
            BgDraws[5] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_z5.jpg");

            BgDraws[6] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi21.jpg");
            BgDraws[7] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi22.jpg");
            BgDraws[8] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi23.jpg");
            BgDraws[9] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi24.jpg");
            BgDraws[10] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi25.jpg");
            BgDraws[11] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi26.jpg");
            BgDraws[12] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi27.jpg");
            BgDraws[13] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_zi28.jpg");
            //bg
            BgDraws[14] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_0.jpg");
            //Ali
            BgDraws[15] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_1.jpg");
            BgDraws[16] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_2.jpg");
            BgDraws[17] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_3.jpg");
            BgDraws[18] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_4.jpg");
            BgDraws[19] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_5.jpg");
            BgDraws[20] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_6.jpg");
            BgDraws[21] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_7.jpg");
            BgDraws[22] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_8.jpg");
            //chat
            BgDraws[23] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_chat1.jpg");
            BgDraws[24] = Image.FromFile(Path_Project + @"\NoClose\Properties\draw\bg_Ali_chat2.jpg");
            //btn
            im_disagreeL[0] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_1.png");
            im_disagreeL[1] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_2.png");
            im_disagreeL[2] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_3.png");
            im_disagreeL[3] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_4.png");
            im_disagreeL[4] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_5.png");
            im_disagreeR[0] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_11.png");
            im_disagreeR[1] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_12.png");
            im_disagreeR[2] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_13.png");
            im_disagreeR[3] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_14.png");
            im_disagreeR[4] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_15.png");
            //agree
            im_Agree[0]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_1.png");
            im_Agree[1]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_2.png");
            im_Agree[2]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_3.png");
            im_Agree[3]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_4.png");
            im_Agree[4]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_5.png");
            im_Agree[5]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_6.png");
            im_Agree[6]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_7.png");
            im_Agree[7]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_8.png");
            im_Agree[8]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_9.png");
            im_Agree[9]  = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_10.png");
            im_Agree[10] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_11.png");
            im_Agree[11] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_12.png");
            im_Agree[12] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_13.png");
            im_Agree[13] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_14.png");
            im_Agree[14] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_15.png");
            im_Agree[15] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_16.png");
            im_Agree[16] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_17.png");
            im_Agree[17] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_18.png");
            im_Agree[18] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_19.png");

            im_bgagree = Image.FromFile(Path_Project+ @"\NoClose\Properties\bg_agree.jpg");
        }
        #endregion




        #region 线程逻辑
        public void Draw1() {
            gs = pictureBox1.CreateGraphics();
            //开始先等待CG
            Thread.Sleep(1000);
            //第一句话
            for (int i = 1; i <= 5; i++) {
                gs.DrawImage(BgDraws[i], bg);
                Thread.Sleep(100);
            }
            Thread.Sleep(1500);
            //第二句话
            for (int i = 6; i <= 13; i++) {
                gs.DrawImage(BgDraws[i], bg);
                Thread.Sleep(100);
            }
            Thread.Sleep(1500);
            //阿里来
            for (int i = 14; i <= 22; i++) {
                gs.DrawImage(BgDraws[i], bg);
                Thread.Sleep(50);
            }
            Thread.Sleep(1500);
            //chat1
            gs.DrawImage(BgDraws[23], bg);
            Thread.Sleep(3000);
            gs.DrawImage(BgDraws[24], bg);
            Thread.Sleep(3000);
            pictureBox1.Image = BgDraws[24];
            gs.Clear(this.BackColor);
            //按钮现
            isCGOver = true;
            btn_agree.Location = new Point(811, 270);
            btn_disagree.Location = new Point(496, 319);
            showMes("点击阿狸，会有帮助！");
        }

        /// <summary>
        /// 不同意逃跑逻辑
        /// </summary>
        private void DisAgreeRun() {
            int index = 0;
            Image[] lin = new Image[5];
            while (!isExit) {
                while (speed != 0) {
                    Random random = new Random();
                    int x = random.Next(1, 1266);
                    int y = random.Next(1, 668);

                    int btn_x = btn_disagree.Location.X;
                    int btn_y = btn_disagree.Location.Y;


                    int abs = Math.Abs(x - btn_x) / (x - btn_x);
                    if (abs == 1) {
                        lin = im_disagreeR;
                    }
                    if (abs == -1) {
                        lin = im_disagreeL;
                    }
                    if (Math.Abs(x - btn_x) >= Math.Abs(y - btn_y) && speed != 0) {
                        int time = Math.Abs(x - btn_x) / speed;
                        if (time != 0) {
                            int speed_y = Math.Abs(y - btn_y) / time;
                            while (true) {
                                if (Math.Abs(x - btn_disagree.Location.X) <= 20) {
                                    break;
                                }
                                if (speed == 0) {
                                    break;
                                }
                                int new_x = btn_disagree.Location.X + speed * ((Math.Abs(x - btn_x) / (x - btn_x)));
                                int new_y = btn_disagree.Location.Y + speed_y * ((Math.Abs(y - btn_y) / (y - btn_y)));
                                btn_disagree.Location = new Point(new_x, new_y);
                                if (btn_disagree.Location.X < 0 || btn_disagree.Location.X > 1266 || btn_disagree.Location.Y < 0 || btn_disagree.Location.Y > 668) {
                                    //最后的保护
                                    break;
                                }
                                btn_disagree.Image = lin[index];

                                Thread.Sleep(10);
                                if (index >= 4) {
                                    index = 0;
                                } else {
                                    index++;
                                }
                            }
                        }
                    }
                    if (Math.Abs(x - btn_x) < Math.Abs(y - btn_y)&&speed != 0) {
                        int time = y - btn_y / speed;
                        if (time != 0) {
                            int speed_x = Math.Abs(x - btn_x) / time;

                            while (true) {
                                if (Math.Abs(y - btn_disagree.Location.Y) <= 20) {
                                    break;
                                }
                                if (speed == 0) {
                                    break;
                                }
                                int new_y = btn_disagree.Location.Y + speed * ((Math.Abs(x - btn_x) / (x - btn_x)));
                                int new_x = btn_disagree.Location.X + speed_x * ((Math.Abs(x - btn_y) / (x - btn_y)));

                                btn_disagree.Location = new Point(new_x, new_y);
                                if (btn_disagree.Location.X < 0 || btn_disagree.Location.X > 1266 || btn_disagree.Location.Y < 0 || btn_disagree.Location.Y > 668) {
                                    //最后的保护
                                    break;
                                }
                                btn_disagree.Image = lin[index];

                                Thread.Sleep(10);
                                if (index >= 4) {
                                    index = 0;
                                } else {
                                    index++;
                                }
                            }
                        }
                    }

                    if (speed == 0) {
                        btn_disagree.Image = lin[0];
                        if (JieDuan == 2) {
                            JieDuan = 3;
                        }
                        break;
                    }
                }

            }
            exitbools[0] = true;
        }
        private void Changespeed() {
            while (!isExit) {
                while (isDisRun) {
                    Thread.Sleep(50);
                    speed = speed / 2;//10
                    Thread.Sleep(50);
                    speed = speed / 2;//5
                    while (true) {
                        Thread.Sleep(1000);//2,1,0
                        speed = speed / 2;
                        if (speed == 0) {
                            isDisRun = false;
                            break;
                        }
                    }
                }
            }
            exitbools[1] = true;
        }
        /// <summary>
        /// 消息提示
        /// </summary>
        private void Mes() {
            while (!isExit) {
                while (isShowMes) {
                    //Thread.Sleep(2000);
                    if (isClientFlash) {
                        Thread.Sleep(3000);
                    } else {
                        DateTime current = DateTime.Now;
                        while (current.AddMilliseconds(3000) > DateTime.Now) {
                            if (isClientFlash) {
                                break;
                            }
                        }
                    }
                    lab_mes.Text = "";
                    isShowMes = false;
                    isClientFlash = false;
                }
            }
            exitbools[2] = true;
        }
        private void fiAgree() {
            while (!isExit) {
                while (isPlayagreecg) {
                    for (int i = 0; i < im_Agree.Length; i++) {
                        btn_agree.Image = im_Agree[i];
                        Thread.Sleep(100);
                    }
                    isPlayagreecg = false;
                }
            }
            exitbools[3] = true;
        }

        #region 跑步第二套
        //int runspeed = 50;
        //int OnceYi = 20;
        //private void btn_disRunPos() {
        //    while (isDisRun) {
        //        Random random = new Random();
        //        int x = random.Next(1, 1266);
        //        int y = random.Next(1, 668);
        //        btn_agree.Text = (x + "," + y);
        //        int bx = btn_disagree.Location.X;
        //        int by = btn_disagree.Location.Y;

        //        if (y == by) {
        //            if (x == bx) {
        //            } else {
        //                run_x(x, y, bx, by);
        //            }
        //        } else if (x == bx) {
        //            if (y == by) { } else {
        //                run_y(x, y, bx, by);
        //            }
        //        } else if (Math.Abs(x - bx) >= Math.Abs(y - by)) {
        //            run_xbigy(x, y, bx, by);
        //        } else if (Math.Abs(x - bx) <= Math.Abs(y - by)) {
        //            run_ybigx(x, y, bx, by);
        //        }
        //    }
        //}
        //private void run_x(int x, int y, int bx, int by) {
        //    int dirX = (x - bx) / (Math.Abs(x - bx));
        //    while (true) {
        //        if (Math.Abs(x - btn_disagree.Location.X) <= OnceYi) {
        //            break;
        //        }
        //        if (finilProtect()) {
        //            break;
        //        }
        //        int nx = btn_disagree.Location.X + OnceYi * dirX;
        //        btn_disagree.Location = new Point(nx, by);

        //        Thread.Sleep(runspeed);
        //    }
        //}
        //private void run_y(int x, int y, int bx, int by) {
        //    int dirY = (y - by) / (Math.Abs(y - by));
        //    while (true) {
        //        if (Math.Abs(y - btn_disagree.Location.Y) <= OnceYi) {
        //            break;
        //        }
        //        if (finilProtect()) {
        //            break;
        //        }
        //        int ny = btn_disagree.Location.Y + OnceYi * dirY;
        //        btn_disagree.Location = new Point(bx, ny);

        //        Thread.Sleep(runspeed);
        //    }
        //}
        //private void run_xbigy(int x, int y, int bx, int by) {
        //    int dirX = (x - bx) / (Math.Abs(x - bx));
        //    int dirY = (y - by) / (Math.Abs(y - by));
        //    int index = 0;
        //    while (true) {
        //        if (Math.Abs(x - btn_disagree.Location.X) <= OnceYi) {
        //            break;
        //        }
        //        if (finilProtect()) {
        //            break;
        //        }
        //        int nx = btn_disagree.Location.X + OnceYi * dirX;
        //        if (btn_disagree.Location.Y == y) {
        //            run_x(x, y, btn_disagree.Location.X, btn_disagree.Location.Y);
        //            break;
        //        } else {
        //            index++;
        //            int ny;
        //            if (index == Math.Abs(btn_disagree.Location.X - x) / Math.Abs(btn_disagree.Location.Y - y)) {
        //                ny = btn_disagree.Location.Y + OnceYi * dirY;
        //                index = 0;
        //            } else {
        //                ny = btn_disagree.Location.Y;
        //            }
        //            btn_disagree.Location = new Point(nx, ny);
        //        }

        //        Thread.Sleep(runspeed);

        //    }
        //}
        //private void run_ybigx(int x, int y, int bx, int by) {
        //    int dirX = (x - bx) / (Math.Abs(x - bx));
        //    int dirY = (y - by) / (Math.Abs(y - by));
        //    int index = 0;
        //    while (true) {
        //        if (Math.Abs(y - btn_disagree.Location.Y) <= OnceYi) {
        //            break;
        //        }
        //        if (finilProtect()) {
        //            break;
        //        }
        //        int ny = btn_disagree.Location.Y + OnceYi * dirY;
        //        if (btn_disagree.Location.X == x) {
        //            run_y(x, y, btn_disagree.Location.X, btn_disagree.Location.Y);
        //            break;
        //        } else {
        //            index++;
        //            int nx;
        //            if (index == Math.Abs(btn_disagree.Location.Y - y) / Math.Abs(btn_disagree.Location.X - x)) {
        //                nx = btn_disagree.Location.X + OnceYi * dirX;
        //                index = 0;
        //            } else {
        //                nx = btn_disagree.Location.X;
        //            }
        //            btn_disagree.Location = new Point(nx, ny);
        //        }

        //        Thread.Sleep(runspeed);
        //    }
        //}
        //private bool finilProtect() {
        //    if (btn_disagree.Location.X < 0 || btn_disagree.Location.X > 1266 || btn_disagree.Location.Y > 668 || btn_disagree.Location.Y < 0) {
        //        return true;
        //    }
        //    return false;
        //}
        #endregion
        #endregion

        #region 监听
        /// <summary>
        /// 关闭监听无限循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form1_FormClosed(object sender, FormClosingEventArgs e) {
            isExit = true;
            DateTime current = DateTime.Now;
            bool over = false;
            while (current.AddMilliseconds(500) > DateTime.Now) {
                for (int i = 0; i < exitbools.Length; i++) {
                    if (!exitbools[i]) {
                        break;
                    }
                    over = true;
                    break;
                }
                if (over) {
                    break;
                }
            }
            MyIni.WriteIniData("Time", "Last", nNowTime.ToString(), Path_Ini);
            if (!isOver) {
                reStart();
            } else {
                System.Environment.Exit(0);
            }
        }
        //无限循环
        private void reStart() {
            Client_SendMesg("1:重启");
            //BgDraws
            string str = MyIni.ReadIniData("Path", "ReStar", "-1", Path_Ini);
            if (str.Equals("-1")) {
                str = "../../reStart.vbs";
            }
            ProcessStartInfo startInfo = new ProcessStartInfo {
                FileName = "wscript.exe",
                Arguments = str
            };
            Process.Start(startInfo);
        }

        /// <summary>
        /// PM命令框监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_PM_Leave(object sender, EventArgs e){
            if (txt_PM.Text == ""|| txt_PM.Text == "无效") {
                hasText = false;                            //文本框有文本置为假
                txt_PM.Text = tishi;                        //显示提示信息
                txt_PM.ForeColor = Color.LightGray;         //字体颜色设为浅灰色

            } else {
                hasText = true;                             //否则为文本框有文本
            } 
        }
        private void txt_PM_Enter(object sender, EventArgs e){
            if (hasText == false){
                txt_PM.Text = "";                               //清空文本
                txt_PM.ForeColor = Color.Black;                 //文本颜色设为黑色
            }
        }

        private void Btnagree_MouseDown(object sender, MouseEventArgs e) {
            Client_SendMesg("6:同意");
            
            
            lab_mes.Text = "好的呢！那你喜欢早晨玩还是傍晚呢，有没有什么计划想法？";
            LabInitPos();
            txt_PM.Text = "喜欢运动的时间；建议";
            txt_PM.ForeColor = Color.LightGray;
            txt_PM.Show();
            
            pictureBox1.Image = im_bgagree;
            if (!isAgree) {
                isOver = true;
                JieDuan = 5;
                btn_disagree.Hide();
                btn_agree.Size = new Size(80, 80);
                btn_agree.Location = new Point(275,349);
                btn_agree.Image = Image.FromFile(Path_Project + @"\NoClose\Properties\agree_s.png");
            }
        }
        private void Btnagree_MouseUp(object sender, MouseEventArgs e) {
            if (!isAgree) {
                isAgree = true;
                btn_agree.Size = new Size(224, 224);
                Thread agreethread = new Thread(fiAgree);
                agreethread.Start();
            }
            isPlayagreecg = true;
        }

        /// <summary>
        /// 鼠标一经过就随机生成一个位置
        /// 前面要btn_disagree.MouseMove+=new MouseEventHandler(BtnDisagree_MouseMove)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDisagree_MouseMove(object sender, MouseEventArgs e) {
            if (!isDisRun&&JieDuan==0) {
                Client_SendMesg("2:鼠标瞬移");
                numIndex = numIndex - 1;
                Random random = new Random();
                int x = random.Next(1, 1266);
                int y = random.Next(1, 668);
                btn_disagree.Location = new Point(x, y);
                if (numIndex <= 0) {
                    if (!isTishi) {
                        txt_PM.Show();
                        isTishi = true;
                    }
                    //txt_PM.Text = "你的名字";
                    showMes("小灰兔子有技能，着实难抓，可以试着输入作弊码");
                }
            }
            //if (isDisRun&&JieDuan==1) {
            //    speed = 40;
            //}
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (!isBegin && isFirst) {
                isBegin = true;
                Thread t_Draw1 = new Thread(new ThreadStart(Draw1));
                t_Draw1.Start();
            }
            if (isBegin && isCGOver && e.X >= 1015 && e.X <= 1215 && e.Y >= 158 && e.Y <= 438) {
                //点中Ali
                txt_PM.Show();
                //txt_PM.Text = "请输入名字";
            } else {
                txt_PM.Hide();
            }
        }

        private void btn_disagree_Click(object sender, EventArgs e) {
            if (JieDuan == 1) {
                showMes(new string[] { "还觉的太快的话，再输入作弊码吧", "小灰兔子还有个加速技能" });
                isDisRun = true;
                speed = 40;
            }
            Client_SendMesg("5:不同意");
            if (JieDuan == 3) {
                isOver = true;
                lab_mes.Text = "好的，那下次再约喽,可以退出了。如果点错了的话，还是可以点小白兔的";
                //JieDuan = 4;
                LabInitPos();
                //showMes("好的，那下次再约喽,可以退出了。如果点错了的话，还可以点小白兔的");
            }
            if (JieDuan == 2) {
                showMes(new string[] { "55555，请让我最后挣下", "要哭了哦" });
            }
        }

        //回车键
        private void Form1_KeyDown(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.Enter) {
                string strPM = txt_PM.Text.ToLower();
                for (int i = 0; i < exitPM.Length; i++) {
                    if (exitPM[i].ToLower().Equals(strPM) && JieDuan == 0) {
                        Client_SendMesg("3:停止瞬移");
                        JieDuan = 1;
                        tishi = "任意输入想对我说的话";
                        showMes("小灰兔子失去瞬移技能");
                        isDisRun = true;
                        btn_disagree.Image = im_disagreeL[0];
                        Thread runThred = new Thread(DisAgreeRun);
                        runThred.Start();
                        Thread speedThred = new Thread(Changespeed);
                        speedThred.Start();
                        txt_PM.Text = "";
                        return;
                    }
                }
                for (int i = 0; i < exit2PM.Length; i++) {
                    if (exit2PM[i].ToLower().Equals(strPM)) {
                        isExit = true;
                        DateTime current = DateTime.Now;
                        bool over = false;
                        while (current.AddMilliseconds(500) > DateTime.Now) {
                            for (int y = 0; y < exitbools.Length; y++) {
                                if (!exitbools[y]) {
                                    break;
                                }
                                over = true;
                                break;
                            }
                            if (over) {
                                break;
                            }
                        }
                        Client_SendMesg("4:直接退出");
                        isOver = true;
                        System.Environment.Exit(0);
                        //this.Close();
                        txt_PM.Text = "";
                        return;
                    }
                }
                if (JieDuan == 1) {
                    JieDuan = 2;
                    Client_SendMesg("7:" + txt_PM.Text);
                    txt_PM.Text = "";
                    speed = 40;
                    isDisRun = true;
                } else if (JieDuan==5) {
                    Client_SendMesg("8:" + txt_PM.Text);
                    txt_PM.Text = "";
                    showMes("嗯嗯，好的呢！那周末见哦，可以退出了哦");
                } else if (JieDuan == 4) {
                    Client_SendMesg("9:" + txt_PM.Text);
                } else {
                    txt_PM.Text = "无效";
                    txt_PM.ForeColor = Color.Gray;
                }
            }
        }
        #endregion
       
            //休息
            //public static void Delay(int mm) {
            //    DateTime current = DateTime.Now;
            //    while (current.AddMilliseconds(mm) > DateTime.Now) {
            //        Application.DoEvents();
            //    }
            //    return;
            //}


        private void LabInitPos() {
            //1366,768
            int newX = (1366 - lab_mes.Size.Width) / 2;
            lab_mes.Location = new System.Drawing.Point(newX, lab_mes.Location.Y);
        }

        
        private void showMes(string s) {
            lab_mes.Text =s;
            LabInitPos();
            isShowMes = true;
        }
        private void showMes(string[] strs) {
            Random r = new Random();
            int index = r.Next(1,strs.Length);
            string s = strs[index - 1];
            showMes(s);
        }
        

        private void Client_SendMesg(string str) {
            if (isConnectSucced) {
                string sendStr = str;
                byte[] sendBytes = Encoding.Unicode.GetBytes(sendStr);

                try {
                    client_socket.Close();
                    client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    client_socket.Connect(ipe);
                    client_socket.Send(sendBytes);
                } catch (Exception) {
                }
            }
            
        }

        private void TimeCheck() {
            string sLasttime = MyIni.ReadIniData("Time", "Last", "0", Path_Ini);
            int.TryParse(sLasttime, out nLastTime);
            var strTime = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            nNowTime = (int)strTime.TotalSeconds;
            int cha = nNowTime - nLastTime;

            if (cha > 100) {
                isFirst = true;
                //背景图设置
                pictureBox1.Image = BgDraws[0];

            } else {
                isCGOver = true;
                showMes(donotit);
                isFirst = false;
                pictureBox1.Image = BgDraws[24];
                btn_agree.Location = new Point(811, 270);
                btn_disagree.Location = new Point(496, 319);
                showMes("点击阿狸，会有帮助！");
            }
            if (!isFirst) {
                isBegin = true;
            }
        }

        private void ClientInit() {
            string ipStr = "127.0.0.1";
            //string ipStr = "192.168.46.182";
            int port = 8888;
            IPAddress ip = IPAddress.Parse(ipStr);
            ipe = new IPEndPoint(ip, port);
            //创建客户端socket
            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //连接服务器
            try {
                client_socket.Connect(ipe);
                Thread thread = new Thread(MyThread_recive);
                thread.Start();
                isConnectSucced = true;
            } catch (Exception) {
                isConnectSucced = false;
            }
        }

        private void MyThread_recive() {
            while (!isExit) {
                try {
                    //接收消息
                    string recStr = "";
                    byte[] recBytes = new byte[1024];
                    int bytes = client_socket.Receive(recBytes, recBytes.Length, 0);
                    recStr += Encoding.Unicode.GetString(recBytes, 0, bytes);
                    resiveMesg = recStr;// "From Server messages: {0}" + recStr;
                    if (resiveMesg.Equals("N")|| resiveMesg.Equals("n")) {
                        showMes("这句话好有深度，我还想听其他的话");
                        if (JieDuan == 2) {
                            JieDuan = 1;
                        }
                    } else if (resiveMesg.Equals("Y")|| resiveMesg.Equals("y")) {
                        showMes("我也是");
                        if (JieDuan == 2) {
                            JieDuan = 3;
                        }
                    } else if (resiveMesg.Equals("O")|| resiveMesg.Equals("o")) {
                        showMes("蟹蟹");
                        //JieDuan = 3;
                    } else {
                        isClientFlash = true;
                        Thread.Sleep(50);
                        showMes(resiveMesg);
                    }
                } catch (Exception) {

                }
            }
            exitbools[4] = true;
        }

    }
}
