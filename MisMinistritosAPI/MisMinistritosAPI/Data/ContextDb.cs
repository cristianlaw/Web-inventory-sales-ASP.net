using Microsoft.EntityFrameworkCore;
using MisMinistritosAPI.Models.ModelDb;

namespace MisMinistritosAPI.Data
{
    public class ContextDb : DbContext //Herencia desde DbContext
    {
        public DbSet<ClienteDbModel> ClienteDbModel { get; set; } //Entidad DbSet en referencia a clase Cliente agregado al DbContext
        public DbSet<ProveedorDbModel> ProveedorDbModel { get; set; } 
        public DbSet<ProductoDbModel> ProductoDbModel { get; set; } 
        public DbSet<TourDbModel> TourDbModel { get; set; } 
        public DbSet<FacturaDbModel> FacturaDbModel { get; set; } 
        public DbSet<VentaDbModel> VentaDbModel { get; set; } 



        public ContextDb(DbContextOptions<ContextDb> options) : base(options) //Contructor para añadir desde Program.cs option recibe los parámetros de conexión a SQL
        {

        }

        protected override void OnModelCreating(ModelBuilder builder) //Metodo de sobre escritura 
        {
            base.OnModelCreating(builder);
        }



    }
}
