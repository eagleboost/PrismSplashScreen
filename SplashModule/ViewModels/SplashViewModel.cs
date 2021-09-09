#region File Info Header
/*________________________________________________________________________________________

  Copyright (C) 2021 Jason Zhang, eagleboost@msn.com

  * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
  * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
  * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

________________________________________________________________________________________*/

#endregion File Info Header

namespace Splash.ViewModels
{
  using System;
  using System.ComponentModel;
  using global::Module.Core;
  using Prism.Events;

  public class SplashViewModel : INotifyPropertyChanged
  {
    #region Declarations
    private string _status;
    #endregion

    #region ctor
    public SplashViewModel(IEventAggregator eventAggregator)
    {
      eventAggregator.GetEvent<MessageUpdateEvent>().Subscribe(e => UpdateMessage(e.Message));
    }
    #endregion

    #region Public Properties
    public string Status
    {
      get { return _status; }
      set
      {
        _status = value;
        NotifyPropertyChanged("Status");
      }
    }
    #endregion

    #region Private Methods
    private void UpdateMessage(string message)
    {
      if (string.IsNullOrEmpty(message))
      {
        return;
      }

      Status += string.Concat(Environment.NewLine, message, "...");
    }
    #endregion

    #region INotifyPropertyChanged Members
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
      var handler = PropertyChanged;
      handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
  }
}
