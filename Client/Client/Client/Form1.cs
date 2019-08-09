using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client {
    public partial class Form1 : Form {
        private bool isRun = true;
        private bool isShow = false;
        private IPEndPoint ipe;
        private Socket client_socket;
        private string resiveMesg = "";

        private int testInt = 0;
        public Form1() {
            InitializeComponent();

            string ipStr = "127.0.0.1";
            int port = 8888;
            IPAddress ip = IPAddress.Parse(ipStr);
            ipe = new IPEndPoint(ip, port);
            Thread mythread = new Thread(MyThread_showMesg);
            mythread.Start();
            //创建客户端socket
            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //连接服务器
            try {
                client_socket.Connect(ipe);
                Thread thread = new Thread(MyThread_recive);
                thread.Start();
            } catch (Exception) {

            }

        }

        private void button1_Click(object sender, EventArgs e) {
            Client_SendMesg("I am Big Bee");

        }
        private void Client_SendMesg(string str) {
            //发送消息
            string sendStr = str;
            byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
            client_socket.Close();
            
            try {
                client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client_socket.Connect(ipe);
                client_socket.Send(sendBytes);
            } catch (Exception) {
                textBox1.Text = "Failure";
            }   
        }

        private void MyThread_recive() {
            while (isRun) {
                try {
                    //接收消息
                    string recStr = "";
                    byte[] recBytes = new byte[1024];
                    int bytes = client_socket.Receive(recBytes, recBytes.Length, 0);
                    recStr += Encoding.Unicode.GetString(recBytes, 0, bytes);
                    resiveMesg = "From Server messages: {0}" + recStr;
                    isShow = true;
                } catch (Exception) {

                }
            }
        }

        private void MyThread_showMesg() {
            while (true) {
                while (isShow) {
                    //testInt++;
                    //label1.Text = testInt + " :";
                    label1.Text = resiveMesg;
                    Thread.Sleep(5000);
                    label1.Text = "";
                    resiveMesg = "";
                    isShow = false;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e) {
                isShow = true;
        }

    }
}
