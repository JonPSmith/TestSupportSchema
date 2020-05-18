﻿// Copyright (c) 2020 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;
using Test.Database1;
using Test.Database2;
using TestSupport.Helpers;
using TestSupportSchema;
using Xunit;

namespace Test.UnitTests
{
    public class TestWipeSchema
    {
        [Fact]
        public void TestDatabase1SchemaChangeToDatabase2Ok()
        {
            //SETUP
            var connectionString = this.GetUniqueDatabaseConnectionString();
            var builder1 = new DbContextOptionsBuilder<DbContext1>();
            using (var context = new DbContext1(builder1.UseSqlServer(connectionString).Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            var builder2 = new DbContextOptionsBuilder<DbContext2>();
            using (var context = new DbContext2(builder2.UseSqlServer(connectionString).Options))
            {
                //ATTEMPT
                context.EnsureClean();

                //VERIFY
                context.Add(new TopClass2());
                context.SaveChanges();
            }
        }
    }
}