using System;
using Ninject;
using JehovaJirehWebApp.DI;
using JehovaJirehWebApp.DI.Ninject;
using JehovaJirehWebApp.DI.Ninject.Modules;

internal class CompositionRoot
{
    public static IDependencyInjectionContainer Compose()
    {
// Create the DI container
        var container = new StandardKernel();

// Setup configuration of DI
        container.Load(new MvcSiteMapProviderModule());

// Return our DI container wrapper instance
        return new NinjectDependencyInjectionContainer(container);
    }
}

