using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Newtonsoft.Json;
using QRPayment.com.agroprombank.ws;
using QRPayment.Commands;
using QRPayment.JSONClasses;
using QRPayment.Model;

namespace QRPayment.ViewModel
{
    internal class VMmainWindow :PropertyChangeNotifier
    {

        private PaymentRequests paymentRequests;
        private PaymentRequest selectedPaymentRequest;
        private string login;
        private string password;
        private string idTerminal;
        private string token;
        private decimal amount;
        private string currencycode;
        private DateTime dateTimeFrom;
        private DateTime dateTimeTo;
        private string paymentsXml;

        private PaymentRequest currentPaymentRequest;

        public PaymentRequests PaymentRequests { get => paymentRequests; set => Set(ref paymentRequests, value); }
        public PaymentRequest SelectedPaymentRequest { get => selectedPaymentRequest; set => Set(ref selectedPaymentRequest, value); }

        public string Login { get => login; set => Set(ref login, value); }
        public string Password { get => password; set => Set(ref password, value); }
        public string IdTerminal { get => idTerminal; set => Set(ref idTerminal, value); }
        public string Token { get => token; set => Set(ref token, value); }
        public decimal Amount { get => amount; set => Set(ref amount, value); }
        public string Currencycode { get => currencycode; set => Set(ref currencycode, value); }

        public DateTime DateTimeFrom { get => dateTimeFrom; set => Set(ref dateTimeFrom, value); }
        public DateTime DateTimeTo { get => dateTimeTo; set => Set(ref dateTimeTo, value); }

        public string PaymentsXml { get => paymentsXml; set => Set(ref paymentsXml, value); }

        public PaymentRequest CurrentPaymentRequest { get => currentPaymentRequest; set => Set(ref currentPaymentRequest, value); }

        

        public ICommand CreatePaymentId_Command { get; private set; }
        private bool CanCreatePaymentIdExecute(object p)
        {
            return true;
        }
        private void CreatePaymentIdExecute(object p)
        {
            CreatePaymentId();
        }

        public ICommand CreatePayment_Command { get; private set; }
        private bool CanCreatePaymentExecute(object p)
        {
            return true;
        }
        private void CreatePaymentExecute(object p)
        {
            CreatePayment();
        }

        public ICommand ClosePayment_Command { get; private set; }
        private bool CanClosePaymentExecute(object p)
        {
            return true;
        }
        private void ClosePaymentExecute(object p)
        {
            ClosePayment();
        }

        public ICommand GetPaymentState_Command { get; private set; }
        private bool CanGetPaymentStateExecute(object p)
        {
            return true;
        }
        private void GetPaymentStateExecute(object p)
        {
            GetPaymentState();
        }

        public ICommand GetPayments_Command { get; private set; }
        private bool CanGetPaymentsExecute(object p)
        {
            return true;
        }
        private void GetPaymentsExecute(object p)
        {
            GetPayments();
        }

        public ICommand ReversePayment_Command { get; private set; }
        private bool CanReversePaymentExecute(object p)
        {
            return true;
        }
        private void ReversePaymentExecute(object p)
        {
            ReversePayment();
        }

        public ICommand CreatePaymentIdWebSocket_Command { get; private set; }
        private bool CanCreatePaymentIdWebSocketExecute(object p)
        {
            return true;
        }
        private void CreatePaymentIdWebSocketExecute(object p)
        {
            CreatePaymentIdWebSocket();
        }

        

        public ICommand CreatePaymentWebSocket_Command { get; private set; }
        private bool CanCreatePaymentWebSocketExecute(object p)
        {
            return true;
        }
        private void CreatePaymentWebSocketExecute(object p)
        {
            CreatePaymentWebSocket();
        }
        

        public ICommand ClosePaymentWebSocket_Command { get; private set; }
        private bool CanClosePaymentWebSocketExecute(object p)
        {
            if (SelectedPaymentRequest == null || SelectedPaymentRequest.ID == 0)
                return false;
            return true;
        }
        private void ClosePaymentWebSocketExecute(object p)
        {
            ClosePaymentWebSocket(SelectedPaymentRequest);
        }

