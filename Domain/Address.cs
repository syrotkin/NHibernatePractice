namespace Domain
{
    public class Address : EntityBase
    {
        public string AddressLine1 {
            get;
            set;
        }

        public string AddressLine2 {
            get;
            set;
        }

        public string Postcode {
            get;
            set;
        }

        public string City {
            get;
            set;
        }

        public string Country {
            get;
            set;
        }
    }
}
