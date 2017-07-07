using FundTrack.DAL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Tests
{
    /// <summary>
    /// Abstract builder for fake data base context
    /// </summary>
    public abstract class FakeDbContextBuilder
    {
        protected FundTrackContext context { get; private set; }

        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <param name="options">The options.</param>
        public void CreateDbContext(DbContextOptions<FundTrackContext> options)
        {
            context = new FundTrackContext(options);
        }

        /// <summary>
        /// Gets the fake context.
        /// </summary>
        /// <returns> FundTrackContext - fake instance </returns>
        public FundTrackContext GetFakeContext()
        {
            return context;
        }

        /// <summary>
        /// Sets the organizations.
        /// </summary>
        public abstract void SetOrganizations();

        /// <summary>
        /// Sets the events.
        /// </summary>
        public abstract void SetEvents();

        /// <summary>
        /// Sets the event images.
        /// </summary>
        public abstract void SetEventImages();
    }
}
