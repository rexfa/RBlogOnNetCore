using System;
using System.Collections.Generic;
using System.Linq;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.Configuration;
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
        public IList<Authorization> GetCustomerAuthorization(Customer customer,bool recache)
        {
            string cacheKey = RBMemCacheKeys.CUSTOMERAUTHORIZATIONKEY + customer.Id.ToString();
            if (recache)
                ClearCustomersCache(cacheKey);
            var authorizations = _memoryCache.GetOrCreate(cacheKey, entry => {
                var roles = GetCustomerRols(customer);
                List<Authorization> aus = new List<Authorization>();
                foreach (var role in roles)
                {
                    var ats = GetRoleAuthorization(role);
                    aus.AddRange(ats);
                }
                return aus; 
            });
            return authorizations;
        }

        public Customer GetCustomerById(int id)
        {
            var customer = _customerRepository.GetById(id);
            return customer;
        }
        public IList<Authorization> GetRoleAuthorization(Role role)
        {
            List<RoleAuthorizationMapper> roleAuthorizationMappers = _roleAuthorizationMapperRepository.Table.Where(ra => ra.RoleId == role.Id).ToList();
            List<Authorization> authorizations = new List<Authorization>();
            foreach (var roleAuthorizationMapper in roleAuthorizationMappers)
            {
                var authorization = _authorizationRepository.GetById(roleAuthorizationMapper.AuthorizationId);
                if (authorization == null)
                    authorizations.Add(authorization);
            }
            return authorizations;
        }
        public IList<Role> GetCustomerRols(Customer customer)
        {
            List<CustomerRoleMapper> customerRoleMappers = _customerRoleMapperRepository.Table.Where(cr => cr.CustomerId == customer.Id).ToList();
            List<Role> roles = new List<Role>();
            foreach (var customerRoleMapper in customerRoleMappers)
            {
                var role = _roleRepository.GetById(customerRoleMapper.RoleId);
                if (role == null)
                    roles.Add(role);
            }
            return roles;
        }
        public void ClearCustomersCache(string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                var ids = _customerRepository.Table.Select(x => x.Id).ToList();
                foreach (int id in ids)
                {
                    _memoryCache.Remove(RBMemCacheKeys.CUSTOMERAUTHORIZATIONKEY+id.ToString());
                }
            }
            else
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
