using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPart : MonoBehaviour
{
    public Character player;
    int PlayerNumber;
    public List<GameObject> RequariedPartsList;
    public List<GameObject> PartStages;
    int partsCounter;
    int partCountNeed;
    [HideInInspector]
    public bool isPartReady = false;
    bool IsCorrectPart;
    public GameObject Hammer;
    bool isHammer = false;
    Character playerWithHammer;
    public bool PartCanBeDestroyed;
    void Start()
    {
        PlayerNumber = player.CharacterNumber;
        partCountNeed = RequariedPartsList.Count;
    }
    void Update()
    {
        if(ControllerSwitcher.IsContollerKeyboard == false)
        {
            if(PlayerNumber == 1)
            {
                if (Input.GetKeyDown(GamepadInputManager.Interact1) && IsCorrectPart == true)
                {
                    PartStages[partsCounter].SetActive(true);
                    partsCounter++;
                    player.gameObject.GetComponentInChildren<AudioSource>().Play();
                    Destroy(player.gameObject.GetComponentInChildren<ItemScript>().gameObject);
                    IsCorrectPart = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(GamepadInputManager.Interact2) && IsCorrectPart == true)
                {
                    PartStages[partsCounter].SetActive(true);
                    partsCounter++;
                    player.gameObject.GetComponentInChildren<AudioSource>().Play();
                    Destroy(player.gameObject.GetComponentInChildren<ItemScript>().gameObject);
                    IsCorrectPart = false;
                }
            }
            if (Input.GetKeyDown(GamepadInputManager.Interact1) && isHammer == true)
            {
                HammerAction();
            }
            if (Input.GetKeyDown(GamepadInputManager.Interact2) && isHammer == true)
            {
                HammerAction();
            }
        }
        else
        {
            if(PlayerNumber == 1)
            {
                if (Input.GetKeyDown(KeyboardInputManager.Interact1) && IsCorrectPart == true)
                {
                    PartStages[partsCounter].SetActive(true);
                    partsCounter++;
                    player.gameObject.GetComponentInChildren<AudioSource>().Play();
                    Destroy(player.gameObject.GetComponentInChildren<ItemScript>().gameObject);
                    IsCorrectPart = false;
                }
                if (Input.GetKeyDown(KeyboardInputManager.Interact1) && isHammer == true)
                {
                    HammerAction();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyboardInputManager.Interact2) && IsCorrectPart == true)
                {
                    PartStages[partsCounter].SetActive(true);
                    partsCounter++;
                    player.gameObject.GetComponentInChildren<AudioSource>().Play();
                    Destroy(player.gameObject.GetComponentInChildren<ItemScript>().gameObject);
                    IsCorrectPart = false;
                }
                if (Input.GetKeyDown(KeyboardInputManager.Interact2) && isHammer == true)
                {
                    HammerAction();
                }
            }
        }

        if (partsCounter == partCountNeed)
        {
            isPartReady = true;
        }
        else
        {
            isPartReady = false;
        }
            

    }
    void HammerAction()
    {
        if (PartCanBeDestroyed == true)
        {
            playerWithHammer.GetComponentInChildren<AudioSource>().Play();
            Destroy(playerWithHammer.GetComponentInChildren<ItemScript>().gameObject);
            foreach (var item in PartStages)
            {
                item.SetActive(false);
            }
            partsCounter = 0;
            isHammer = false;
            isPartReady = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPartReady == false)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Character>().CharacterNumber == PlayerNumber)
            {
                if (collision.gameObject.GetComponent<Character>().HandPos.childCount != 0)
                {
                    if (collision.gameObject.GetComponentInChildren<ItemScript>().ItemName == RequariedPartsList[partsCounter].GetComponent<ItemScript>().ItemName)
                    {
                        IsCorrectPart = true;
                    }
                }
            }
        }
        if (collision.gameObject.GetComponent<Character>().HandPos.childCount != 0 && partsCounter != 0)
        {
            if (collision.gameObject.GetComponentInChildren<ItemScript>().ItemName == Hammer.GetComponent<ItemScript>().ItemName && partsCounter != 0)
            {
                playerWithHammer = collision.gameObject.GetComponent<Character>();
                isHammer = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPartReady == false)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Character>().CharacterNumber == PlayerNumber)
            {
                IsCorrectPart = false;
            }
            if (collision.gameObject.GetComponent<Character>().HandPos.childCount != 0)
            {
                if (collision.gameObject.GetComponentInChildren<ItemScript>().ItemName == Hammer.GetComponent<ItemScript>().ItemName)
                {
                    playerWithHammer = null;
                    isHammer = false;
                }
            }
        }
        playerWithHammer = null;
        isHammer = false;
    }
}
