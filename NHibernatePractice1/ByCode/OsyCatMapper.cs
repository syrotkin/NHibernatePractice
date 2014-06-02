using NHibernate.Mapping.ByCode;
using NHibernatePractice1.Domain;

namespace NHibernatePractice1.ByCode
{
    public class OsyCatMapper
    {
        public void MapOsyCat()
        {
            var mapper = new ModelMapper();
            mapper.Class<OsyCat>(osyCat =>
            {
                osyCat.Id(x => x.Id, map =>
                {
                    map.Column("OsyCatId");
                    map.Generator(Generators.Guid);

                });
                osyCat.Property(x => x.Name, map =>
                {
                    map.Length(16);
                    map.Column("Name");
                });
                osyCat.Property(x => x.Sex);
                osyCat.Property(x => x.Weight);
            });
        }
    }

    /*
    internal class MyGenerator : IGeneratorDef
    {
        public string Class
        {
            get { throw new NotImplementedException(); }
        }

        public Type DefaultReturnType
        {
            get { return typeof(string); }
        }

        public object Params
        {
            get { throw new NotImplementedException(); }
        }

        public bool SupportedAsCollectionElementId
        {
            get { throw new NotImplementedException(); }
        }
    }
    */
}
