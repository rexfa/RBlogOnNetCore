using System;
using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.EF;


namespace RBlogOnNetCore.Services
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int id);
        IList<Role> GetCustomerRols(Customer customer);
        IList<Authorization> GetCustomerAuthorization(Customer customer);

    }
}