        public ICommand CloseCurrentPaymentWebSocket_Command { get; private set; }
        private bool CanCloseCurrentPaymentWebSocketExecute(object p)
        {
            if (CurrentPaymentRequest == null || CurrentPaymentRequest.ID == 0)
                return false;
            return true;
        }
        private void CloseCurrentPaymentWebSocketExecute(object p)
        {
            ClosePaymentWebSocket(CurrentPaymentRequest);
        }

        public ICommand GetPaymentStateWebSocket_Command { get; private set; }
        private bool CanGetPaymentStateWebSocketExecute(object p)
        {
            return true;
        }
        private void GetPaymentStateWebSocketExecute(object p)
        {
            GetPaymentStateWebSocket(SelectedPaymentRequest);
        }

        public ICommand GetCurrentPaymentStateWebSocket_Command { get; private set; }
        private bool CanGetCurrentPaymentStateWebSocketExecute(object p)
        {
            return true;
        }
        private void GetCurrentPaymentStateWebSocketExecute(object p)
        {
            GetPaymentStateWebSocket(CurrentPaymentRequest);
        }


        public ICommand GetPaymentsWebSocket_Command { get; private set; }
        private bool CanGetPaymentsWebSocketExecute(object p)
        {
            return true;
        }
        private void GetPaymentsWebSocketExecute(object p)
        {
            GetPaymentsWebSocket();
        }
        

        public ICommand ReversePaymentWebSocket_Command { get; private set; }
        private bool CanReversePaymentWebSocketExecute(object p)
        {
            if (SelectedPaymentRequest == null 
                || SelectedPaymentRequest.ID == 0 
                || SelectedPaymentRequest.ReferenceNumber == "")
                return false;
            return true;
        }
        private void ReversePaymentWebSocketExecute(object p)
        {
            ReversePaymentWebSocket(SelectedPaymentRequest);
        }
        public ICommand CloseServer_Command { get; private set; }
        private bool CanCloseServerExecute(object p)
        {
            return true;
        }
        private void CloseServerExecute(object p)
        {
            CloseServer();
        }
        public ICommand CreatePaymentIdWebSocketAndWaitStatus_Command { get; private set; }
        private bool CanCreatePaymentIdWebSocketAndWaitStatusExecute(object p)
        {
            return true;
        }
        private void CreatePaymentIdWebSocketAndWaitStatusExecute(object p)
        {
            CreatePaymentIdWebSocketAndWaitStatus();
        }

        

        public VMmainWindow()
        {
            RegistryCommand();
            IdTerminal = "QRT226787970352034";
            Login = "hnk@kvint.md";
            Password = "987963";
            DateTimeFrom = DateTime.Today;
            DateTimeTo = DateTime.Today;
            PaymentRequests = new PaymentRequests();
        }

        private void RegistryCommand()
        {
            CreatePaymentId_Command = new LambdaCommand(CreatePaymentIdExecute, CanCreatePaymentIdExecute);
            CreatePayment_Command = new LambdaCommand(CreatePaymentExecute, CanCreatePaymentExecute);
            ClosePayment_Command = new LambdaCommand(ClosePaymentExecute, CanClosePaymentExecute);
            GetPaymentState_Command = new LambdaCommand(GetPaymentStateExecute, CanGetPaymentStateExecute);
            GetPayments_Command = new LambdaCommand(GetPaymentsExecute, CanGetPaymentsExecute);
            ReversePayment_Command = new LambdaCommand(ReversePaymentExecute, CanReversePaymentExecute);


            CreatePaymentIdWebSocket_Command = new LambdaCommand(CreatePaymentIdWebSocketExecute, CanCreatePaymentIdWebSocketExecute);
            CreatePaymentWebSocket_Command = new LambdaCommand(CreatePaymentWebSocketExecute, CanCreatePaymentWebSocketExecute);
            ClosePaymentWebSocket_Command = new LambdaCommand(ClosePaymentWebSocketExecute, CanClosePaymentWebSocketExecute);
            CloseCurrentPaymentWebSocket_Command = new LambdaCommand(CloseCurrentPaymentWebSocketExecute, CanCloseCurrentPaymentWebSocketExecute);
            GetPaymentStateWebSocket_Command = new LambdaCommand(GetPaymentStateWebSocketExecute, CanGetPaymentStateWebSocketExecute);
            GetCurrentPaymentStateWebSocket_Command = new LambdaCommand(GetCurrentPaymentStateWebSocketExecute, CanGetCurrentPaymentStateWebSocketExecute);
            GetPaymentsWebSocket_Command = new LambdaCommand(GetPaymentsWebSocketExecute, CanGetPaymentsWebSocketExecute);
            ReversePaymentWebSocket_Command = new LambdaCommand(ReversePaymentWebSocketExecute, CanReversePaymentWebSocketExecute);
            CreatePaymentIdWebSocketAndWaitStatus_Command = new LambdaCommand(CreatePaymentIdWebSocketAndWaitStatusExecute, CanCreatePaymentIdWebSocketAndWaitStatusExecute);
            CloseServer_Command = new LambdaCommand(CloseServerExecute, CanCloseServerExecute);
        }

