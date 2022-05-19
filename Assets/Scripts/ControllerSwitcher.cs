using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    public static bool IsContollerKeyboard;
    public GameObject keyboardControls;
    public GameObject gamepadControls;
    public void SwitchController(bool value)
    {
        IsContollerKeyboard = value;
        keyboardControls.SetActive(value);
        gamepadControls.SetActive(!value);
    }
}
