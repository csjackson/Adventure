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
                Component.For<CommandController>().LifeStyle.Transient,
                Component.For<IConsoleFacade>().ImplementedBy<ConsoleFacade>().LifeStyle.Transient,
                Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>()
                    .LifeStyle.Transient,
                Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>))
                    .LifeStyle.Transient,
                Component.For<IUnknownInputHandler>().ImplementedBy<UnknownInputHandler>()
                    .LifeStyle.Transient,
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
                var cntrl = container.Resolve<CommandController>();
                cont = cntrl.Parse(input);
                container.Release(cntrl);
            }
            while (cont); ;
        }
    }
}