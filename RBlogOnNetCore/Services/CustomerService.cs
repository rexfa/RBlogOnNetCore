using System;
using System.Collections.Generic;
using System.Linq;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.EF;
using Microsoft.Extensions.Caching.Memory;
namespace RBlogOnNetCore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly MysqlContext _mysqlContext;
        //private readonly EfRepository<Blog> _blogRepository;
        private readonly EfRepository<Customer> _customerRepository;
        private readonly EfRepository<Role> _roleRepository;
        private readonly EfRepository<Authorization> _authorizationRepository;
        private readonly EfRepository<CustomerRoleMapper> _customerRoleMapperRepository;
        private readonly EfRepository<RoleAuthorizationMapper> _roleAuthorizationMapperRepository;
        private readonly IMemoryCache _memoryCache;
        

        public CustomerService(MysqlContext mysqlContext, IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            this._mysqlContext = mysqlContext;
            _customerRepository = new EfRepository<Customer>(this._mysqlContext);
            _authorizationRepository = new EfRepository<Authorization>(this._mysqlContext);
            _customerRoleMapperRepository = new EfRepository<CustomerRoleMapper>(this._mysqlContext);
            _roleAuthorizationMapperRepository = new EfRepository<RoleAuthorizationMapper>(this._mysqlContext);
            _roleRepository = new EfRepository<Role>(this._mysqlContext);

        }
        public IList<Authorization> GetCustomerAuthorization(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Role> GetCustomerRols(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
