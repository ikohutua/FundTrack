using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;
using System.Collections.Generic;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class for work with OrganizationsForLayout
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IOrganizationsForLayout" />
    public sealed class OrganizationsForLayoutService : IOrganizationsForLayoutService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsForLayout"/> class.
        /// </summary>
        /// <param name="unitOfWorkParam">The unit of work parameter.</param>
        public OrganizationsForLayoutService(IUnitOfWork unitOfWorkParam)
        {
            this._unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Gets all organizations for search
        /// </summary>
        /// <returns> Collection of OrganizationsForLayout </returns>
        public IEnumerable<OrganizationsForLayout> GetAll()
        {
            //return this._unitOfWork
            //    .OrganizationsListRepository
            //    .GetAll
            //    .Select(o => new OrganizationsForLayout()
            //    {
            //        Id = o.Id,
            //        Name = o.Name
            //    });

            //HARDCODE, because we don`t have common database now
            var model = new List<OrganizationsForLayout>();
            model.Add(new OrganizationsForLayout() { Id = 1, Name = "Наш Солдат" });
            model.Add(new OrganizationsForLayout() { Id = 2, Name = "Крила Фенікса" });
            model.Add(new OrganizationsForLayout() { Id = 3, Name = "Куля В Лоб" });
            model.Add(new OrganizationsForLayout() { Id = 4, Name = "Танкі і Планшеты" });
            model.Add(new OrganizationsForLayout() { Id = 5, Name = "Інша" });
            return model;
        }
    }
}
