using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernatePractice1.Domain
{
    public class OsyProduct
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Category { get; set; }

        public virtual bool Discontinued { get; set; }

        public override string ToString()
        {
            return Id + ", " + Name + ", " + Category + ", " + (Discontinued ? "Discontinued" : "Available");
        }
    }
}
