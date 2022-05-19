using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInputManager : MonoBehaviour
{
    
    public static KeyCode Left1 { get; set; }
    public static KeyCode Left2 { get; set; }
    public static KeyCode Right1 { get; set; }
    public static KeyCode Right2 { get; set; }
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
        Left1 = KeyCode.A;
        Left2 = KeyCode.LeftArrow;
        Right1 = KeyCode.D;
        Right2 = KeyCode.RightArrow;
        Jump1 = KeyCode.W;
        Jump2 = KeyCode.UpArrow;
        JumpDown1 = KeyCode.S;
        JumpDown2 = KeyCode.DownArrow;
        Interact1 = KeyCode.T;
        Interact2 = KeyCode.P;
    }
    void Start()
    {
        keys = new List<KeyCode>()
        {
            Left1,
            Left2,
            Right1,
            Right2,
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
            Left1,
            Left2,
            Right1,
            Right2,
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
    void Update()
    {
        if (btnToChange != -1)
        {
            foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    if (vKey != KeyCode.Escape)
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
                        hintText.SetActive(false);
                        switch (btnToChange)
                        {
                            case 0:
                                Left1 = vKey;
                                break;
                            case 1:
                                Left2 = vKey;
                                break;
                            case 2:
                                Right1 = vKey;
                                break;
                            case 3:
                                Right2 = vKey;
                                break;
                            case 4:
                                Jump1 = vKey;
                                break;
                            case 5:
                                Jump2 = vKey;
                                break;
                            case 6:
                                JumpDown1 = vKey;
                                break;
                            case 7:
                                JumpDown2 = vKey;
                                break;
                            case 8:
                                Interact1 = vKey;
                                break;
                            case 9:
                                Interact2 = vKey;
                                break;
                            default:
                                break;
                        }
                        keys[btnToChange] = vKey;
                        btns[btnToChange].GetComponentInChildren<Text>().text = vKey.ToString();
                        btnToChange = -1;
                    }
                }
            }
        }
    }
}
