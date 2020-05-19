// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using Test.Database1;
using Test.Database2;
using TestSupport.EfHelpers;
using TestSupportSchema;
using Xunit;
using Xunit.Abstractions;

namespace Test.UnitTests
{
    public class TestCreateDatabase
    {
        private readonly ITestOutputHelper _output;

        public TestCreateDatabase(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestEnsureDeletedThenCreateDatabase1Ok()
        {
            //SETUP
            var showLog = false;
            var options = this.CreateUniqueClassOptionsWithLogging<DbContext1>(log =>
            {
                if (showLog)
                    _output.WriteLine(log.Message);
            });
            using (var context = new DbContext1(options))
            {
                context.Database.EnsureDeleted();

                //ATTEMPT
                showLog = true;
                context.Database.EnsureClean();
                showLog = false;

                //VERIFY
                context.Add(new TopClass1());
                context.SaveChanges();
            }
        }

        [Fact]
        public void TestEnsureDeletedThenCreateDatabase2Ok()
        {
            //SETUP
            var showLog = false;
            var options = this.CreateUniqueClassOptionsWithLogging<DbContext2>(log =>
            {
                if (showLog)
                    _output.WriteLine(log.Message);
            });
            using (var context = new DbContext2(options))
            {
                context.Database.EnsureDeleted();

                //ATTEMPT
                showLog = true;
                context.Database.EnsureClean();
                showLog = false;

                //VERIFY
                context.Add(new TopClass2());
                context.SaveChanges();
            }
        }
    }
}