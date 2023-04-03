using System;

public class ObservableVariable<T>
{
    public event Action<T, T> OnChanged;
    private T _value;
    public T Value { 
        get { return _value; }
        set {
            T oldValue = _value;
            _value = value; 
            OnChanged?.Invoke(oldValue, value);
        }
    }
    public ObservableVariable()
    {
        Value = default;
    }

    public ObservableVariable(T value)
    {
        Value = value;
    }
    public override string ToString()
    {
        return Value.ToString();
    }
}
