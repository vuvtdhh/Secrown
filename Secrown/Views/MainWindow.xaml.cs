﻿using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Secrown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Minimize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
        private void Maximize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }
        private void Restore_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
        public MainWindow()
        {
            InitializeComponent();
            InitializeWebViewZalo();
            InitializeWebViewMessenger();
        }
        #region SetWindowDisplayAffinity
        [DllImport("user32.dll")]
        private static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);

        private const uint WDA_NONE = 0x00000000;
        private const uint WDA_MONITOR = 0x00000001;
        private const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

        private void ExcludeWindowFromCapture()
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowDisplayAffinity(hwnd, WDA_EXCLUDEFROMCAPTURE);
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ExcludeWindowFromCapture();
        }
        #region WebView2
        private async void InitializeWebViewZalo()
        {
            await WebViewZalo.EnsureCoreWebView2Async();
            WebViewZalo.CoreWebView2.PermissionRequested += CoreWebView2_PermissionRequested;
            WebViewZalo.Source = new Uri("https://chat.zalo.me/");
        }

        private async void InitializeWebViewMessenger()
        {
            await WebViewMessenger.EnsureCoreWebView2Async();
            WebViewMessenger.CoreWebView2.PermissionRequested += CoreWebView2_PermissionRequested;
            WebViewMessenger.Source = new Uri("https://messenger.com/");
        }

        #endregion
        private void CoreWebView2_PermissionRequested(object? sender, CoreWebView2PermissionRequestedEventArgs e)
        {
            if(e.PermissionKind == CoreWebView2PermissionKind.Notifications)
            {
                e.State = CoreWebView2PermissionState.Allow;
            }
            else
            {
                e.State = CoreWebView2PermissionState.Deny;
            }
        }
    }
}