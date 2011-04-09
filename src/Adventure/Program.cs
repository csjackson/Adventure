using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;
using Adventure.Commands;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;

namespace Adventure
{
    class Program
    {
        private static IWindsorContainer BuildContainer()
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(
                new ArrayResolver(container.Kernel, false));
            container.Register(
                Component.For<ICommandController>().ImplementedBy<CommandController>().LifeStyle.Transient,
                Component.For<IConsoleFacade>().ImplementedBy<ConsoleFacade>().LifeStyle.Transient,
                Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>()
                    .LifeStyle.Transient,
                Component.For<IFormatter>().ImplementedBy<Formatter>().LifeStyle.Transient,
                Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>))
                    .LifeStyle.Transient,
                Component.For<IUnknownInputHandler>().ImplementedBy<UnknownInputHandler>()
                    .LifeStyle.Transient,
                Component.For<IMasterRoomFactory>().ImplementedBy<MasterRoomFactory>().LifeStyle.Transient,
                Component.For<IPlayerFactory>().ImplementedBy<PlayerFactory>().LifeStyle.Transient,
                Component.For<IPlayer>().UsingFactoryMethod(k => k.Resolve<IPlayerFactory>().FindPlayer()).LifeStyle.Singleton,
                Component.For<IMasterRoom>().UsingFactoryMethod(k => k.Resolve<IMasterRoomFactory>().FindRoom()).LifeStyle.Singleton,
                AllTypes.FromAssemblyContaining<EchoCommand>().BasedOn<ICommand>()
                    .Configure(c=> c.LifeStyle.Transient)
                    .WithService.FromInterface(typeof(ICommand))
                );

            return container;
        }
        static void Main(string[] args)
        {
            var container = BuildContainer();
            bool cont = false;
            
            do 
            {
                Console.WriteLine();
                Console.Write("> ");
                var input = Console.ReadLine();
                var cntrl = container.Resolve<ICommandController>();
                cont = cntrl.Parse(input);
                container.Release(cntrl);
            }
            while (cont); ;
        }
    }
}