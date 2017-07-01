using FundTrack.DAL.Concrete;
using Microsoft.EntityFrameworkCore;
using System;

namespace FundTrack.DAL.Tests
{
    /// <summary>
    /// Class for creating fake data base context for testing
    /// </summary>
    public sealed class FakeDbContextBuilder
    {
        /// <summary>
        /// Gets the fake context.
        /// </summary>
        /// <returns> FundTrackContext - fake instance </returns>
        public static FundTrackContext GetFakeContext()
        {
            var options = new DbContextOptionsBuilder<FundTrackContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString())
                 .Options;
            var context = new FundTrackContext(options);

            //Create fake data for
            //Organizations
            context.Organizations.Add(new Entities.Organization { Id = 1, Name = "Name 1", Description = "Description 1", });
            context.Organizations.Add(new Entities.Organization { Id = 2, Name = "Name 2", Description = "Description 2", });
            context.Organizations.Add(new Entities.Organization { Id = 3, Name = "Name 3", Description = "Description 3", });
            //EventImages
            context.EventImages.Add(new Entities.EventImage { Id = 1, EventId = 1, ImageUrl = "EventId = 1" });
            //Events
            context.Events.Add(new Entities.Event { Id = 1, Description = "Event Description", OrganizationId = 1 });
            context.SaveChanges();
            return context;
        }
    }
}
