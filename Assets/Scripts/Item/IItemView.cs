using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IItemView
{    
    void SetSprite(int id);
    void SetSprite(ItemStateTypes type);
    void SetSprite(BoostTypes type);    
    UniTask MoveOn(float time = 0,params Vector3[] path);
    void SetWorldPosition(Vector3 worldPosition);
    Vector3 GetWorldPosition();    
    void Show();   
    UniTask Hide(float speed = 0);
}