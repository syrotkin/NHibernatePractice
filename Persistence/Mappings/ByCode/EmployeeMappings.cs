using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode {
    public class EmployeeMappings : ClassMapping<Employee> {

        public EmployeeMappings() {
            Id(e => e.Id, mapper => {
                mapper.Generator(Generators.HighLow);
                mapper.Column("Id");
                mapper.Length(10);
                mapper.UnsavedValue(0);
            });
            Property(e => e.EmployeeNumber); // the only mandatory parameter
             
            // can also speicfy optional parameters:
            Property(e => e.DateOfBirth, mapper => {
                mapper.Column("DateOfBirth");
                mapper.Type(NHibernateUtil.Date);
                mapper.NotNullable(false);
                mapper.Lazy(true); // ???
            });

                // Summary: Set: 
                // - which property is the "many" end of the one-to-many
                // - which column on the "many" end is the foreign key to the current class
                // - what is the nature of the association (relation) -- OneToMany in this case
            Set(e => e.Benefits, mapper => {
                mapper.Key(k => k.Column("Employee_Id"));
                mapper.Cascade(Cascade.All);
            },
            relation => relation.OneToMany(
                mapping => mapping.Class(typeof(Benefit)))
            );

            
            OneToOne(e => e.ResidentialAddress, mapper => {
                mapper.Cascade(Cascade.Persist);
                mapper.PropertyReference(a => a.Employee);
            });

        }
    }
}
