using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRPayment.Model
{
    internal class OperationWithQR
    {
        private WorkerWithWebSocket workerWithWebSocket;

        internal delegate void AnswerReceivedHandler((int code, string message, string responce) answer);
        internal event AnswerReceivedHandler AnswerReceived;

        public OperationWithQR()
        {
            workerWithWebSocket = new WorkerWithWebSocket();
        }

        public async void SendTestStringAsync(string message)
        {
            var sdf = await workerWithWebSocket.ChatWithSocketAsync("127.0.0.1", 8885, message);
            AnswerReceived(sdf);
        }
    }
}
