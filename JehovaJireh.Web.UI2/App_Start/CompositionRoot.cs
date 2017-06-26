using System;
using Ninject;
using JehovaJireh.Web.UI.DI;
using JehovaJireh.Web.UI.DI.Ninject;
using JehovaJireh.Web.UI.DI.Ninject.Modules;

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

