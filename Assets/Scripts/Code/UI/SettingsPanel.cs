using Assets.Scripts.Code;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private SoundService soundService;
    public Toggle MusicToggle => musicToggle;
    public Toggle SoundToggle => soundToggle;
    private void Start()
    {
        if (PlayerPrefs.HasKey(SoundService.Music))
        {
            int music = PlayerPrefs.GetInt(SoundService.Music);
            soundService.TurnSound(SoundService.Music, music);
            musicToggle.isOn = music == 0;
        }
        if (PlayerPrefs.HasKey(SoundService.Effects))
        {
            int effects = PlayerPrefs.GetInt(SoundService.Effects);
            soundService.TurnSound(SoundService.Effects, effects);
            musicToggle.isOn = effects == 0;
        }
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        soundToggle.onValueChanged.AddListener(ToggleEffects);
    }
    public void ToggleMusic(bool enable)
    {
        soundService.ToggleSound(enable, SoundService.Music);
        PlayerPrefs.SetInt(SoundService.Music, enable?0:-80);
    }
    public void ToggleEffects(bool enable)
    {
        soundService.ToggleSound(enable, SoundService.Effects);
        PlayerPrefs.SetInt(SoundService.Effects, enable ? 0 : -80);
    }
    public void ShowInfo()
    {
        System.Diagnostics.Process.Start("https://www.google.com/");
    }
}
