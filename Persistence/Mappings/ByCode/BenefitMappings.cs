using Domain;

using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode {

    public class BenefitMappings : ClassMapping<Benefit> {

        public BenefitMappings() {
            Id(b => b.Id);

            // Don't have to configure this because the OneToMany is already configured
            ManyToOne(b => b.Employee, mapping => {
                mapping.Class(typeof(Employee));
                mapping.Column("Employee_Id");
            });
        }

    }

}