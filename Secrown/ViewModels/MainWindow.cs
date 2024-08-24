using Secrown.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Secrown.ViewModels
{
    public class MainWindow : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implement
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        private bool _isLock;
        public bool IsLock { 
            get { return _isLock; }
            set { 
                _isLock = value; 
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsUnlock));
            }
        }
        public bool IsUnlock
        {
            get { return !IsLock; }
        }
        public ICommand LockCommand { get; private set; }
        public MainWindow() {
            IsLock = false;
            LockCommand = new RelayCommand(LockWindow);
        }
        private void LockWindow()
        {
            IsLock = true;
        }
    }
}
