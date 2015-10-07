namespace Domain {

    public class Benefit : EntityBase {

        public string Name {
            get;
            set;
        }

        public string Description {
            get;
            set;
        }

        // Bidirectional link
        public Employee Employee {
            get;
            set;
        }

    }

}