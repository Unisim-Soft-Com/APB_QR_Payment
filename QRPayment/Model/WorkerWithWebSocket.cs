using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QRPayment.Model
{
    internal class WorkerWithWebSocket
    {
        
        static object locker = new object();

        

        private Socket Connect(string ipAdressHost, int portin)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAdressHost), portin);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
            return socket;
        }


        public async Task<(int code, string message, string responce)> ChatWithSocketAsync(string ipAdressHost, int portin, string message)
        {
            return await Task.Run(() => ChatWithSocket( ipAdressHost, portin, message));
        }

        public (int code, string message, string responce) ChatWithSocket(string ipAdressHost, int portin, string message)
        {
            lock (locker)
            {
                try
                {
                    Socket socket = Connect(ipAdressHost, portin);
                    byte[] data = Encoding.UTF8.GetBytes(Encoding.UTF8.GetBytes(message).Length + Environment.NewLine+message);
                    socket.Send(data);

                    data = new byte[256]; // буфер для ответа
                    

                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт
                    
                    do
                    {
                        

                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    } while (socket.Available > 0);
                    
                    if (bytes == 0)
                        return (-2, "Нет ответа от сервера", "");

                    string resp = builder.ToString();

                    resp = resp.Substring(resp.IndexOf('{'));

                    return (0, "Success", resp);
                }
                catch (Exception e)
                {
                    return (-1, e.Message, "");

                }
            }
        }

    }   
}
