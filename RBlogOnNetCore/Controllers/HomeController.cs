using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace RBlogOnNetCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        public HomeController(MysqlContext context)
        {
            //var options = new DbContextOptions<MysqlContext>();

            //this._context = new MysqlContext(options);
            this._context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
        }
        public IActionResult Index()
        {
            /*
            var mappingInterface = typeof(IEntityTypeConfiguration<>);
            var mappingTypes1 = GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType))
                .Where(z=> { return z.GetInterfaces().Where(q => q.Name.Contains("IEntityTypeConfigura")).Count() > 0 ? true : false; })
                .ToArray();
            var t2 = mappingTypes1[1].GetInterfaces()[0].Name;
            var mappingTypes = GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace)).ToArray();
            //.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<>)).ToArray();
            bool  t = typesToRegister[7].BaseType.IsGenericType;
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                //modelBuilder.ApplyConfiguration(configurationInstance);
            }
            */
            var firstCustomer = _customerRepository.Table.FirstOrDefault();
            //var firstCustomer = this._context.Customers.FirstOrDefault();
            if (firstCustomer != null)
            {
                ViewData["fristCustomerName"] = firstCustomer.name;
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
