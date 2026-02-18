namespace TabLayout.Commands;

public class RelayCommand : CommandBase
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(string commandName, Action execute, Func<bool> canExecute = null)
    {
        CommandName = commandName;
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute();
    }

    public override void Execute(object? parameter)
    {
        _execute();
    }
}

public class RelayCommand<T> : CommandBase
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool> _canExecute;

    public RelayCommand(string commandName, Action<T> execute, Func<T, bool> can = null)
    {
        CommandName= commandName;
        _execute = execute ?? throw new ArgumentNullException( nameof(execute));
        _canExecute = can;
    }

    public override bool CanExecute(object? parameter)
    {
        if(parameter == null && typeof(T).IsValueType) return false;

        return _canExecute == null || _canExecute((T)parameter);
    }

    public override void Execute(object? parameter)
    {
        _execute((T)parameter);
    }
}
