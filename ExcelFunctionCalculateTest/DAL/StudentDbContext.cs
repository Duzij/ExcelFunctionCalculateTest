﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ExcelFunctionCalculateTest.DAL
{
    public class StudentDbContext : IdentityDbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
