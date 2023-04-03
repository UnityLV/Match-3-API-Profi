using UnityEngine;

public interface IBoostItem : IItem
{
    BoostTypes GetBoostType();
    void SetBoostType(BoostTypes value);   


}
