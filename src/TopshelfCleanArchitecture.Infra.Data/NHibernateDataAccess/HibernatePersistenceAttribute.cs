using System;
using TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess;

[assembly: HibernatePersistence]
namespace TopshelfCleanArchitecture.Infra.Data.NHibernateDataAccess
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class HibernatePersistenceAttribute : Attribute
    {
    }
}
