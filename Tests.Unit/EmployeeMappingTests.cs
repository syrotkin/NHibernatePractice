using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using Domain;

using NHibernate;
using NHibernate.Util;

using NUnit.Framework;

namespace Tests.Unit {

    [TestFixture]
    public class EmployeeMappingTests {

        private InMemoryDatabaseForXmlMappings m_database;
        private ISession m_session;

        [TestFixtureSetUp]
        public void Setup() {
            m_database = new InMemoryDatabaseForXmlMappings();
            m_session = m_database.Session;
        }

        [Test]
        public void MapsPrimitiveProperties() {
            const string emailAddress = "hillary.gamble@corporate.com";
            var employee = new Employee {
                EmployeeNumber = "562342",
                Firstname = "Hillary",
                Lastname = "Gamble",
                EmailAddress = emailAddress,
                DateOfBirth = new DateTime(1980, 4, 23)
            };
            object id = SaveEmployee(employee);
            m_session.Clear();

            using (var transaction = m_session.BeginTransaction()) {
                employee = m_session.Get<Employee>(id);
                Assert.That(employee.EmployeeNumber, Is.EqualTo("562342"));
                Assert.That(employee.Firstname, Is.EqualTo("Hillary"));
                Assert.That(employee.Lastname, Is.EqualTo("Gamble"));
                Assert.That(employee.EmailAddress, Is.EqualTo(emailAddress));
                transaction.Commit();
            }
        }

        [Test]
        public void MapsBenefits() {
            var employeeToSave = CreateEmployeeWithBenefits();
            object id = SaveEmployee(employeeToSave);

            m_session.Clear();

            using (var transaction = m_session.BeginTransaction()) {
                var employee = m_session.Get<Employee>(id);

                Assert.That(employee.Benefits.Count, Is.EqualTo(3));

                var seasonTicketLoan = employee.Benefits.OfType<SeasonTicketLoan>().FirstOrDefault();
                Assert.That(seasonTicketLoan, Is.Not.Null);
                if (seasonTicketLoan != null) {
                    Assert.That(seasonTicketLoan.Employee.EmployeeNumber, Is.EqualTo("123456"));
                }
                // blah blah, similar tests for other kinds of benefits

                transaction.Commit();
            }
        }

        [Test]
        public void MapsResidentialAddress() {
            var employee = CreateEmployeeWithAddress();
            object id = SaveEmployee(employee);

            m_session.Clear();

            using (var transaction = m_session.BeginTransaction()) {
                employee  = m_session.Get<Employee>(id);
                Assert.That(employee.ResidentialAddress.City, Is.EqualTo("Phildelphia"));

                transaction.Commit();
            }
        }

        [Test]
        public void MapsCommunities() {

            var employee = CreateEmployeeWithCommunities();
            object id = SaveEmployee(employee);

            m_session.Clear();

            using (var transaction = m_session.BeginTransaction()) {
                employee = m_session.Get<Employee>(id);
                Assert.That(employee.Communities.Count, Is.EqualTo(2));
                Assert.That(employee.Communities.First().Members.First().EmployeeNumber, Is.EqualTo(employee.EmployeeNumber));
                transaction.Commit();
            }
        }
        
        
        private object SaveEmployee(
                Employee employee) {
            object id;
            using (var transation = m_session.BeginTransaction()) {
                id = m_session.Save(employee);
                transation.Commit();
            }
            return id;
        }
        
        private Employee CreateEmployeeWithCommunities() {
            var employee = new Employee {
                EmployeeNumber = "123",
                Communities = new HashSet<Community> {
                    new Community {
                        Name = "Community 1"
                    },
                    new Community {
                        Name = "Community 2"
                    }
                }
            };
            return employee;
        }

        private static Employee CreateEmployeeWithBenefits() {
            var employeeToSave = new Employee {
                EmployeeNumber = "123456",
                Benefits = new HashSet<Benefit> {
                    new SkillsEnhancementAllowance {
                        Entitlement = 1000,
                        RemainingEntitlement = 250
                    },
                    new SeasonTicketLoan {
                        Amount = 1416,
                        MonthlyInstalment = 118,
                        StartDate = new DateTime(2014, 4, 25),
                        EndDate = new DateTime(2015, 3, 25)
                    },
                    new Leave {
                        AvailableEntitlement = 30,
                        RemainingEntitlement = 15,
                        Type = LeaveType.Sick
                    }
                }
            };
            return employeeToSave;
        }

        private static Employee CreateEmployeeWithAddress() {
            var employee = new Employee {
                EmployeeNumber = "123",
                ResidentialAddress = new Address {
                    AddressLine1 = "3901 Locust Walk",
                    City = "Philadelphia",
                    Postcode = "19104",
                    Country = "US"
                }
            };
            return employee;
        }

    }

}