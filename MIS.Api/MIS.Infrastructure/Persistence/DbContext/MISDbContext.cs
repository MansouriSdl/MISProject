using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MIS.Domain.Commons;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.DbContext
{
    public class MISDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public MISDbContext(DbContextOptions<MISDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Bureau> Bureaus { get; set; }
        public DbSet<BureauStockAssignment> BureauStockAssignments { get; set; }
        public DbSet<BureauStockInventory> BureauStockInventories { get; set; }
        public DbSet<ConsumptionType> ConsumptionTypes { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<ServiceEquipmentRequest> ServiceEquipmentRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<QrCode> QrCodes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierType> SupplierTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            var userName = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Admin";

            var addedEntities = ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();
            foreach (var entity in addedEntities)
            {
                if (entity is AuditEntity)
                {
                    var model = entity as AuditEntity;
                    model.CreatedAt = DateTime.Now;
                    model.CreatedBy = userName;
                }
            }
            var modifiedEntities = ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in modifiedEntities)
            {
                if (entity is AuditEntity)
                {
                    var model = entity as AuditEntity;
                    model.LastUpdatedAt = DateTime.Now;
                    model.LastUpdatedBy = userName;
                }
            }
            var deletedEntities = ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Deleted)
                        .Select(t => t.Entity)
                        .ToArray();
            foreach (var entity in deletedEntities)
            {
                if (entity is AuditEntity)
                {
                    var model = entity as AuditEntity;
                    model.DeletedAt = DateTime.Now;
                    model.DeletedBy = userName;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
