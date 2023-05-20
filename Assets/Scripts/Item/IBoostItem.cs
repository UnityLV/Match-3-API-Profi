public interface IBoostItem : IItem
{
    BoostTypes GetBoostType();
    void Init(BoostTypes value);
    bool IsUsed { get; set; }
}
