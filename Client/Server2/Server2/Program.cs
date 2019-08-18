using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server2 {
    class Program {
        //private Dictionary<int,s>

        private static Socket client_socket;
        private static Socket server_socket;
        private static bool isRun = true;
        private static string path = @"F:\邀请函\Invite\Client\Server2\Server2\ini.ini";
        private static string path_advice = @"F:\邀请函\Invite\Client\Server2\Server2\adivice.txt";
        static void Main(string[] args) {
            //ini init
            iniInit();
            //配置端口
            int port = 8888;//端口
            string ipStr = "127.0.0.1";
            //string ipStr = "192.168.46.182";
            IPAddress ip = IPAddress.Parse(ipStr);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            //创建socket
            server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定端口
            server_socket.Bind(ipe);
            Console.WriteLine("=====绑定端口{0}=====", ipe);
            //开始监听
            server_socket.Listen(10);//设置最大监听数为1
            Console.WriteLine("=====Server Start and Waitting connection=====");
            Thread thread = new Thread(Mythread);
            thread.Start();
            while (true) {
                try {
                    string strInput = Console.ReadLine();
                    byte[] sendBytes = Encoding.Unicode.GetBytes(strInput);
                    client_socket.Send(sendBytes);
                    Console.WriteLine("======向{0}发送一条消息:{1}======", "ta", strInput);
                } catch (Exception) {

                }
                
            }
        }

        private static void iniInit() {
            Myini.WriteIniData("First", "result", "0", path);
            Myini.WriteIniData("JianTing", "ReStart", "0", path);
            Myini.WriteIniData("JianTing", "Flash", "0", path);
            Myini.WriteIniData("JianTing", "Stop", "0", path);
            Myini.WriteIniData("JianTing", "Exit", "0", path);
            Myini.WriteIniData("JianTing", "DisAgree", "0", path);
            Myini.WriteIniData("JianTing", "Agree", "0", path);
        }

        private static void Mythread() {
            while (isRun) {
                try {
                    client_socket = server_socket.Accept();

                    //接收来自客户端的消息
                    string name = "";
                    byte[] recbyte = new byte[1024];
                    int bytes = client_socket.Receive(recbyte, recbyte.Length, 0);//字节流 
                    name += Encoding.Unicode.GetString(recbyte, 0, bytes);          //--> 字符流
                    Console.WriteLine("====={0} 连接成功=====", name);
                    if (name.Substring(0,1).Equals("1")) {
                        Myini.ADDIniData("JianTing", "ReStart",1,path);
                    }
                    if (name.Substring(0, 1).Equals("2")) {
                        Myini.ADDIniData("JianTing", "Flash", 1, path);
                    }
                    if (name.Substring(0, 1).Equals("3")) {
                        Myini.ADDIniData("JianTing", "Stop", 1, path);
                    }
                    if (name.Substring(0, 1).Equals("4")) {
                        Myini.ADDIniData("JianTing", "Exit", 1, path);
                    }

                    if (name.Substring(0, 1).Equals("5")) {
                        Myini.ADDIniData("JianTing", "DisAgree", 1, path);
                        string result = Myini.ReadIniData("First", "result", "0", path);
                        if (result.Equals("0")) {
                            Myini.WriteIniData("First", "result","-1",path);
                        }
                    }
                    if (name.Substring(0, 1).Equals("6")) {
                        Myini.ADDIniData("JianTing", "Agree", 1, path);
                        string result = Myini.ReadIniData("First", "result", "0", path);
                        if (result.Equals("0")) {
                            Myini.WriteIniData("First", "result", "1", path);
                        }
                    }
                    if (name.Substring(0, 1).Equals("7")) {
                        FileStream fs = new FileStream(path_advice, FileMode.Append);
                        byte[] data = System.Text.Encoding.Default.GetBytes(name+"\n");
                        fs.Write(data,0,data.Length);
                        fs.Flush();
                        fs.Close();
                        Console.WriteLine("====={0}", name);

                    }
                    if (name.Substring(0, 1).Equals("8")) {
                        FileStream fs = new FileStream(path_advice, FileMode.Append);
                        byte[] data = System.Text.Encoding.Default.GetBytes(name + "\n");
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                        fs.Close();
                    }
                    if (name.Substring(0, 1).Equals("9")) {
                        FileStream fs = new FileStream(path_advice, FileMode.Append);
                        byte[] data = System.Text.Encoding.Default.GetBytes(name + "\n");
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                        fs.Close();
                    }
                } catch (Exception) {

                }
            }
            
        }
    }
}
