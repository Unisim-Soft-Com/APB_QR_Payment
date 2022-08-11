using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRPayment.Model
{
    internal class PaymentRequests : PropertyChangeNotifier
    {
        private ObservableCollection<PaymentRequest> paymentRequestList;

        public ObservableCollection<PaymentRequest> PaymentRequestList { get => paymentRequestList; set => Set(ref paymentRequestList, value); }

        public PaymentRequests()
        {
            PaymentRequestList = new ObservableCollection<PaymentRequest>();
        }
    }

    internal class PaymentRequest : PropertyChangeNotifier
    {

        private int id;
        private decimal amount;
        private string currencyCode;
        private string state;
        private int stateCode;
        private string referenceNumber;
        private string dataCreate;
        public int ID { get => id; set => Set(ref id, value); }

        public decimal Amount { get => amount; set => Set(ref amount, value); }

        public string CurrencyCode { get => currencyCode; set => Set(ref currencyCode, value); }
        public string State { get => state; set => Set(ref state, value); }
        public int StateCode { get => stateCode; set => Set(ref stateCode, value); }

        public string ReferenceNumber { get => referenceNumber; set => Set(ref referenceNumber, value); }
        public string DataCreate { get => dataCreate; set => Set(ref dataCreate, value); }


    }



}

