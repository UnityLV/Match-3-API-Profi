using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BoostSounds), menuName = "ScriptableObjects/Sounds/" + nameof(BoostSounds), order = 1)]

public class BoostSounds : ScriptableObject
{
    [SerializeField] private BoostSound[] _boostSounds;

    private Dictionary<BoostTypes, AudioClip> _soundsDictionary;

    public  AudioClip this[BoostTypes type]
    {
        get
        {
            if (_soundsDictionary == null)
            {
                ConstructBoostDictionaryes();
            }

            return _soundsDictionary[type];
        }
    }

    private void ConstructBoostDictionaryes()
    {
        _soundsDictionary = new();

        foreach (var bind in _boostSounds)
            _soundsDictionary.Add(bind.Type, bind.Sound);
    }
    



}
