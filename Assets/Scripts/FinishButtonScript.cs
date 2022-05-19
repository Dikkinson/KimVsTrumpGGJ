using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonScript : MonoBehaviour
{
    public RocketPart TopPart;
    public RocketPart MiddlePart;
    public RocketPart FuelPart;
    bool IsRocketReady = false;
    public GameObject Hammer;
    public GameObject Tape;
    bool isCapBroken;
    public int OwnerNumber;
    public GameObject cap;
    bool isHammer;
    bool isTape;
    Character player;
    bool PlayerNearButton = false;
    public GameObject EndScreen;
    void Start()
    {

    }

    void Win()
    {
        switch (OwnerNumber)
        {
            case 1:
                Debug.Log("TRUMP WIN");
                EndScreen.SetActive(true);
                break;
            case 2:
                Debug.Log("KIM WIN");
                EndScreen.SetActive(true);
                break;
        }
    }
    void Update()
    {
        if (TopPart.isPartReady && MiddlePart.isPartReady && FuelPart.isPartReady)
        {
            IsRocketReady = true;
        }
        if(ControllerSwitcher.IsContollerKeyboard == false)
        {
            if (Input.GetKeyDown(GamepadInputManager.Interact1) && isHammer == true)
            {
                HammerAction();
            }
            if (Input.GetKeyDown(GamepadInputManager.Interact1) && isTape == true)
            {
                TapeAction();
            }
            if (Input.GetKeyDown(GamepadInputManager.Interact2) && isHammer == true)
            {
                HammerAction();
            }
            if (Input.GetKeyDown(GamepadInputManager.Interact2) && isTape == true)
            {
                TapeAction();
            }
            if (isCapBroken == true && IsRocketReady == true && PlayerNearButton == true)
            {
                if (Input.GetKeyDown(GamepadInputManager.Interact1))
                {
                    Win();
                }
                if (Input.GetKeyDown(GamepadInputManager.Interact2))
                {
                    Win();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyboardInputManager.Interact1) && isHammer == true)
            {
                HammerAction();
            }
            if (Input.GetKeyDown(KeyboardInputManager.Interact2) && isTape == true)
            {
                TapeAction();
            }
            if (Input.GetKeyDown(KeyboardInputManager.Interact2) && isHammer == true)
            {
                HammerAction();
            }
            if (Input.GetKeyDown(KeyboardInputManager.Interact2) && isTape == true)
            {
                TapeAction();
            }
            if (isCapBroken == true && IsRocketReady == true && PlayerNearButton == true)
            {
                if (Input.GetKeyDown(KeyboardInputManager.Interact1))
                {
                    Win();
                }
                if (Input.GetKeyDown(KeyboardInputManager.Interact2))
                {
                    Win();
                }
            }
        }
        
    }
    void HammerAction()
    {
        isHammer = false;
        cap.SetActive(false);
        isCapBroken = true;
        player.GetComponentInChildren<AudioSource>().Play();
        Destroy(player.gameObject.GetComponentInChildren<ItemScript>().gameObject);
    }
    void TapeAction()
    {
        isTape = false;
        cap.SetActive(true);
        isCapBroken = false;
        player.GetComponentInChildren<AudioSource>().Play();
        Destroy(player.gameObject.GetComponentInChildren<ItemScript>().gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>().HandPos.childCount != 0)
        {
            if (collision.gameObject.GetComponentInChildren<ItemScript>().ItemName == Hammer.GetComponent<ItemScript>().ItemName && isCapBroken == false)
            {
                player = collision.gameObject.GetComponent<Character>();
                isHammer = true;
            }
            if (collision.gameObject.GetComponentInChildren<ItemScript>().ItemName == Tape.GetComponent<ItemScript>().ItemName && isCapBroken == true)
            {
                player = collision.gameObject.GetComponent<Character>();
                isTape = true;
            }
        }
        if (isCapBroken == true && IsRocketReady == true)
        {
            PlayerNearButton = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>().HandPos.childCount != 0)
        {
            if (collision.gameObject.GetComponentInChildren<ItemScript>().ItemName == Hammer.GetComponent<ItemScript>().ItemName && isCapBroken == false)
            {
                player = null;
                isHammer = false;
            }
            if (collision.gameObject.GetComponentInChildren<ItemScript>().ItemName == Tape.GetComponent<ItemScript>().ItemName && isCapBroken == true)
            {
                player = null;
                isTape = false;
            }
        }
        if (isCapBroken == true && IsRocketReady == true)
        {
            PlayerNearButton = false;
        }
    }
}
