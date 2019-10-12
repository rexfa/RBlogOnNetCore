using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
//using Pomelo.EntityFrameworkCore.MySql;
//using Pomelo.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using RBlogOnNetCore.EF.Domain;
using System.Reflection;

namespace RBlogOnNetCore.EF
{
    public class MysqlContext : DbContext
    {
        //public DbSet<Customer> Customers { set; get; }
        //public DbSet<Blog> Blog { set; get; }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        public MysqlContext(DbContextOptions<MysqlContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseMySql(@"Server=mysql.rexz.me;database=rexblog;uid=root;pwd=rootrex");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var typesToRegister = GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType))
                .Where(z => { return z.GetInterfaces().Where(q => q.Name.Contains("IEntityTypeConfigura")).Count() > 0 ? true : false; })
                .ToArray();
            var entityMethod = typeof(ModelBuilder).GetMethods().Single(x => x.Name == "Entity" &&
                         x.IsGenericMethod &&
                         x.ReturnType.Name == "EntityTypeBuilder`1");
           foreach (var mappingType in typesToRegister)
           {
                var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);
                var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

                dynamic configurationInstance = Activator.CreateInstance(mappingType);
                configurationInstance.GetType().GetMethod("Configure").Invoke(configurationInstance, new[] { entityBuilder });
                //modelBuilder.ApplyConfiguration(configurationInstance);
           }
           //删除动作在 mapping里做设置了，这里不统一强调
           //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
           //{
           //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
           //}

        }
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                Set<TEntity>().Attach(entity);
                return entity;
            }
            else
            {
                return alreadyAttached;
            }
        }
        //public DbSet<RBlogOnNetCore.EF.Domain.Customer> Customer { get; set; }
        #region Methods
        //public string CreateDatabaseScript()
        //{
        //    return ((ObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        //}


        //public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        //{
        //    //add parameters to command
        //    if (parameters != null && parameters.Length > 0)
        //    {
        //        for (int i = 0; i <= parameters.Length - 1; i++)
        //        {
        //            var p = parameters[i] as DbParameter;
        //            if (p == null)
        //                throw new Exception("Not support parameter type");

        //            commandText += i == 0 ? " " : ", ";

        //            commandText += "@" + p.ParameterName;
        //            if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
        //            {
        //                //output parameter
        //                commandText += " output";
        //            }
        //        }
        //    }

        //    var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

        //    //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
        //    bool acd = this.Configuration.AutoDetectChangesEnabled;
        //    try
        //    {
        //        this.Configuration.AutoDetectChangesEnabled = false;

        //        for (int i = 0; i < result.Count; i++)
        //            result[i] = AttachEntityToContext(result[i]);
        //    }
        //    finally
        //    {
        //        this.Configuration.AutoDetectChangesEnabled = acd;
        //    }

        //    return result;
        //}
        //public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        //{
        //    return this.Database.SqlQuery<TElement>(sql, parameters);
        //}


        //public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        //{
        //    int? previousTimeout = null;
        //    if (timeout.HasValue)
        //    {
        //        //store previous timeout
        //        previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
        //        ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
        //    }

        //    var transactionalBehavior = doNotEnsureTransaction
        //        ? TransactionalBehavior.DoNotEnsureTransaction
        //        : TransactionalBehavior.EnsureTransaction;
        //    var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

        //    if (timeout.HasValue)
        //    {
        //        //Set previous timeout back
        //        ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
        //    }

        //    //return result
        //    return result;
        //}
        #endregion
    }
}
