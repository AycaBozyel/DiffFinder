using DiffFinder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiffFinder.Data
{    public class MvcWebAppDbContext : DbContext
    {
        public MvcWebAppDbContext(DbContextOptions<MvcWebAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<DiffrenceInformation> DiffrenceInformation { get; set; }
        public DbSet<DiffsOffsets> DiffsOffsets { get; set; }
    }
}
