using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Abstract
{
    public interface IRoleRepository
    {
        int GetIdRole(string name);
    }
}
