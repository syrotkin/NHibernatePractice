using NHibernate;
using NHibernate.Cfg;
using NHibernatePractice1.Domain;
using NHibernatePractice1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernatePracticeTest
{
    class ProductTester
    {
        internal void Run()
        {
            throw new NotImplementedException();
        }

        private void CheckQueryByName()
        {
            string name = "Apple";
            var repository = new ProductRepository();
            var product = repository.GetByName(name);
            Console.WriteLine(product == null ? "product null" : product.ToString());
        }

        private void CheckQueryMultiple()
        {
            string category = "Fruit";
            var repository = new ProductRepository();
            ICollection<OsyProduct> products = repository.GetByCategoryCriteria(category);
            Console.WriteLine("number of products found: " + products.Count);
        }

        private void CheckCanDeleteProduct()
        {
            var product = new OsyProduct() { Name = "Coke", Category = "Drinks" };
            IProductRepository repository = new ProductRepository();
            repository.Add(product);

            Guid oldId = product.Id;

            Console.WriteLine(string.Format("Removing product with id: {0}", oldId));
            repository.Remove(product);

            OsyProduct result = repository.GetById(oldId);
            Console.WriteLine(result != null ? string.Format("product with id {0} is in DB", oldId) :
                string.Format("product with id {0} is not in DB", oldId));
        }

       

        private void AddProduct()
        {
            var product = new OsyProduct { Name = "Apple", Category = "Fruit" };
            IProductRepository repository = new ProductRepository();
            repository.Add(product);

            OsyProduct fromDb = repository.GetById(product.Id);
            if (product.Id == fromDb.Id && 
                product.Name == fromDb.Name && 
                product.Category == fromDb.Category &&
                product.Discontinued == fromDb.Discontinued)
            {
                Console.WriteLine("Equal");
            }
            else
            {
                Console.WriteLine("Not equal");
            }
            Console.WriteLine(Object.ReferenceEquals(product, fromDb) ? "references equal" : "references not equal");
        }

    }
}
