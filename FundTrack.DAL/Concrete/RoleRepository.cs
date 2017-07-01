using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Repository from entity Role
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IRoleRepository" />
    public class RoleRepository : IRoleRepository
    {
        private readonly FundTrackContext context;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RoleRepository(FundTrackContext context)
        {
            this.context = context;
        }

        public int GetIdRole(string name)
        {
            return this.context.Roles
                               .FirstOrDefault(r => r.Name == name)
                               .Id;
        }
    }
}
