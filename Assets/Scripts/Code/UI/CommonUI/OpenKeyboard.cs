using UnityEngine;

public class OpenKeyboard : MonoBehaviour
{
    private TouchScreenKeyboard keyboard;
    public void Open()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
