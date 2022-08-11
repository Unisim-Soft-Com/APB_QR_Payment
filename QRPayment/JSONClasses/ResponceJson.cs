using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRPayment.JSONClasses
{
    internal class ResponceJson
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public ResponceData ResponceData { get; set; }
        public ObservableCollection<ResponceData> ResponceDataList { get; set; }
        public string XmlList { get; set; }
    }

    internal class ResponceData
    {
        public int Id { get; set; }
        public string RRN { get; set; }
        public string XmlResponce { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string DataCreate { get; set; }

    }
}
