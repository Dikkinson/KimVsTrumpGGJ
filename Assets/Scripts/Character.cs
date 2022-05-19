using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public Transform HandPos;
    public GameObject EmptyHands;
    public GameObject NotEmptyHands;
    public int CharacterNumber;
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;
    [HideInInspector]
    public bool isOnPlatform;
    bool isMainPlatform;
    [HideInInspector]
    public bool isThroughPlatform;
    public Collider2D physicCollider;
    //private CharState State = CharState.Idle;
    bool crush;
    float crushTimerSetting = 0.4f;
    float crushTimer;
    float platformTimerSetting = 0.5f;
    float platformTimer;
    bool IsPlayerNearItem = false;
    bool isJumpDelay = false;
    SkeletonAnimation animation;
    GameObject ItemToPickUp;
    bool isRun;
    Vector3 direction;
    //{
    //get { return (CharState)animator.GetInteger("State"); }
    //set { animator.SetInteger("State", (int)value); }
    //}

    //rivate Animator animator;
    private SpriteRenderer sprite;

    bool CharacterFaceRight = true;

    private void Awake()
    {
        animation = gameObject.GetComponent<SkeletonAnimation>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        crushTimer = crushTimerSetting;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (HandPos.childCount != 0)
        {
            EmptyHands.SetActive(false);
            NotEmptyHands.SetActive(true);
        }
        else
        {
            EmptyHands.SetActive(true);
            NotEmptyHands.SetActive(false);
        }
        //Debug.Log(rigidbody.velocity.y);
        if (platformTimer > 0 && isThroughPlatform == true)
        {
            platformTimer -= Time.deltaTime;
        }
        else
        {
            isThroughPlatform = false;
            platformTimer = platformTimerSetting;
        }
        //if (isOnPlatform) State = CharState.Idle;
        if (ControllerSwitcher.IsContollerKeyboard == false)
        {
            if (Input.GetAxis("L_Horizontal" + CharacterNumber) != 0)
            {
                Run();
            }
            else
            {
                animation.AnimationName = "idle";
            }
            if(CharacterNumber == 1)
            {
                if (isOnPlatform && Input.GetKeyDown(GamepadInputManager.Jump1))
                {
                    if (isJumpDelay == false) StartCoroutine(JumpDelay());
                }
                if (Input.GetKeyDown(GamepadInputManager.JumpDown1))
                {
                    if (isOnPlatform == true && isMainPlatform == false)
                    {
                        StartCoroutine(JumpDown());
                    }
                }
                if (Input.GetKeyDown(GamepadInputManager.Interact1) && IsPlayerNearItem == true)
                {
                    TakeItem();
                    if (HandPos.childCount > 1)
                    {
                        Destroy(HandPos.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                if (isOnPlatform && Input.GetKeyDown(GamepadInputManager.Jump2))
                {
                    if (isJumpDelay == false) StartCoroutine(JumpDelay());
                }
                if (Input.GetKeyDown(GamepadInputManager.JumpDown2))
                {
                    if (isOnPlatform == true && isMainPlatform == false)
                    {
                        StartCoroutine(JumpDown());
                    }
                }
                if (Input.GetKeyDown(GamepadInputManager.Interact2) && IsPlayerNearItem == true)
                {
                    TakeItem();
                    if (HandPos.childCount > 1)
                    {
                        Destroy(HandPos.GetChild(0).gameObject);
                    }
                }
            }
        }
        else
        {
            if (CharacterNumber == 1)
            {
                if (Input.GetKey(KeyboardInputManager.Left1) || Input.GetKey(KeyboardInputManager.Right1))
                {
                    isRun = true;
                }
                else
                {
                    isRun = false;
                }
                if (isOnPlatform && Input.GetKeyDown(KeyboardInputManager.Jump1))
                {
                    if (isJumpDelay == false) StartCoroutine(JumpDelay());
                }
                if (Input.GetKeyDown(KeyboardInputManager.JumpDown1))
                {
                    if (isOnPlatform == true && isMainPlatform == false)
                    {
                        StartCoroutine(JumpDown());
                    }
                }
                if (Input.GetKeyDown(KeyboardInputManager.Interact1) && IsPlayerNearItem == true)
                {
                    TakeItem();
                    if (HandPos.childCount > 1)
                    {
                        Destroy(HandPos.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyboardInputManager.Left2) || Input.GetKey(KeyboardInputManager.Right2))
                {
                    isRun = true;
                }
                else
                {
                    isRun = false;
                }
                if (isOnPlatform && Input.GetKeyDown(KeyboardInputManager.Jump2))
                {
                    if (isJumpDelay == false) StartCoroutine(JumpDelay());
                }
                if (Input.GetKeyDown(KeyboardInputManager.JumpDown2))
                {
                    if (isOnPlatform == true && isMainPlatform == false)
                    {
                        StartCoroutine(JumpDown());
                    }
                }
                if (Input.GetKeyDown(KeyboardInputManager.Interact2) && IsPlayerNearItem == true)
                {
                    TakeItem();
                    if (HandPos.childCount > 1)
                    {
                        Destroy(HandPos.GetChild(0).gameObject);
                    }
                }
            }
            if (isRun == true)
            {
                Run();
            }
            else
            {
                animation.AnimationName = "idle";
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator JumpDelay()
    {
        isJumpDelay = true;
        Jump();
        yield return new WaitForSeconds(0.5f);
        isJumpDelay = false;
        EmptyHands.GetComponent<Rigidbody2D>().simulated = true;
    }
    private void Run()
    {
        if (crush == false)
        {
            animation.AnimationName = "animation";
            if (ControllerSwitcher.IsContollerKeyboard == false)
            {
                direction = transform.right * Input.GetAxis("L_Horizontal" + CharacterNumber);
            }
            else
            {
                if (CharacterNumber == 1)
                {
                    if (Input.GetKey(KeyboardInputManager.Left1)) direction = transform.right * -1;
                    if (Input.GetKeyUp(KeyboardInputManager.Left1)) direction = transform.right * 0;
                    if (Input.GetKey(KeyboardInputManager.Right1)) direction = transform.right * 1;
                    if (Input.GetKeyUp(KeyboardInputManager.Right1)) direction = transform.right * 0;
                }
                else
                {
                    if (Input.GetKey(KeyboardInputManager.Left2)) direction = transform.right * -1;
                    if (Input.GetKeyUp(KeyboardInputManager.Left2)) direction = transform.right * 0;
                    if (Input.GetKey(KeyboardInputManager.Right2)) direction = transform.right * 1;
                    if (Input.GetKeyUp(KeyboardInputManager.Right2)) direction = transform.right * 0;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

            if (direction.x > 0 && !CharacterFaceRight)
            {
                Flip();
            }
            else
            if (direction.x < 0 && CharacterFaceRight)
            {
                Flip();
            }

            //if (isOnPlatform) State = CharState.Run;
        }
        else
        {
            if (crushTimer > 0)
            {
                crushTimer -= Time.deltaTime;
            }
            else
            {
                crush = false;
                crushTimer = crushTimerSetting;
            }
        }
    }
    void Flip()
    {
        CharacterFaceRight = !CharacterFaceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void Jump()
    {
        if (isThroughPlatform == false)
        {
            animation.AnimationName = "idle";
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            gameObject.GetComponent<AudioSource>().Play();
            EmptyHands.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        //EmptyHands.GetComponent<Rigidbody2D>().simulated = true;
        //if (!isOnPlatform) State = CharState.Jump;
    }
    IEnumerator JumpDown()
    {
        while (isOnPlatform == true)
        {
            physicCollider.enabled = false;
            isThroughPlatform = true;
            yield return null;
        }
        physicCollider.enabled = true;
        isThroughPlatform = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            isOnPlatform = true;
        }
        if (col.gameObject.tag == "MainPlatform")
        {
            isMainPlatform = true;
            isOnPlatform = true;
        }
        if (col.gameObject.tag == "Player")
        {
            crush = true;
            if (transform.position.x < col.transform.position.x)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, 0.5f) * 4f, ForceMode2D.Impulse);

            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 0.5f) * 4f, ForceMode2D.Impulse);
            }
        }
        if (col.gameObject.tag == "Item")
        {
            IsPlayerNearItem = true;
            ItemToPickUp = col.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            isOnPlatform = false;
        }
        if (col.gameObject.tag == "MainPlatform")
        {
            isMainPlatform = false;
            isOnPlatform = false;
        }
        if (col.gameObject.tag == "Item")
        {
            IsPlayerNearItem = false;
            ItemToPickUp = null;
        }
    }
    private void TakeItem()
    {
        ItemToPickUp.transform.SetParent(HandPos);
        ItemToPickUp.transform.localPosition = Vector3.zero;
        //ItemToPickUp.gameObject.GetComponent<Animator>().enabled = false;
        IsPlayerNearItem = false;
        ItemToPickUp.GetComponent<Collider2D>().enabled = false;
    }
}