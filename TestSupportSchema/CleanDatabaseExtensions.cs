// Copied from SqlServerDatabaseFacadeExtensions class 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore;
using TestSupportSchema.Internal;

namespace TestSupportSchema
{
    public static class CleanDatabaseExtensions
    {
        public static void EnsureClean(this DbContext context)
            => context.Database.CreateExecutionStrategy()
                .Execute(context.Database, database => new SqlServerDatabaseCleaner(context).Clean(database));
    }
}