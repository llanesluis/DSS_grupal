using DSS_Scoring.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS_Scoring.Data;

public partial class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
