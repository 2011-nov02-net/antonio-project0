using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApplication.DataAccess
{
    public static class Mapper_Customer
    {
        public static Entities.Customer MapCustomerWithLocation(Library.Models.Customer customer)
        {
            return new Entities.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.ID,
                Location = Mapper_Location.Map(customer.MyStoreLocation)
            };
        }

        /// <summary>
        /// This turns a customer Model into a customer entity, by assigning each relavent property
        /// to a column in the customer table
        /// </summary>
        /// <param name="customer">The customer model.</param>
        /// <returns></returns>
        public static Entities.Customer Map(Library.Models.Customer customer)
        {
            return new Entities.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.ID,
                LocationId = customer.MyStoreLocation.ID
            };
        }

        /// <summary>
        /// This turns a customer Entity into a customer model.
        /// </summary>
        /// <param name="customer">The customer entity.</param>
        /// <returns></returns>
        public static Library.Models.Customer Map(Entities.Customer customer)
        {
            return new Library.Models.Customer
            {
                ID = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

    }
}
