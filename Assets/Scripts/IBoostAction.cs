using Cysharp.Threading.Tasks;

public interface IBoostAction
{
    UniTask Execute(IItem item);
}
