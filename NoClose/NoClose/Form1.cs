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
        //protected override CreateParams CreateParams {
        //    get {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }

        //}
        //控制
        private bool isOver = false;  //重启
        private bool isAgree = false;  //是否答应
        private bool isPlayagreecg = false;  //是否答应
        private bool isCGOver = false;  //cg结束
        private bool isZi = true;     //cg字
        private bool isALi = false;   //cg阿狸
        private bool isBegin = false; //第一次点击
        private bool isFirst = true; //第一次进入
        private bool isChose = false; //开始考虑
        private bool isDisRun = false; //不同意进入逃跑状态
        private bool isShowMes = false; //显示提示
        private int speed = 20;//不同意逃跑速度
        private int time_zi = 100;
        private int nLastTime = 0;
        private int nNowTime = 0;

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
        };
        private string[] exit2PM = {
            "shutdown",
            "tuichu",
            "exit",
            "e",
        };
        //范围
        private RectangleF r_bg = new RectangleF(0, 0, 1366f, 768f);
        private RectangleF r_ALi = new RectangleF(1015, 158, 200, 280);
        private RectangleF r_ALi_chat = new RectangleF(0, 0, 1366, 768);

        private int[] zi_x = { 1000, 1050, 1100, 1150, 1030, 1080, 1070, 1120 };
        private int[] zi_y = { 200, 210, 220, 230, 265, 275, 325, 335 };
        //private bool[] b_zi = {false, false, false, false, false, false, false, false };
        private int[] w = { 0, 0, 0, 0, 0, 0, 0, 0 };
        private int[] h = { 0, 0, 0, 0, 0, 0, 0, 0 };



        //绘图变
        private Graphics gs;
        private Image im_ALi_chat;
        //字
        private Image im_zi_0;
        private Image im_zi_1;
        private Image im_zi_2;
        private Image im_zi_3;
        private Image im_zi_4;
        private Image im_zi_5;
        private Image im_zi_6;
        private Image im_zi_7;
        private Image im_zi_8;
        //run
        private Image[] im_runL = new Image[5];
        private Image[] im_runR = new Image[5];
        private Image[] im_Agree = new Image[18];
        //private List<Image> im_zi;

        //路径
        private string Path_Ini = "../../Ini.ini";
        private string Path_Project = "";
        private string Path_Client = "";
        private string Path_VBS_restart = "";
        //文字
        private string[] donotit = {
            "直接退出是不行的",
            "你可以问问阿狸怎么退出",
            "如果你知道特殊的命令，那你就能直接出去了",
            "好期待~",
        };

        public Form1() {
            InitializeComponent();
            PathInit();
            string sLasttime = MyIni.ReadIniData("Time", "Last", "0", Path_Ini);
            int.TryParse(sLasttime, out nLastTime);
            var strTime = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            nNowTime = (int)strTime.TotalSeconds;
            int cha = nNowTime - nLastTime;
            var nowTime = new DateTime(1970, 1, 1, 8, 0, 0).AddSeconds(cha);//时间戳转时间
            //lab_mes.Text = nLastTime + "; " + nNowTime+"; "+ nowTime.ToString();
            //lab_mes.Text = "LastTime:"+ sLasttime + " 时间差：" + cha + "; " + nowTime.ToString();
            showMes("LastTime:" + sLasttime + " 时间差：" + cha + "; " + nowTime.ToString());
            txt_PM.Hide();
            if (cha > 20) {
                isFirst = true;
                //背景图设置
                pictureBox1.Image = Image.FromFile(Path_Project + @"\NoClose\Properties\bg\background1.jpg");//Image.FromFile(Path_Project + @"\NoClose\Properties\bg\background1.jpg");
                btn_disagree.Hide();
                btn_agree.Hide();
            } else {
                showMes(donotit);
                isFirst = false;
                pictureBox1.Image = Image.FromFile(Path_Project + @"\NoClose\Properties\chat\ALiChat_2.png");//Image.FromFile(Path_Project + @"\NoClose\Properties\bg\background1.jpg");
            }
            if (!isFirst) {
                isBegin = true;
            }

            
            
            ////采用双缓冲技术的控件必需的设置
            //SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            //监听
            FormClosing += new FormClosingEventHandler(form1_FormClosed);       //重启监听
            pictureBox1.MouseMove+= new MouseEventHandler(Form1_MouseMove);     //画面点击
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);//鼠标移动
            btn_agree.MouseUp += new MouseEventHandler(Btnagree_MouseUp);
            btn_agree.MouseDown += new MouseEventHandler(Btnagree_MouseDown);
            btn_disagree.MouseMove+=new MouseEventHandler(BtnDisagree_MouseMove);//危险，选中不同意
            KeyDown += new KeyEventHandler(Form1_KeyDown);                      //回车键
           //加载图片
            Init_Image();

            Thread mesThread = new Thread(Mes);
            mesThread.Start();
        }
        /// <summary>
        /// 路径获取
        /// </summary>
        private void PathInit() {
            Path_Client = MyIni.ReadIniData("Path", "Client", "", Path_Ini);
            Path_Project = MyIni.ReadIniData("Path", "Project", "", Path_Ini);
            Path_VBS_restart = MyIni.ReadIniData("Path", "ReStar", "", Path_Ini);
        }

        private void Init_Image() {
            im_ALi_chat = Image.FromFile(Path_Project + @"\NoClose\Properties\bg\bg_window2.png");
            //字
            im_zi_0 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_0.png");
            im_zi_1 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_0.png");
            im_zi_2 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_0.png");
            im_zi_3 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_0.png");
            im_zi_4 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_4.png");
            im_zi_5 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_5.png");
            im_zi_6 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_6.png");
            im_zi_7 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_7.png");
            im_zi_8 = im_zi_0;//mage.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_8.png");
            //run
            im_runL[0] = Image.FromFile(Path_Project+ @"\NoClose\Properties\run\dis_1.png");
            im_runL[1] = Image.FromFile(Path_Project+ @"\NoClose\Properties\run\dis_2.png");
            im_runL[2] = Image.FromFile(Path_Project+ @"\NoClose\Properties\run\dis_3.png");
            im_runL[3] = Image.FromFile(Path_Project+ @"\NoClose\Properties\run\dis_4.png");
            im_runL[4] = Image.FromFile(Path_Project+ @"\NoClose\Properties\run\dis_5.png");
            im_runR[0] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_11.png");
            im_runR[1] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_12.png");
            im_runR[2] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_13.png");
            im_runR[3] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_14.png");
            im_runR[4] = Image.FromFile(Path_Project + @"\NoClose\Properties\run\dis_15.png");
            im_Agree[0] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_1.png");
            im_Agree[1] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_2.png");
            im_Agree[2] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_3.png");
            im_Agree[3] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_4.png");
            im_Agree[4] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_5.png");
            im_Agree[5] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_6.png");
            im_Agree[6] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_7.png");
            im_Agree[7] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_8.png");
            im_Agree[8] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_9.png");
            im_Agree[9] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_10.png");
            im_Agree[10] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_11.png");
            im_Agree[11] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_12.png");
            im_Agree[12] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_13.png");
            im_Agree[13] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_14.png");
            im_Agree[14] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_15.png");
            im_Agree[15] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_16.png");
            im_Agree[16] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_17.png");
            im_Agree[17] = Image.FromFile(Path_Project + @"\NoClose\Properties\agree\agree_18.png");

        }

        private void form1_FormClosed(object sender, FormClosingEventArgs e) {
            MyIni.WriteIniData("Time", "Last", nNowTime.ToString(), Path_Ini);
            if (!isOver) {
                reStart();
            }
        }
        //无限循环
        private void reStart() {
            ProcessStartInfo startInfo = new ProcessStartInfo {
                FileName = "wscript.exe",
                //Arguments = @"..\reStart.vbs"
                Arguments = Path_Project + @"\reStart.vbs"
            };
            Process.Start(startInfo);
        }

        
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
                    gs.DrawImage(im_ALi_chat,r_ALi_chat);
                }
                if (isChose) {
                    break;
                }
            }
            
        }
        public void Draw2() {
            DateTime current = DateTime.Now;
            im_zi_1 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_1.png");
            while (current.AddMilliseconds(time_zi) > DateTime.Now) {
                if (w[0] < 50) {
                    Thread.Sleep(1);
                    w[0] = w[0] + 1;
                    h[0] = h[0] + 1;
                }
            }
            im_zi_2 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_2.png");
            while (current.AddMilliseconds(time_zi*2) > DateTime.Now) {
                if (w[1] < 50) {
                    Thread.Sleep(1);
                    w[1] = w[1] + 1;
                    h[1] = h[1] + 1;
                }
            }
            im_zi_3 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_3.png");
            while (current.AddMilliseconds(time_zi * 3) > DateTime.Now) {
                if (w[2] < 50) {
                    Thread.Sleep(1);
                    w[2] = w[2] + 1;
                    h[2] = h[2] + 1;
                }
            }
            im_zi_4 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_4.png");
            while (current.AddMilliseconds(time_zi * 4) > DateTime.Now) {
                if (w[3] < 50) {
                    Thread.Sleep(1);
                    w[3] = w[3] + 1;
                    h[3] = h[3] + 1;
                }
            }
            w[4] = 50;
            h[4] = 50;
            im_zi_5 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_5.png");
            //while (current.AddMilliseconds(time_zi * 5) > DateTime.Now) {
            //    if (w[4] < 50) {
            //        Thread.Sleep(1);
            //        w[4] = w[4] + 1;
            //        h[4] = h[4] + 1;
            //    }
            //}
            Thread.Sleep(time_zi);
            im_zi_6 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_6.png");
            while (current.AddMilliseconds(time_zi * 6) > DateTime.Now) {
                if (w[5] < 50) {
                    //Thread.Sleep(1);
                    //w[5] = w[2] + 1;
                    //h[5] = h[2] + 1;
                    w[5] = 50;
                    h[5] = 50;
                }
            }
            im_zi_7 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_7.png");
            im_zi_8 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_8.png");
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
            
            pictureBox1.Image= Image.FromFile(Path_Project + @"\NoClose\Properties\bg\background1.jpg");
            gs.Clear(this.BackColor);
            //gs.Dispose();
            //gs = pictureBox1.CreateGraphics();
            isZi = true;
            Thread.Sleep(time_zi);
            im_zi_1 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_11.png");
            Thread.Sleep(time_zi);
            im_zi_2 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_12.png");
            Thread.Sleep(time_zi);
            im_zi_3 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_13.png");
            Thread.Sleep(time_zi);
            im_zi_4 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_14.png");
            Thread.Sleep(time_zi);
            im_zi_5 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_15.png");
            Thread.Sleep(time_zi);
            im_zi_6 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_16.png");
            Thread.Sleep(time_zi);
            im_zi_7 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_17.png");
            Thread.Sleep(time_zi);
            im_zi_8 = Image.FromFile(Path_Project + @"\NoClose\Properties\zi\zi_18.png");
            isZi = false;
            Thread.Sleep(1000);
            pictureBox1.Image = Image.FromFile(Path_Project + @"\NoClose\Properties\bg\background1.jpg");
            gs.Clear(this.BackColor);
            isALi = true;
            Thread.Sleep(500);
            im_ALi_chat = Image.FromFile(Path_Project + @"\NoClose\Properties\chat\ALiChatBg0.jpg");
            Thread.Sleep(500);
            im_ALi_chat = Image.FromFile(Path_Project + @"\NoClose\Properties\chat\ALiChat_1.png");
            Thread.Sleep(3000);
            im_ALi_chat = Image.FromFile(Path_Project + @"\NoClose\Properties\chat\ALiChat_2.png");
            Thread.Sleep(500);
            isALi = false;
            Thread.Sleep(500);
            isCGOver = true;
            isChose = true;
            gs.Clear(this.BackColor);
            gs.Dispose();
            pictureBox1.Image = Image.FromFile(Path_Project + @"\NoClose\Properties\chat\ALiChat_2.png");
        }

        private void Btnagree_MouseDown(object sender, MouseEventArgs e) {
            isOver = true;
            if (!isAgree) {
                btn_agree.Size = new Size(80, 80);
                btn_agree.Image = Image.FromFile(Path_Project + @"\NoClose\Properties\agree_s.png");
            }
        }
        private void Btnagree_MouseUp(object sender, MouseEventArgs e) {
            showMes(new string[] { "说好的哦", "那快退出吧，不许变哦", "不过这周是有加班的，我们也可以下周约的，你答应了就好", "那这周去吗，好像有加班的样子的" });
            if (!isAgree) {
                isAgree = true;
                btn_agree.Size = new Size(224, 224);
                Thread agreethread = new Thread(fiAgree);
                agreethread.Start();
                
            }
            isPlayagreecg = true;
        }

        private void fiAgree() {
            while (true) {
                while (isPlayagreecg) {
                    for (int i = 0; i < im_Agree.Length; i++) {
                        btn_agree.Image = im_Agree[i];
                        Thread.Sleep(100);
                    }
                    isPlayagreecg = false;
                }
            }
        }
            //休息
            //public static void Delay(int mm) {
            //    DateTime current = DateTime.Now;
            //    while (current.AddMilliseconds(mm) > DateTime.Now) {
            //        Application.DoEvents();
            //    }
            //    return;
            //}

            private void Form1_MouseMove(object sender, MouseEventArgs e) {
            if (true) {
                //r_Tao_chat = new RectangleF(e.X, e.Y, 300,100);
            }
        }
        private void BtnDisagree_MouseMove(object sender, MouseEventArgs e) {
            if (!isDisRun) {
                Random random = new Random();
                int x = random.Next(1, 1266);
                int y = random.Next(1, 668);
                btn_disagree.Location = new Point(x, y);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            if (!isBegin&&isFirst) {
                isBegin = true;
                Thread t_Draw1 = new Thread(new ThreadStart(Draw1));
                t_Draw1.Start();
                Thread t_Draw2 = new Thread(new ThreadStart(Draw2));
                t_Draw2.Start();
            }
            if (isCGOver) {
                btn_agree.Show();
                btn_disagree.Show();
            }
            if (isBegin && isCGOver && e.X >= 1015 && e.X <= 1215 && e.Y >= 158 && e.Y <= 438) {
                //点中Ali
                txt_PM.Show();
            } else {
                txt_PM.Hide();
            }
        }

        private void btn_disagree_Click(object sender, EventArgs e) {
            isOver = true;
            showMes(new string[] { "要哭了哦","周末开心" });
        }

        //private void textBox1_TextChanged(object sender, EventArgs e) {

        //}

        //回车键
        private void Form1_KeyDown(object sender, KeyEventArgs e){

            if (e.KeyCode == Keys.Enter){
                string strPM = txt_PM.Text.ToLower();
                for (int i = 0; i < exitPM.Length; i++) {
                    if (exitPM[i].ToLower().Equals(strPM)) {
                        //lab_mes.Text = "小灰兔子不再乱跑了";
                        showMes("小灰兔子不再乱跑了");
                        isDisRun = true;
                        btn_disagree.Image = im_runL[0];
                        Thread runThred = new Thread(DisAgreeRun);
                        runThred.Start();
                        Thread speedThred = new Thread(Changespeed);
                        speedThred.Start();
                    }
                }
                for (int i = 0; i < exit2PM.Length; i++) {
                    if (exit2PM[i].ToLower().Equals(strPM)) {
                        //lab_mes.Text = "小灰兔子不再乱跑了";
                        //showMes("好吧，那你有空的话再来哦，可以直接关掉了");
                        isOver = true;
                        this.Close();
                    }
                }
            }
        }

        private void LabInitPos() {
            //1366,768
            int newX = (1366 - lab_mes.Size.Width) / 2;
            lab_mes.Location = new System.Drawing.Point(newX, lab_mes.Location.Y);
        }

        
        private void DisAgreeRun() {
            int index = 0;
            Image[] lin = new Image[5];
            while (true) {
                Random random = new Random();
                int x = random.Next(1, 1266);
                int y = random.Next(1, 668);
                
                int btn_x = btn_disagree.Location.X;
                int btn_y = btn_disagree.Location.Y;

                
                int abs = Math.Abs(x - btn_x) / (x - btn_x);
                if (abs == 1) {
                    lin = im_runR;
                }
                if (abs == -1) {
                    lin = im_runL;
                }
                if (Math.Abs(x - btn_x) >= Math.Abs(y - btn_y) || true) {
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
                if (Math.Abs(x - btn_x) < Math.Abs(y - btn_y)) {
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
                    break;
                }
            }
            while (true) {
                if (index >= 4) {
                    index = 0;
                } else {
                    index++;
                }
                btn_disagree.Image = lin[index];
                Thread.Sleep(100);
            }
        }
        private void Changespeed() {
            Thread.Sleep(1000);
            speed = speed / 2;//10
            Thread.Sleep(1000);
            speed = speed / 2;//5
            while (true) {
                Thread.Sleep(2000);//2,1,0
                speed = speed / 2;
                if (speed == 0) {
                    break;
                }
            }
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
        private void Mes() {
            while (true) {
                while (isShowMes) {
                    Thread.Sleep(2000);
                    lab_mes.Text = "";
                    isShowMes = false;
                }
            }
        }
    }
}
