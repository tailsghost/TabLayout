using System.Windows.Input;

namespace TabLayout.Commands;

public abstract class CommandBase : IDisposable, ICommand
{
    public bool IsDispose { get; set; }
    public object? CommandParametet { get; set; }
    public string CommandName { get; set; }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public abstract bool CanExecute(object? parameter);

    public abstract void Execute(object? parameter);

    public void Dispose()
    {
        if(IsDispose) return;
        CommandParametet = null;
        IsDispose = true;
    }
}
