using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class DataContext : DbContext {

    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<TaskModel> Tb_Tasks { get; set; }
}