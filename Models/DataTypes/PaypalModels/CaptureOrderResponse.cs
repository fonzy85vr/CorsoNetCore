using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class CaptureOrderResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public List<CapturePurchaseUnit> purchase_units { get; set; }
    }

    public class CapturePurchaseUnit
    {
        public string reference_id { get; set; }
        public Payment payments { get; set; }
    }

    public class Payment
    {
        public List<Capture> captures { get; set; }
    }

    public class Capture
    {
        public string id { get; set; }
        public string status { get; set; }
        public Amount amount { get; set; }
    }
}