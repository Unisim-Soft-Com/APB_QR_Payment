using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRPayment.com.agroprombank.ws;

namespace QRPayment.Model
{
    internal class BankOperations :PropertyChangeNotifier
    {
        private QRMCredentials qrmCredentials;
        private QRMobileService qrMobileService;

        private string terminalId;

        public string TerminalId { get => terminalId; set => Set(ref terminalId, value); }

        public BankOperations(string terminalId, string login, string password, string deviceId = "-1", string pushId = "-1", string externalToken = "")
        {
            SetQRMCredentials(login, password, deviceId, pushId, externalToken);
            TerminalId = terminalId;
            qrMobileService = new QRMobileService();
            qrMobileService.QRMCredentialsValue = qrmCredentials;
        }

        public void SetQRMCredentials(string login, string password, string deviceId = "-1", string pushId = "-1", string externalToken = "")
        {
            qrmCredentials = new QRMCredentials();
            qrmCredentials.Login = login;
            qrmCredentials.Password = password;
            qrmCredentials.DeviceId = deviceId;
            qrmCredentials.PushId = pushId;
            qrmCredentials.ExternalToken = externalToken;
        }

        
        

        public (bool, string, bool) TryCreatePayment(decimal amount, string currencycode)
        {
            try
            {
                return (qrMobileService.CreatePayment(TerminalId, amount, currencycode), "Success", true);
            }
            catch (Exception e)
            {
                return (false, e.Message, false);
            }
        }

        public (decimal, string, bool) TryCreatePaymentId(decimal amount, string currencycode)
        {
            try
            {
                return (qrMobileService.CreatePaymentID(TerminalId, amount, currencycode, ""), "Success", true);
            }
            catch (Exception e)
            {
                return (-1, e.Message, false);
            }
        }

        public (string, bool) TryClosePayment()
        {
            try
            {
                qrMobileService.ClosePayment(TerminalId);
                return ("Success", true);
            }
            catch (Exception e)
            {
                return (e.Message, false);
            }
        }


        public (string, string, bool) TryGetPaymentState(decimal id)
        {
            try
            {
                return (qrMobileService.GetPaymentState(TerminalId, id), "Success", true);
            }
            catch (Exception e)
            {
                return ("", e.Message, false);
            }
        }


        public (string, bool) TryReversePayment(decimal id, string rrn)
        {
            try
            {
                qrMobileService.ReversePayment(TerminalId, id, rrn);
                return ("Success", true);
            }
            catch (Exception e)
            {
                return (e.Message, false);
            }
        }

        public (string, string, bool) TryGetPayments(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            try
            {
                return (qrMobileService.GetPayments(dateTimeFrom, dateTimeTo, TerminalId), "Success", true);
            }
            catch (Exception e)
            {
                return ("", e.Message, false);
            }
        }

    }
}
