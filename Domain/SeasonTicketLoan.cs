using System;

namespace Domain {

    public class SeasonTicketLoan : Benefit {

        public int Amount {
            get;
            set;
        }

        public double MonthlyInstalment {
            get;
            set;
        }

        public DateTime StartDate {
            get;
            set;
        }

        public DateTime EndDate {
            get;
            set;
        }

    }

}