using System;
using UnityEngine;

[Serializable]
public class BoostSound
{
    [field: SerializeField] public BoostTypes Type;
    [field: SerializeField] public AudioClip Sound;
}
