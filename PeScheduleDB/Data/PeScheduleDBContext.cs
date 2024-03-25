using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeScheduleDB.Models;

    public class PeScheduleDBContext : IdentityDbContext
    {
        public PeScheduleDBContext (DbContextOptions<PeScheduleDBContext> options)
            : base(options)
        {
        }

        public DbSet<PeScheduleDB.Models.Student> Student { get; set; } = default!;

public DbSet<PeScheduleDB.Models.Course> Course { get; set; } = default!;

public DbSet<PeScheduleDB.Models.Teacher> Teacher { get; set; } = default!;

public DbSet<PeScheduleDB.Models.Schedule> Schedule { get; set; } = default!;

public DbSet<PeScheduleDB.Models.Location> Location { get; set; } = default!;
    }
