using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace EasyBillingService.BillingBookConfiguration
{
    public abstract class BaseBillingBookConfiguration
    {
        public abstract string RetrieveCell(Worksheet worksheet,List<Entry> addressData, double address, DateTime date);

        public abstract (string billingID, string date, string sum, string recipient) GetRowStructure();
    }
}