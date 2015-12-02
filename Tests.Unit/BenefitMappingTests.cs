using System;
using System.Runtime;

using Domain;

using NHibernate;

using NUnit.Framework;

namespace Tests.Unit {

    public class BenefitMappingTests {

        private InMemoryDatabaseForXmlMappings m_database;
        private ISession m_session;

        [TestFixtureSetUp]
        public void SetUp() {
            m_database = new InMemoryDatabaseForXmlMappings();
            m_session = m_database.Session;
        }


        [Test]
        public void MapsSkillsEnhancementAllowance() {
             object id = 0;

             using (var transaction = m_session.BeginTransaction()) {
                 var allowance = new SkillsEnhancementAllowance {
                     Name = "Skills Enhancement Allowance",
                     Description = "Money to pay for courses for employees",
                     Entitlement = 1000,
                     RemainingEntitlement = 250
                 };
                 id = m_session.Save(allowance);
                 transaction.Commit();
             }

            m_session.Clear();

            using (var transaction = m_session.BeginTransaction()) {
                var benefit = m_session.Get<Benefit>(id); // !!! Reference as Benefit, not as SkilsEnhancementAllowance -- polymorphic association (inheritance mapping)
                Assert.IsNotNull(benefit);
                var allowance = benefit as SkillsEnhancementAllowance;
                if (allowance != null) {
                    Assert.AreEqual(250, allowance.RemainingEntitlement);
                    Assert.AreEqual(1000, allowance.Entitlement);    
                }
                transaction.Commit();
            }

         }

    }

}