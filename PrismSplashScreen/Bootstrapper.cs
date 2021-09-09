#region File Info Header
/*________________________________________________________________________________________

  Copyright (C) 2021 Jason Zhang, eagleboost@msn.com

  * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
  * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
  * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

________________________________________________________________________________________*/

#endregion File Info Header

namespace PrismSplashScreen
{
  using System.Windows;
  using Prism.Ioc;
  using Prism.Modularity;
  using Prism.Unity;
  using Splash.Interfaces;

  public class Bootstrapper : PrismBootstrapper
  {
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterSingleton<IShell, Shell>();
    }

    protected override DependencyObject CreateShell()
    {
      var shell = Container.Resolve<IShell>();
      return (DependencyObject)shell;
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
      moduleCatalog.AddModule<Splash.Module>();
      moduleCatalog.AddModule<Module1.Module1>();
      moduleCatalog.AddModule<Module2.Module2>();
      moduleCatalog.AddModule<Module3.Module3>();
    }
  }
}