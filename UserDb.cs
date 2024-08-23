using Microsoft.EntityFrameworkCore;

class UserDb : DbContext
{
    public UserDb(DbContextOptions<UserDb> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }

    public DbSet<SuperAdmin> SuperAdmins { get; set; }
}