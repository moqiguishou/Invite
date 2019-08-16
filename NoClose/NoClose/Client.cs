using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoClose {
    public class Client {
        private bool isRun = true;
        private bool isShow = false;
        private IPEndPoint ipe;
        private Socket client_socket;
        private string resiveMesg = "";

        public Client() {
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

        private void MyThread_recive() {
            throw new NotImplementedException();
        }

        private void MyThread_showMesg() {
            
        }
    }
}
