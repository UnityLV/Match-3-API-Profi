using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour, IItemView
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _stateSpriteRenderer;

    [SerializeField] private ItemStateTypesSpriteBinds _stateSpriteBinds;
    [SerializeField] private BoostSpriteBinds _boostSpriteBinds;   

    [SerializeField] private Sprite[] _idSprites;

    private Dictionary<BoostTypes, Sprite> _boostSpriteDictionary;
    private Dictionary<ItemStateTypes, Sprite> _stateSpriteDictionary;
        

    private void Awake()
    {
        ConstructDictionaryes();
    }

    public async UniTask MoveOn(float moveDuration,params Vector3[] path)
    {  
        for (int pointIndex = 0; pointIndex < path.Length; pointIndex++)
        {
            bool isLastPoint = pointIndex == path.Length - 1;
            if (isLastPoint)
            {
                await NormalMove(path[pointIndex], moveDuration * 2).SetEase(Ease.OutBack).AsyncWaitForCompletion();
                break;
            }
            await NormalMove(path[pointIndex], moveDuration).SetEase(Ease.Linear).AsyncWaitForCompletion();
        }        
    }

    private Tween NormalMove(Vector3 path, float moveDuration)
    {
        return transform.DOMove(path, moveDuration);
    }    

    public void SetWorldPosition(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public virtual async UniTask Hide(float hideTime = 0.1f)
    {
        await transform.DOScale(0, hideTime).AsyncWaitForCompletion();        
    }

    public virtual void Show()
    {
        transform.localScale = Vector3.zero;
        float showTime = 0.1f;
        gameObject.SetActive(true);
        transform.DOScale(1, showTime);
    }

    public void SetSprite(int id) => _spriteRenderer.sprite = GetSpriteBy(id);

    public void SetSprite(ItemStateTypes state) => _stateSpriteRenderer.sprite = GetSpriteBy(state);

    public void SetSprite(BoostTypes type) => _spriteRenderer.sprite = GetSpriteBy(type);

    private Sprite GetSpriteBy(BoostTypes type) => _boostSpriteDictionary[type];

    private Sprite GetSpriteBy(int id)
    {
        if (id == GameConstatns.NullId)
        {
            return null;
        }
        return _idSprites[id];
    }

    private Sprite GetSpriteBy(ItemStateTypes state) => _stateSpriteDictionary[state];

    private void ConstructDictionaryes()
    {
        ConstructBoostDictionaryes();
        ConstructStateDictionaryes();
    }

    private void ConstructBoostDictionaryes()
    {
        _boostSpriteDictionary = new();

        foreach (var bind in _boostSpriteBinds.Binds)
            _boostSpriteDictionary.Add(bind.Type, bind.Sprite);
    }

    private void ConstructStateDictionaryes()
    {
        _stateSpriteDictionary = new();

        foreach (var bind in _stateSpriteBinds.Binds)
            _stateSpriteDictionary.Add(bind.State, bind.Sprite);
    }

}
