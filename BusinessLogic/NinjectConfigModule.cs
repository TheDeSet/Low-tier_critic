using BusinessLogic.Services;
using DataAccessLayer;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class NinjectConfigModule : NinjectModule
    {
        private readonly bool useEF;

        public NinjectConfigModule(bool useEF)
        {
            this.useEF = useEF;
        }
        public override void Load()
        {
            if (useEF)
            {
                Bind<IUnitOfWork>().To<EFWUnitOfWork>().InSingletonScope();
            }
            else
            {
                Bind<IUnitOfWork>().To<DapperUnitOfWork>().InSingletonScope();
            }

            Bind<IGameService>().To<GameService>();
            Bind<IReviewService>().To<ReviewService>();
        }
    }
}
