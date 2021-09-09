#region File Info Header
/*________________________________________________________________________________________

  Copyright (C) 2021 Jason Zhang, eagleboost@msn.com

  * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
  * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
  * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

________________________________________________________________________________________*/

#endregion File Info Header

namespace Splash
{
  using System;
  using System.Threading;
  using System.Windows.Threading;
  using Events;
  using Interfaces;
  using ViewModels;
  using Views;
  using Prism.Events;
  using Prism.Ioc;
  using Prism.Modularity;
  using Unity;

  public class Module : IModule
  {
    [Dependency]
    public IUnityContainer Container { get; set; }

    [Dependency]
    public IEventAggregator EventAggregator { get; set; }

    [Dependency]
    public IShell Shell { get; set; }

    private AutoResetEvent WaitForCreation { get; set; }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
      
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
      Dispatcher.CurrentDispatcher.BeginInvoke((Action) (() =>
      {
        Shell.Show();
        EventAggregator.GetEvent<CloseSplashEvent>().Publish(new CloseSplashEvent());
      }));

      WaitForCreation = new AutoResetEvent(false);

      void ShowSplash()
      {
        Dispatcher.CurrentDispatcher.BeginInvoke((Action) (() =>
        {
          Container.RegisterType<SplashViewModel, SplashViewModel>();
          Container.RegisterType<SplashView, SplashView>();

          var splash = Container.Resolve<SplashView>();
          EventAggregator.GetEvent<CloseSplashEvent>().Subscribe(e => splash.Dispatcher.BeginInvoke((Action) splash.Close), ThreadOption.PublisherThread, true);

          splash.Show();

          WaitForCreation.Set();
        }));

        Dispatcher.Run();
      }

      var thread = new Thread((ThreadStart) ShowSplash) {Name = "Splash Thread", IsBackground = true};
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();

      WaitForCreation.WaitOne();
    }
  }
}
