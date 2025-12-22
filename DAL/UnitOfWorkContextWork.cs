using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UnitOfWorkContextWork
    {
        public static IUnitOfWork Create(bool useEntityFramework)
        {
            if (useEntityFramework)
            {
                var context = new AppDBContext();
                context.Database.EnsureCreated();
                return new EFWUnitOfWork(context);
            }
            else
            {
                return new DapperUnitOfWork();
            }
        }
    }
}
