using System.Windows.Controls;
using TabLayout.ViewModels;

namespace TabLayout.Views
{
    public partial class TabItemControl : UserControl
    {
        public TabItemControl()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not Border) return;
            if (DataContext is not ViewModels.TabItem item) return;
            if(!item.IsActive) return;
            item.Owner.SelectedItem = item;
            item.IsSelected = true;
        }
    }
}
