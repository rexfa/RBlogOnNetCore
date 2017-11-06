using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Utils;

namespace RBlogOnNetCore
{
    public class Startup
    {
        private string _authenticationSchemeSetting = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var EFSetting = Configuration.GetSection("ConnectionStrings")["MysqlConnection"];
            this._authenticationSchemeSetting = Configuration.GetSection("AuthenticationSetting")["DefaultScheme"];
            //CheckBaseData();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MysqlContext>(options => options.UseMySql(Configuration.GetSection("ConnectionStrings")["MysqlConnection"]));
            services.AddAuthentication(this._authenticationSchemeSetting)
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public void CheckBaseData()
        {
            var options = new DbContextOptions<MysqlContext>();
            MysqlContext _context = new MysqlContext(options);
            IRepository<Customer> _customerRepository = new EfRepository<Customer>(_context);
            var firstCustomer = _customerRepository.Table.FirstOrDefault();
            if (firstCustomer == null)
            {
                Guid guid = new Guid();
                string salt = "1234";
                string password = SecurityTools.MD5Hash("123qwe" + salt);
                Customer customer = new Customer() { name = "Blogowner", createdOn = DateTime.Now,password = password,salt =salt,guid = guid.ToByteArray() };
                _customerRepository.Insert(customer);
                _context.SaveChanges();
            }
        }
    }
}