        public void Test()
        {
            QRMCredentials credentials = new QRMCredentials();
            credentials.Login = "hnk@kvint.md";
            credentials.Password = "987963";
            credentials.DeviceId = "-1";
            credentials.PushId = "-1";
            credentials.ExternalToken = "";

            QRMobileService qrMobileService = new QRMobileService();
            qrMobileService.QRMCredentialsValue = credentials;
            try
            {
                bool responce = qrMobileService.CreatePayment("QRT226787970352034", 2, "000");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //CreatePaymentResponse resp = client.CreatePaymentAsync(credentials, "-1", 1, "000").Result;
        }


        public void CreatePaymentId()
        {

            BankOperations bankOperations = new BankOperations(IdTerminal, Login, Password);
            var (operationId, message, isComplete) = bankOperations.TryCreatePaymentId(Convert.ToDecimal(Amount), Currencycode);

            if (!isComplete)
            {
                MessageBox.Show("Ошибка: " + Environment.NewLine + message, "Ошибка");
                return;
            }
        }

        public void CreatePayment()
        {

            BankOperations bankOperations = new BankOperations(IdTerminal, Login, Password);
            var (operationId, message, isComplete) = bankOperations.TryCreatePayment(Convert.ToDecimal(Amount), Currencycode);

            if (!isComplete)
            {
                MessageBox.Show("Ошибка: " + Environment.NewLine + message, "Ошибка");
                return;
            }

            
        }

        private void ClosePayment()
        {
            BankOperations bankOperations = new BankOperations(IdTerminal, Login, Password);
            var (message, isComplete) = bankOperations.TryClosePayment();

            if (!isComplete)
            {
                MessageBox.Show("Ошибка: " + Environment.NewLine + message, "Ошибка");
                return;
            }
        }

        private void GetPaymentState()
        {
            if (SelectedPaymentRequest == null ) return;
            BankOperations bankOperations = new BankOperations(IdTerminal, Login, Password);
            var (responcePaymentsXml, message, isComplete) = 
                bankOperations.TryGetPaymentState(Convert.ToDecimal(SelectedPaymentRequest.ID));

            if (!isComplete)
            {
                MessageBox.Show("Ошибка: " + Environment.NewLine + message, "Ошибка");
                return;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responcePaymentsXml);
            XmlNodeList xmlNodePayments = xmlDocument.GetElementsByTagName("root");
            SelectedPaymentRequest.ReferenceNumber = xmlNodePayments[0]["trxreferencenumber"].InnerText;


            //SelectedPaymentRequest.Status = status;


            PaymentsXml = responcePaymentsXml;

        }


        private void GetPayments()
        {
            BankOperations bankOperations = new BankOperations(IdTerminal, Login, Password);
            var (responcePaymentsXml, message, isComplete) = bankOperations.TryGetPayments(DateTimeFrom, DateTimeTo);

            if (!isComplete)
            {
                MessageBox.Show("Ошибка: " + Environment.NewLine + message, "Ошибка");
                return;
            }

            PaymentsXml = responcePaymentsXml;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responcePaymentsXml);

            XmlNodeList xmlNodePayments = xmlDocument.GetElementsByTagName("Payment");

            PaymentRequests.PaymentRequestList = new ObservableCollection<PaymentRequest>();
            if (xmlNodePayments.Count > 0)
            {
                foreach (XmlNode xmlNodePayment in xmlNodePayments)
                {
                    //var ID = xmlNodePayment["trxreferencenumber"].InnerText;
                    PaymentRequests.PaymentRequestList.Add(new PaymentRequest()
                    {
                        ID = Convert.ToInt32(xmlNodePayment["qrpaymentid"].InnerText),
                        Amount = Convert.ToDecimal(xmlNodePayment["amount"].InnerText),
                        CurrencyCode = xmlNodePayment["paymentcurrency"].InnerText,
                        State = xmlNodePayment["state"].InnerText,
                        ReferenceNumber = xmlNodePayment["trxreferencenumber"].InnerText
                    });
                }
            }
        }


