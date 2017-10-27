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
        public const string CashFinOpDescription = "Готівкова пожертва";
        public const string DefaultTargetName = "Призначення не вказано";
        public const string BaseTargetName = "Базове призначення";
    }
}
