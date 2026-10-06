using System.ComponentModel;
using System.Windows.Input;
using TabLayout.ViewModelsp;

namespace TabLayout.ViewModels;

public class TabItem : BaseViewModel, IDisposable
{
    internal TabControlViewModel Owner;
    public Guid Id { get; set; }
    public string Header
    {
        get => field;
        set => SetValue(ref field, value);
    } = string.Empty;

    public bool IsSelected 
    {
        get => field; 
        set => SetValue(ref field, value);
    }
    public bool IsActive
    {
        get => field;
        set => SetValue(ref field, value);
    } = true;

    public object? Content
    {
        get => field;
        set => SetValue(ref field, value);
    }

    public INotifyPropertyChanged? ViewModel { get; private set; }

    public TabItem()
    {

    }

    public TabItem(INotifyPropertyChanged viewmodel = null)
    {
        ViewModel = viewmodel;
    }

    public void Dispose()
    {
        if(Content is IDisposable disposable) disposable.Dispose();
        Owner = null;
        Content = null;
        ViewModel = null;
    }
}