        private void ReversePayment()
        {
            if (SelectedPaymentRequest == null) return;

            BankOperations bankOperations = new BankOperations(IdTerminal, Login, Password);
            var (message, isComplete) =
                bankOperations.TryReversePayment(Convert.ToDecimal(SelectedPaymentRequest.ID), SelectedPaymentRequest.ReferenceNumber);

            if (!isComplete)
            {
                MessageBox.Show("Ошибка: " + Environment.NewLine + message, "Ошибка");
                return;
            }

        }




        private void CreatePaymentIdWebSocket()
        {
            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                AuthenticationData = new AuthenticationData()
                {
                    Login = this.Login,
                    Password = this.Password,
                    ExternalToken = this.Token,
                    TerminalId = this.IdTerminal
                },
                OpetaionType = 2,
                RequestData = new RequestData()
                {
                    CurrencyCode = this.Currencycode,
                    Amount = this.Amount
                }
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate ((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }

                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson == null)
                    MessageBox.Show("Не удалось преобразовать ответ от сервера");


                if (responceJson.Code != 0)
                {
                    MessageBox.Show(responceJson.Message);
                    return;
                }

                CurrentPaymentRequest = new PaymentRequest()
                {
                    ID = responceJson.ResponceData.Id,
                    Amount = responceJson.ResponceData.Amount,
                    CurrencyCode = responceJson.ResponceData.CurrencyCode
                };
                GetPaymentStateWebSocket(CurrentPaymentRequest);

            };

