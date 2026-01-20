using Microsoft.EntityFrameworkCore;
using ReactCRUD.Core.Domain;
using System.Collections.Generic;



namespace ReactCRUD.Data
{
    public class ReactCRUDContext : DbContext
    {
        public ReactCRUDContext(DbContextOptions<ReactCRUDContext> options)
        : base(options) 
        {
        }

        public DbSet<School> Schools { get; set; }
    }
}