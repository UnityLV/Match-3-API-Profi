using System;

public interface IInit<T> where T : Delegate
{
    public void Initialize(T @delegate);
    public void Deinitialize(T @delegate);

}