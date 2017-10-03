﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Concrete
{
   public static class AzureStorageConfiguration
    {
        private static string _baseUrl = "";

        public static string GetImageUrl(string imageName)
        {
            return _baseUrl + imageName;
        }
    }
}
