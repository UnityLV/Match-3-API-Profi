using UnityEngine;

public class BoostStateChangedSound : MonoBehaviour
{
    [SerializeField] private BoostItem _boostItem;
    [SerializeField] private BoostSounds _boostSounds;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        _boostItem.LeavedBoostState += OnLeavedBoostState;
    }
    private void OnDisable()
    {
        _boostItem.LeavedBoostState -= OnLeavedBoostState;

    }
    private void OnLeavedBoostState(BoostTypes boostType)
    {
        var sound = _boostSounds[boostType];
        _audioSource.PlayOneShot(sound);
    }


}
