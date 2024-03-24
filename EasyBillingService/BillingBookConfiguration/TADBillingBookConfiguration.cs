using System;
using System.Collections.Generic;
using System.Linq;
using EasyBillingService.Extensions;
using Microsoft.Office.Interop.Excel;

namespace EasyBillingService.BillingBookConfiguration
{
    public class TADBillingBookConfiguration : BaseBillingBookConfiguration
    {
        public override string RetrieveCell(Worksheet worksheet,List<Entry> addressData, double address,DateTime date)
        {
            if (!addressData.Any())
            {
                return "";
            }
            var lastEntry = addressData[0];
            
            var monthDate = DateTime.Today.Month;

            if (lastEntry.DateTime.Month == monthDate)
            {
                return "A"+(lastEntry.RowNumber+1);
            }
            else
            {
                var grid = worksheet.Range["A1", "B1000"].Value as object[,];
                for (int i = 1; i < grid.GetLength(0); i++)
                {
                    var obj = grid[i, 1];
                    if (obj is DateTime)
                    {
                        var entryDate = (DateTime)obj;

                        if (entryDate.Month == monthDate)
                        {
                            return "A" + (i+1);
                        }
                    }
                }
            }
            return "";
        }

        public override (string billingID, string date, string sum, string recipient) GetRowStructure()
        {
            (string billingID, string date, string sum, string recipient) structure = new ValueTuple<string, string, string, string>();
            structure.billingID = "A";
            structure.date = "B";
            structure.sum = "D";
            structure.recipient = "C";
            return structure;
        }
    }
}