#region File Info Header
/*________________________________________________________________________________________

  Copyright (C) 2021 Jason Zhang, eagleboost@msn.com

  * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
  * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
  * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

________________________________________________________________________________________*/

#endregion File Info Header

namespace Splash.Behaviours
{
  using System.Windows;

  public class SplashBehaviour
  {
    #region Dependency Properties
    public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
      "Enabled", typeof (bool), typeof (SplashBehaviour), new PropertyMetadata(OnEnabledChanged));

    public static bool GetEnabled(DependencyObject obj)
    {
      return (bool) obj.GetValue(EnabledProperty);
    }

    public static void SetEnabled(DependencyObject obj, bool value)
    {
      obj.SetValue(EnabledProperty, value);
    }
    #endregion

    #region Event Handlers
    private static void OnEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
      if (obj is Window splash && args.NewValue is bool isEnabled && isEnabled)
      {
        splash.Closed += (s, e) =>
        {
          splash.DataContext = null;
          splash.Dispatcher.InvokeShutdown();
        };
        
        splash.MouseDoubleClick += (s, e) => splash.Close();
        splash.MouseLeftButtonDown += (s, e) => splash.DragMove();
      }
    }
    #endregion
  }
}
