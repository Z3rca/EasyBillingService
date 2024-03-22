using System;

namespace EasyBillingService
{
    public static class FileFormatHelper
    {
        public static String FileFormatDisplay(string infoText, string fileExtension)
        {
            return infoText + "|" + "*" + fileExtension;
        }
    }
}