using System;
using System.Collections.Generic;
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
        static void Main(string[] args) {
            //配置端口
            int port = 8888;//端口
            string ipStr = "127.0.0.1";
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

                    throw;
                }
                
            }
        }

        private static void Mythread() {
            while (isRun) {
                try {
                    client_socket = server_socket.Accept();

                    //接收来自客户端的消息
                    string name = "";
                    byte[] recbyte = new byte[1024];
                    int bytes = client_socket.Receive(recbyte, recbyte.Length, 0);//字节流 
                    name += Encoding.ASCII.GetString(recbyte, 0, bytes);          //--> 字符流
                    Console.WriteLine("====={0} 连接成功=====", name);
                } catch (Exception) {

                }
            }
            
        }
    }
}
