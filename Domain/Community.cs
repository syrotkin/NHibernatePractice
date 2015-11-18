using System.Collections;
using System.Collections.Generic;

namespace Domain {

    public class Community : EntityBase {
        public virtual string Name {
            get;
            set;
        }

        public virtual ICollection<Employee> Members {
            get;
            set;
        }

    }

}