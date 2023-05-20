using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceScrptableObject", menuName = "ScriptableObjects/ChoiceScrptableObject")]
public class ChoiceScrptableObject : ScriptableObject
{
    [SerializeField] private int index;

    [SerializeField] private Sprite[] sprites;
    public Sprite[] Sprites => sprites;
    public int Index => index;
}