            operationWithQr.SendTestStringAsync(jsonString);
        }
        private void CreatePaymentWebSocket()
        {
            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                AuthenticationData = new AuthenticationData()
                {
                    Login = this.Login,
                    Password = this.Password,
                    ExternalToken = this.Token,
                    TerminalId = this.IdTerminal
                },
                OpetaionType = 1,
                RequestData = new RequestData()
                {
                    CurrencyCode = "000",
                    Amount = 1
                }
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }

                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson.Code != 0)
                {
                    MessageBox.Show(responceJson.Message);
                    return;
                }

                CurrentPaymentRequest = new PaymentRequest()
                {
                    Amount = responceJson.ResponceData.Amount,
                    CurrencyCode = responceJson.ResponceData.CurrencyCode
                };
            };

            operationWithQr.SendTestStringAsync(jsonString);
        }

        private void GetPaymentStateWebSocket(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.ID == 0)
            {
                MessageBox.Show("Не удалось получить статус. Платеж пустой или неопределен его id");
                return;
            }

            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                AuthenticationData = new AuthenticationData()
                {
                    Login = this.Login,
                    Password = this.Password,
                    ExternalToken = this.Token,
                    TerminalId = this.IdTerminal
                },
                OpetaionType = 4,
                RequestData = new RequestData()
                {
                    Id = paymentRequest.ID
                }
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate ((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }

                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson.Code != 0)
                {
                    MessageBox.Show(responceJson.Message);
                    return;
                }

                paymentRequest.ReferenceNumber = responceJson.ResponceData.RRN;
                paymentRequest.State = responceJson.ResponceData.StatusText;
                paymentRequest.StateCode = responceJson.ResponceData.Status;
                PaymentsXml = responceJson.ResponceData.XmlResponce;
            };

            operationWithQr.SendTestStringAsync(jsonString);


        }

        private void ClosePaymentWebSocket(PaymentRequest paymentRequest)
        {
            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                AuthenticationData = new AuthenticationData()
                {
                    Login = this.Login,
                    Password = this.Password,
                    ExternalToken = this.Token,
                    TerminalId = this.IdTerminal
                },
                OpetaionType = 3
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate ((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }
                
                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson.Code != 0)
                {
                    MessageBox.Show(responceJson.Message);
                    return;
                }

                GetPaymentStateWebSocket(paymentRequest);
            };

            operationWithQr.SendTestStringAsync(jsonString);
        }
        
        private void GetPaymentsWebSocket()
        {
            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                AuthenticationData = new AuthenticationData()
                {
                    Login = this.Login,
                    Password = this.Password,
                    ExternalToken = this.Token,
                    TerminalId = this.IdTerminal
                },
                OpetaionType = 6,
                RequestData = new RequestData()
                {
                    DateTimeFrom = DateTimeFrom,
                    DateTimeTo = DateTimeTo
                }
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate ((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }

                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson.Code != 0)
                {
                    MessageBox.Show(responceJson.Message);
                    return;
                }

                PaymentRequests = new PaymentRequests();
                foreach (ResponceData responceData in responceJson.ResponceDataList)
                {
                    PaymentRequests.PaymentRequestList.Add(new PaymentRequest()
                    {
                        Amount = responceData.Amount,
                        CurrencyCode = responceData.CurrencyCode,
                        ID = responceData.Id,
                        ReferenceNumber = responceData.RRN,
                        State = responceData.StatusText,
                        StateCode = responceData.Status,
                        DataCreate = responceData.DataCreate
                    });
                }

                PaymentsXml = responceJson.XmlList;

            };

            operationWithQr.SendTestStringAsync(jsonString);
        }
        private void ReversePaymentWebSocket(PaymentRequest paymentRequest)
        {
            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                AuthenticationData = new AuthenticationData()
                {
                    Login = this.Login,
                    Password = this.Password,
                    ExternalToken = this.Token,
                    TerminalId = this.IdTerminal
                },
                OpetaionType = 5,
                RequestData = new RequestData()
                {
                    Id = paymentRequest.ID,
                    RRN = paymentRequest.ReferenceNumber
                }
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate ((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }

                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson.Code != 0)
                {
                    MessageBox.Show(answer.message);
                    return;
                }

                GetPaymentStateWebSocket(CurrentPaymentRequest);
            };

            operationWithQr.SendTestStringAsync(jsonString);
        }

        private void CreatePaymentIdWebSocketAndWaitStatus()
        {
            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                AuthenticationData = new AuthenticationData()
                {
                    Login = this.Login,
                    Password = this.Password,
                    ExternalToken = this.Token,
                    TerminalId = this.IdTerminal
                },
                OpetaionType = 7,
                RequestData = new RequestData()
                {
                    CurrencyCode = this.Currencycode,
                    Amount = this.Amount
                }
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate ((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }

                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson == null)
                    MessageBox.Show("Не удалось преобразовать ответ от сервера");


                if (responceJson.Code != 0)
                {
                    MessageBox.Show(responceJson.Message);
                    return;
                }

                CurrentPaymentRequest = new PaymentRequest()
                {
                    ID = responceJson.ResponceData.Id,
                    Amount = responceJson.ResponceData.Amount,
                    CurrencyCode = responceJson.ResponceData.CurrencyCode,
                    State = responceJson.ResponceData.StatusText,
                    StateCode = responceJson.ResponceData.Status
                };
                //GetPaymentStateWebSocket(CurrentPaymentRequest);

            };

            operationWithQr.SendTestStringAsync(jsonString);
        }

        private void CloseServer()
        {
            OperationWithQR operationWithQr = new OperationWithQR();
            RequestJson requestJson = new RequestJson()
            {
                OpetaionType = -1
            };

            string jsonString = JsonConvert.SerializeObject(requestJson);
            operationWithQr.AnswerReceived += delegate ((int code, string message, string responce) answer)
            {
                if (answer.code != 0)
                {
                    MessageBox.Show("Ошибка выполнения запроса к WebServer: " + Environment.NewLine + answer.message);
                    return;
                }

                ResponceJson responceJson = JsonConvert.DeserializeObject<ResponceJson>(answer.responce);

                if (responceJson.Code != 0)
                {
                    MessageBox.Show(answer.message);
                    return;
                }
            };

            operationWithQr.SendTestStringAsync(jsonString);
        }
    }
}
