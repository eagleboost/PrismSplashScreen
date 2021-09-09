#region File Info Header
/*________________________________________________________________________________________

  Copyright (C) 2021 Jason Zhang, eagleboost@msn.com

  * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
  * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
  * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

________________________________________________________________________________________*/

#endregion File Info Header

namespace Module.Core
{
  using System.Threading;
  using Prism.Events;
  using Prism.Ioc;
  using Prism.Modularity;
  using Unity;

  public abstract class ModuleBase : IModule
  {
    [Dependency]
    public IEventAggregator EventAggregator { get; set; }
    
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
      EventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent {Message = GetType().Name});
      ////Sleep for 2 seconds to simulate time consuming module initializaiton
      Thread.Sleep(2000);
    }
  }
}