// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Test.Database1;
using TestSupport.EfHelpers;
using TestSupportSchema;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Test.UnitTests
{
    public class TestExample
    {
        [Fact]
        public void TestExampleOk()
        {
            //SETUP
            var options = this.CreateUniqueClassOptions<DbContext1>();
            using (var context = new DbContext1(options))
            {
                context.EnsureClean();

                //... put your unit tests here

            }
        }
    }
}