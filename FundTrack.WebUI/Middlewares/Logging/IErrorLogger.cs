using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundTrack.WebUI.Middlewares.Logging
{
    public interface IErrorLogger
    {
        void WriteLogInFile(Exception ex);
    }
}
