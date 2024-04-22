using LagDaemon.YAMUD.Desltop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Desltop.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IView _currentView;

        public MainWindowViewModel() 
        { 
            CurrentView = new LoginView(new LoginViewModel());
        }

        public IView CurrentView 
        { 
            get { return _currentView; }
            set { 
                _currentView = value; 
                OnPropertyChanged(); 
            }

        }

    }
}
