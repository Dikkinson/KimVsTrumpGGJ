using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static bool isFirstStart = false;
    private void Start()
    {
        if(isFirstStart == false)
        {
            KeyboardInputManager.SetDefaultControl();
            GamepadInputManager.SetDefaultControl();
            AudioSettingsController.MusicVolume = 0.5f;
            AudioSettingsController.EffectVolume = 0.5f;
            ControllerSwitcher.IsContollerKeyboard = true;
            isFirstStart = true;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenPanel(GameObject panelToOpen)
    {
        panelToOpen.SetActive(true);
    }
    public void ClosePanel(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
    }
}
