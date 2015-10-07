using System;
using System.Collections.Generic;

namespace Domain {

    public class Employee : EntityBase {

        public string EmployeeNumber {
            get;
            set;
        }
        public string Firstname {
            get;
            set;
        }
        public string Lastname {
            get;
            set;
        }
        public string EmailAddress {
            get;
            set;
        }
        public DateTime DateOfBirth {
            get;
            set;
        }
        public DateTime DateOfJoining {
            get;
            set;
        }

        public Address ResidentialAddress {
            get;
            set;
        }
        public bool IsAdmin {
            get;
            set;
        }
        public string Password {
            get;
            set;
        }
        public ICollection<Benefit> Benefits {
            get;
            set;
        }

    }

}