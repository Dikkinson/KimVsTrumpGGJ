using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepadInputManager : MonoBehaviour
{
    public static KeyCode Jump1 { get; set; }
    public static KeyCode Jump2 { get; set; }
    public static KeyCode JumpDown1 { get; set; }
    public static KeyCode JumpDown2 { get; set; }
    public static KeyCode Interact1 { get; set; }
    public static KeyCode Interact2 { get; set; }
    List<KeyCode> keys;
    public List<Button> btns;
    int btnToChange = -1;
    public GameObject hintText;
    public static void SetDefaultControl()
    {
        Jump1 = KeyCode.None;
        Jump2 = KeyCode.None;
        JumpDown1 = KeyCode.None;
        JumpDown2 = KeyCode.None;
        Interact1 = KeyCode.None;
        Interact2 = KeyCode.None;
    }
    void Start()
    {
        keys = new List<KeyCode>()
        {
            Jump1,
            Jump2,
            JumpDown1,
            JumpDown2,
            Interact1,
            Interact2
        };
        UpdateButtons();
    }
    public void ToDefault()
    {
        SetDefaultControl();
        keys = new List<KeyCode>()
        {
            Jump1,
            Jump2,
            JumpDown1,
            JumpDown2,
            Interact1,
            Interact2
        };
        UpdateButtons();
    }
    void UpdateButtons()
    {
        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].GetComponentInChildren<Text>().text = keys[i].ToString();
        }
    }
    public void ChangeControl(int ButtonId)
    {
        btnToChange = ButtonId;
        btns[btnToChange].GetComponentInChildren<Text>().text = "Жми кнопку";
    }
    KeyCode reCodeButton(KeyCode vKey, int playerNumber)
    {
        KeyCode newBtn = KeyCode.None;
        if(playerNumber == 1)
        {
            if (vKey == KeyCode.JoystickButton0) newBtn = KeyCode.Joystick1Button0;
            if (vKey == KeyCode.JoystickButton1) newBtn = KeyCode.Joystick1Button1;
            if (vKey == KeyCode.JoystickButton2) newBtn = KeyCode.Joystick1Button2;
            if (vKey == KeyCode.JoystickButton3) newBtn = KeyCode.Joystick1Button3;
            if (vKey == KeyCode.JoystickButton4) newBtn = KeyCode.Joystick1Button4;
            if (vKey == KeyCode.JoystickButton5) newBtn = KeyCode.Joystick1Button5;
        }
        else
        {
            if (vKey == KeyCode.JoystickButton0) newBtn = KeyCode.Joystick2Button0;
            if (vKey == KeyCode.JoystickButton1) newBtn = KeyCode.Joystick2Button1;
            if (vKey == KeyCode.JoystickButton2) newBtn = KeyCode.Joystick2Button2;
            if (vKey == KeyCode.JoystickButton3) newBtn = KeyCode.Joystick2Button3;
            if (vKey == KeyCode.JoystickButton4) newBtn = KeyCode.Joystick2Button4;
            if (vKey == KeyCode.JoystickButton5) newBtn = KeyCode.Joystick2Button5;
        }
        return newBtn;
    }
    void Update()
    {
        if (btnToChange != -1)
        {
            foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    if (vKey != KeyCode.Escape || vKey != KeyCode.JoystickButton6 || vKey != KeyCode.JoystickButton7 || vKey != KeyCode.JoystickButton8 || vKey != KeyCode.JoystickButton9)
                    {
                        for (int i = 0; i < keys.Count; i++)
                        {
                            if (vKey == keys[i])
                            {
                                if (vKey != keys[btnToChange])
                                {
                                    hintText.SetActive(true);
                                    return;
                                }
                            }
                        }
                        KeyCode newKey = vKey;
                        hintText.SetActive(false);
                        switch (btnToChange)
                        {
                            case 0:
                                newKey = reCodeButton(vKey, 1);
                                Jump1 = newKey;
                                break;
                            case 1:
                                newKey = reCodeButton(vKey, 2);
                                Jump2 = newKey;
                                break;
                            case 2:
                                newKey = reCodeButton(vKey, 1);
                                JumpDown1 = newKey;
                                break;
                            case 3:
                                newKey = reCodeButton(vKey, 2);
                                JumpDown2 = newKey;
                                break;
                            case 4:
                                newKey = reCodeButton(vKey, 1);
                                Interact1 = newKey;
                                break;
                            case 5:
                                newKey = reCodeButton(vKey, 2);
                                Interact2 = newKey;
                                break;
                            default:
                                break;
                        }
                        keys[btnToChange] = newKey;
                        Debug.Log(newKey);
                        btns[btnToChange].GetComponentInChildren<Text>().text = newKey.ToString();
                        btnToChange = -1;
                    }
                }
            }
        }
    }
}
