using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernatePractice1.Domain
{
    public interface IProductRepository
    {
        void Add(OsyProduct product);
        void Update(OsyProduct product);
        void Remove(OsyProduct product);
        OsyProduct GetById(Guid productId);
        OsyProduct GetByName(string name); // names are also unique
        ICollection<OsyProduct> GetByCategory(string category);
    }
}
