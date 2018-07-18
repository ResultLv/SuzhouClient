using client.ViewModel;
using System.Windows.Controls;

namespace client.View
{
    /// <summary>
    /// display.xaml 的交互逻辑
    /// </summary>
    public partial class display : Page
    {
        public display()
        {
            InitializeComponent();
            this.DataContext = new DataViewModel();
        }
    }
}
