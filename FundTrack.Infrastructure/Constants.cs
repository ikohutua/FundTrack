using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure
{
    public static class Constants
    {
        public const int AdminRoleId = 2;
        public const int FinOpTypeIncome = 1;
        public const int FinOpTypeSpending = 0;
        public const int FinOpTypeTransfer = 2;
        public const string BankType = "Банк";
        public const string DefaultAccountType = "Загальний";
        public const string CashFinOpDescription = "Готівкова пожертва";
        public const string DefaultTargetName = "Призначення не вказано";
        public const string BaseTargetName = "Базове призначення";
        public const string DefaultImageName = "default_image.png";
        public const int DefaultImportInterval = 720;
        public const string BaseImageURL = "https://fundrackss.blob.core.windows.net/fundtrackssimages/";

        public const string Anonymous = "<Анонімний>";
        public const int AnyFinOpType = -1;
    }
}
