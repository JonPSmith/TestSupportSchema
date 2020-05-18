// Copied from SqlServerDatabaseFacadeExtensions class and altered by GitHun @JonPSmith
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;
using TestSupportSchema.Internal;

namespace TestSupportSchema
{
    public static class CleanDatabaseExtensions
    {
        /// <summary>
        /// Calling this will wipe the database schema down to nothing in the database and then calls
        /// Database.EnsureCreated to create the correct database schema based on your EF Core Context
        /// </summary>
        /// <param name="context">The DbContext linked to the Sql Server database you want to clean</param>
        public static void EnsureClean(this DbContext context)
            => context.Database.CreateExecutionStrategy()
                .Execute(context.Database, database => new SqlServerDatabaseCleaner(context).Clean(database));
    }
}