using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenKeyboard : MonoBehaviour
{
    [SerializeField]
    private TouchScreenKeyboard keyboard;
    public TextMeshProUGUI textbox;
    public void OpenSystemKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    private void Update()
    {
        if (keyboard != null)
        {
            string keyboardText = keyboard.text;
            textbox.text = keyboardText;
        }
    }
}
