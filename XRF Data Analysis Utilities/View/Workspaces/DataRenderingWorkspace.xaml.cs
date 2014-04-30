using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XRF_Data_Analysis_Utilities.View.Workspaces
{
    /// <summary>
    /// Interaction logic for DataRenderingWorkspace.xaml
    /// </summary>
    public partial class DataRenderingWorkspace : UserControl
    {
        public DataRenderingWorkspace()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem != null)
            {
                (sender as DataGrid).ScrollIntoView((sender as DataGrid).SelectedItem);
            }
        }
    }
}
