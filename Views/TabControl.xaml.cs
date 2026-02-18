using System.Windows;
using System.Windows.Controls;
using TabLayout.ViewModels;

namespace TabLayout.Views
{
    /// <summary>
    /// Логика взаимодействия для TabControl.xaml
    /// </summary>
    public partial class TabControl : UserControl, IDisposable
    {
        public TabControlViewModel ViewModel { get; private set; }
        public TabControl(TabControlViewModel vm)
        {
            ViewModel = vm;
            DataContext = ViewModel;
            foreach (var item in ViewModel.Items)
                InitialContent(item);
            ViewModel.Items.CollectionChanged += Items_CollectionChanged;
            InitializeComponent();
        }

        public static readonly DependencyProperty LayoutItemTemplateSelectorProperty = DependencyProperty.Register(nameof(LayoutItemTemplateSelector), typeof(DataTemplateSelector), typeof(TabControl),
            new FrameworkPropertyMetadata(null));

        public TabControl()
        {
            DataContextChanged += TabControl_DataContextChanged;
            InitializeComponent();
        }

        private void InitialContent(ViewModels.TabItem item)
        {
            if(LayoutItemTemplateSelector != null)
            {
                if(item.ViewModel != null)
                {
                    var template = LayoutItemTemplateSelector.SelectTemplate(item, this);
                    if (template != null)
                    {
                        item.Content = template.LoadContent();
                    }
                }
            }
        }

        private void Items_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
                foreach (ViewModels.TabItem item in e.NewItems)
                    InitialContent(item);
        }

        public DataTemplateSelector LayoutItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(LayoutItemTemplateSelectorProperty);
            set => SetValue(LayoutItemTemplateSelectorProperty, value);
        }

        private void TabControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(sender is TabControl { DataContext: TabControlViewModel  vm})
            {
                ViewModel = vm;
                ViewModel.Items.CollectionChanged += Items_CollectionChanged;
                DataContextChanged -= TabControl_DataContextChanged;
                foreach(var item in ViewModel.Items)
                    InitialContent(item);
            }
        }

        public void Dispose()
        {
            ViewModel.Items.CollectionChanged -= Items_CollectionChanged;
            ViewModel = null;
            DataContext = null;
        }
    }
}
