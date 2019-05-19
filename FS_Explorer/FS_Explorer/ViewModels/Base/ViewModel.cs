using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace FS_Explorer.VievModels.Base
{
    public abstract class ViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public class Command: ICommand
        {
            public delegate void Del();
            Del del;
            #region ICommand Members  
            public Command(Del del)
            {
                this.del = del;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
            public void Execute(object parameter)
            {
                del();
            }
            #endregion
        }
    }
}
