using System.Collections.Generic;
using System.Linq;

using Domain;

using NUnit.Framework;

namespace Tests.Unit
{
    public class EmployeeTests
    {
        [Test]
        public void EmployeeIsEntitledToPaidLeaves() {
            // arrange and act
            var employee = new Employee {
                Benefits = new List<Benefit> {
                    new Leave {
                        Type = LeaveType.Unpaid,
                        AvailableEntitlement = 15
                    }
                }
            };

            // Assert
            var paidLeave = employee.Benefits.OfType<Leave>().FirstOrDefault(l => l.Type == LeaveType.Unpaid);
            Assert.IsNotNull(paidLeave);
            Assert.That(paidLeave.AvailableEntitlement, Is.EqualTo(15));
        }
    }
}
