///////////////////////////////////////
#region Namespace Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfHelper.Initialization;
using XRF_Data_Analysis_Utilities.View.Windows;
using XRF_Data_Analysis_Utilities.ViewModel.Windows;

#endregion
///////////////////////////////////////

namespace XRF_Data_Analysis_Utilities
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ////////////////////////////////////////
        #region Constructor

        public App()
        {
            WindowInitializer _window = new WindowInitializer(new MainWindow(), new MainWindowViewModel());
            _window.Show();
        }

        #endregion
    }
}
