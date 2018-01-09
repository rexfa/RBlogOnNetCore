using System;
using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.EF;


namespace RBlogOnNetCore.Services
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int id);
        IList<Role> GetCustomerRoles(Customer customer, bool recache = false);
        IList<Authorization> GetCustomerAuthorization(Customer customer, bool recache = false);
        IList<Authorization> GetRoleAuthorization(Role role);
        void ClearCustomersCache(string key = null);
        bool IsAdmin(Role role);

    }
}
