using FundTrack.BLL.Concrete;
using FundTrack.DAL.Concrete;
using FundTrack.DAL.Repositories;
using FundTrack.DAL.Tests;
using FundTrack.Infrastructure.ViewModel;
using Xunit;

namespace FundTrack.BLL.Tests
{
    /// <summary>
    /// Test class for RegisterOrganizationService
    /// </summary>
    public sealed class RegisterOrganizationServiceTest
    {
        private FundTrackContext _context;
        private FakeFundTrackDbContextBaseBuilder _fakeBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterOrganizationServiceTest"/> class.
        /// </summary>
        public RegisterOrganizationServiceTest()
        {
            this._fakeBuilder = new FakeFundTrackDbContextBaseBuilder();
            this._context = this._fakeBuilder.GetFakeContext();
        }

        /// <summary>
        /// Creates new record in Organization Repository, adding 
        /// records to AddressRepository, OrgAddressRepository, MembershipRepository
        /// SCRIPT - Setup fake data base context with test data, reposirory and unitOfWork.
        /// Result returning record with error messages
        /// </summary>
        //[Fact]
        //public void CreateOrganization_SetupFakeDbContext_CreatesOrganizationRegistrationViewModel()
        //{
        //    //Arrange
        //    var organizationRepository = new OrganizationRepository(_context);
        //    var addressRepository = new AddressRepository(_context);
        //    var orgAddressRepository = new OrgAddressRepository(_context);
        //    var membershipRepository = new MembershipRepository(_context);
        //    var usersRepository = new UserRepository(_context);
        //    var roleRepository = new RoleRepository(_context);
        //    var unitOfWork = new UnitOfWork(_context, 
        //        null, 
        //        usersRepository, 
        //        null, 
        //        organizationRepository, 
        //        membershipRepository, 
        //        addressRepository,  
        //        orgAddressRepository,
        //        null,
        //        null, 
        //        null,
        //        roleRepository,
        //        null,
        //        null,
        //        null,
        //        null,
        //        null,
        //        null,
        //        null,
        //        null,
        //        null);

        //    var registerService = new OrganizationRegistrationService(unitOfWork);

        //    var organizationToCreate = new OrganizationRegistrationViewModel {Id = 5, Name = "Volunteers Union", Description = "Some description", City = "Lviv",
        //        Country = "Ukraine", House = "5", Street = "Zelena", AdministratorLogin = "admin1",
        //        NameError = string.Empty, UserError = string.Empty };

        //    var secondOrganizationToCreate = new OrganizationRegistrationViewModel
        //    {
        //        Id = 6,
        //        Name = "Name 1",
        //        Description = "Description of organization",
        //        City = "Lviv",
        //        Country = "Ukraine",
        //        House = "3",
        //        Street = "Sunny",
        //        AdministratorLogin = "admin1",
        //        NameError = string.Empty,
        //        UserError = string.Empty
        //    };

        //    //Act
        //    var result = registerService.RegisterOrganization(organizationToCreate);
        //    var secondResult = registerService.RegisterOrganization(secondOrganizationToCreate);

        //    //Assert
        //    Assert.False(result.UserError == string.Empty);
        //    Assert.True(secondResult.NameError == "Організація з такою назвою вже існує");
        //}
    }
}
