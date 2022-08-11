using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRPayment.JSONClasses
{
    internal class RequestJson
    {
        public AuthenticationData AuthenticationData { get; set; }
        public int OpetaionType { get; set; }
        public RequestData RequestData { get; set; }
    }
    internal class RequestData
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int Id { get; set; }
        public string RRN { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }

    }

    internal class AuthenticationData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string TerminalId { get; set; }
        public string ExternalToken { get; set; }

    }

    internal class RequestForPayment : IRequestData
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }

    internal class RequestForGetPaymentState : IRequestData
    {
        public decimal Id { get; set; }
    }

    internal class RequestForReversePayment : IRequestData
    {
        public decimal Id { get; set; }
        public string RRN { get; set; }
    }

    internal class RequestForGetPayments : IRequestData
    {
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
    }

    

    internal interface IRequestData
    {
    }
}
