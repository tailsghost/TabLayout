using System.Collections.ObjectModel;
using TabLayout.ViewModelsp;

namespace TabLayout.ViewModels;

public class TabControlViewModel : BaseViewModel, IDisposable
{
    public ObservableCollection<TabItem> Items { get; } = [];

    public TabControlViewModel()
    {
        Items.CollectionChanged += Items_CollectionChanged;
    }

    private void Items_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
            foreach (TabItem item in e.NewItems)
                item.Owner = this;
    }
     
    public TabItem SelectedItem 
    { 
        get => field; 
        set
        {
            if(SetValue(ref field, value))
            {
                foreach(var item in Items)
                {
                    if(item == value)
                    {
                        item.IsSelected = true;
                    }
                    else
                    item.IsSelected = false;
                }
            }
        }
    }

    public void Dispose()
    {
        foreach (var item in Items)
        {
            item.Dispose();
        }
        Items.Clear();
        SelectedItem = null;
        Items.CollectionChanged -= Items_CollectionChanged;
    }
}
