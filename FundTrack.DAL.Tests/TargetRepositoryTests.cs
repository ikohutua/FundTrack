using FundTrack.DAL.Concrete;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using Xunit;

namespace FundTrack.DAL.Tests
{
    public sealed class TargetRepositoryTests
    {
        private FundTrackContext _context;
        private FakeFundTrackDbContextBaseBuilder _fakeBuilder;

        public TargetRepositoryTests()
        {
            this._fakeBuilder = new FakeFundTrackDbContextBaseBuilder();
            this._fakeBuilder.SetTargets();
            this._context = this._fakeBuilder.GetFakeContext();
        }

        [Fact]
        public void CreateNewTarget_FakeDbContext()
        {
            //Arrange
            var repository = new TargetRepository(this._context);

            //Act
            var item = new Entities.Target { Id = 5, TargetName = "Приладдя", OrganizationId = 3 };
            var result = repository.Create(item);
            this._context.SaveChanges();

            //Assert
            Assert.IsType<Entities.Target>(result);
            Assert.Equal(item.TargetName, result.TargetName);
            Assert.True(this._context.Targets.Local.Count == 4);
        }

        [Fact]
        public void GetTargetByName_TargetNotNull()
        {
            //Arrange
            var repository = new TargetRepository(this._context);
            const string targetName = "Харчі";

            //Act
            var target = repository.GetTargetByName(targetName) as Target;

            //Assert
            Assert.NotNull(target);
        }

        public void TargetNameEqualHarchi()
        {
            //Arrange
            var repository = new TargetRepository(this._context);
            const string targetName = "Харчі";

            //Act
            var target = repository.GetTargetByName(targetName) as Target;

            //Assert
            Assert.Equal(targetName, target.TargetName);
        }


        [Fact]
        public void ReadAllTargetsNotEmpty()
        {
            //Arrange
            var repository = new TargetRepository(this._context);

            //Act
            var targets = repository.Read() as IEnumerable<Target>;

            //Assert
            Assert.NotEmpty(targets);
        }

        [Fact]
        public void GetTargetById_TargetNotNull()
        {
            //Arrange
            var repository = new TargetRepository(this._context);

            //Act
            var target = repository.GetTargetById(1);

            //Assert
            Assert.NotNull(target);
        }

        [Fact]
        public void GetTargetById_TargetIdEqualsResultId()
        {
            //Arrange
            var repository = new TargetRepository(this._context);

            //Act
            var target = repository.GetTargetById(1);

            //Assert
            Assert.Equal(1, target.Id);
        }

        [Fact]
        public void Update_TargetFieldsAreEqualWithResultFields()
        {
            //Arrange
            var repository = new TargetRepository(this._context);

            //Act
            var item = new Entities.Target { Id = 1, TargetName = "Приладдя", OrganizationId = 3 };
            var target = repository.Update(item);
            this._context.SaveChanges();

            //Assert
            Assert.Equal(1, target.Id);
            Assert.Equal(item.TargetName, target.TargetName);
            Assert.Equal(3, target.OrganizationId);
        }

        [Fact]
        public void Delete_TargetNotExists()
        {
            //Arrange
            var repository = new TargetRepository(this._context);

            //Act
            var count = _context.Targets.Local.Count;
            repository.Delete(3);
            this._context.SaveChanges();

            //Assert
            Assert.Equal(count - 1, _context.Targets.Local.Count);
        }
    }
}
