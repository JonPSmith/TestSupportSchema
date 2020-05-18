// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Test.Database1;
using TestSupport.EfHelpers;
using TestSupportSchema;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests
{
    public class TestTimings
    {
        private readonly ITestOutputHelper _output;

        public TestTimings(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task TestTimingsSmallDatabaseOk()
        {
            //SETUP
            var options = this.CreateUniqueClassOptions<DbContext1>();
            using (var context = new DbContext1(options))
            using (new TimeThings(_output, "EnsureDeleted/EnsureCreated"))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            using (var context = new DbContext1(options))
            {
                //ATTEMPT
                using (new TimeThings(_output, "EnsureClean"))
                {
                    context.EnsureClean();
                }

                //VERIFY
                context.Add(new TopClass1());
                context.SaveChanges();
            }
            using (var context = new DbContext1(options))
            {
                //ATTEMPT
                using (new TimeThings(_output, "Respawn"))
                {
                    var checkpoint = new Checkpoint();
                    await checkpoint.Reset(context.Database.GetDbConnection().ConnectionString);
                    context.EnsureClean();
                }

                //VERIFY
                context.TopClasses.Count().ShouldEqual(0);
            }
            using (var context = new DbContext1(options))
            {
                //ATTEMPT
                using (new TimeThings(_output, "EnsureClean"))
                {
                    context.EnsureClean();
                }

                //VERIFY
                context.Add(new TopClass1());
                context.SaveChanges();
            }
            using (var context = new DbContext1(options))
            {
                //ATTEMPT
                using (new TimeThings(_output, "Respawn"))
                {
                    var checkpoint = new Checkpoint();
                    await checkpoint.Reset(context.Database.GetDbConnection().ConnectionString);
                    context.EnsureClean();
                }

                //VERIFY
                context.TopClasses.Count().ShouldEqual(0);
            }
            using (var context = new DbContext1(options))
            using (new TimeThings(_output, "EnsureDeleted/EnsureCreated"))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}