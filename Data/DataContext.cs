using Microsoft.EntityFrameworkCore;
using ToDoListAPI.MODELS;

namespace ToDoListAPI.Data
{
    public class DataContext : Dbcontext
    {
        public DataContext (DbContextOptions<DataContext> options): base(options){}
        public DbSet<ToDoItem> ToDoItems {get; set;}
    }
}