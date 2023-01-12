﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Data
{
    public class AuthorizationDbContext : IdentityDbContext
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
            : base(options) { }
    }
}
