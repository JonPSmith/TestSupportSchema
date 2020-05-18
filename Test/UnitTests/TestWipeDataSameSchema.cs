// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Test.Database1;
using Test.Database2;
using TestSupport.EfHelpers;
using TestSupportSchema;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests
{
    public class TestWipeDataSameSchema
    {
        [Fact]
        public void TestWipeDataDatabase1Ok()
        {
            //SETUP
            var options = this.CreateUniqueMethodOptions<DbContext1>();
            using (var context = new DbContext1(options))
            {
                context.Database.EnsureCreated();
                context.Add(new TopClass1{ Dependents =  new List<Dependent1>{ new Dependent1()}});
                context.SaveChanges();
                context.TopClasses.Count().ShouldEqual(1);
                context.Dependents.Count().ShouldEqual(1);

                //ATTEMPT
                context.EnsureClean();

                //VERIFY
                context.TopClasses.Count().ShouldEqual(0);
                context.Dependents.Count().ShouldEqual(0);
            }
        }

        [Fact]
        public void TestWipeDataDatabase2Ok()
        {
            //SETUP
            var options = this.CreateUniqueMethodOptions<DbContext2>();
            using (var context = new DbContext2(options))
            {
                context.Database.EnsureCreated();
                context.Add(new TopClass2 { Dependents = new List<Dependent2> { new Dependent2() } });
                context.SaveChanges();
                context.TopClasses.Count().ShouldEqual(1);
                context.Dependents.Count().ShouldEqual(1);

                //ATTEMPT
                context.EnsureClean();

                //VERIFY
                context.TopClasses.Count().ShouldEqual(0);
                context.Dependents.Count().ShouldEqual(0);
            }
        }
    }
}