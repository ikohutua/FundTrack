﻿using System;

namespace FundTrack.WebUI.Middlewares.Logging
{
    public interface IErrorLogger
    {
        void WriteLogInFile(Exception ex);
    }
}
