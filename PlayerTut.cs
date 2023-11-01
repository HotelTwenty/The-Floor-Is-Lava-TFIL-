using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTut : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    public bool isGrounded = false;
    public static bool facingRight = true;
    int PAJumpPower = 8000;

    public GameObject JPad;
    // cont butts =========================================================================
    public GameObject spitPRESS, jumpPRESS, stickPRESS;
    // kb butts ===========================================================================
    public GameObject aPRESS, dPRESS, fPRESS, sbPRESS, leftArrowPRESS, rightArrowPRESS;
    Vector2 JPadPos;
    float nextJumpPad = 0f;
    float jumpPadRate = .2f;
    public static float fxVol;
    public AudioSource playerShootSound, playerDieSound, playerLandSound, playerJumpSound;
    bool canMove;
    public GameObject ToJumpTxt, JumpCmd, ToSpitTxt, SpitCmd, NowChainTxt, RememberTxt, RememberTxt2, blacker, ExcelentTxt, youReady, staySafeTxt;
    bool shouldJump, shouldSpit, shouldChain, shouldRemember;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        canMove = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        spitPRESS.SetActive(false);
        jumpPRESS.SetActive(false);
        stickPRESS.SetActive(false);
        aPRESS.SetActive(false);
        dPRESS.SetActive(false);
        leftArrowPRESS.SetActive(false);
        rightArrowPRESS.SetActive(false);
        fPRESS.SetActive(false);
        sbPRESS.SetActive(false);
        Invoke("switchMove", 45f);
        ToJumpTxt.SetActive(false);
        JumpCmd.SetActive(false);
        ToSpitTxt.SetActive(false);
        SpitCmd.SetActive(false);
        NowChainTxt.SetActive(false);
        RememberTxt.SetActive(false);
        RememberTxt2.SetActive(false);
        shouldJump = true;
        shouldChain = true;
        shouldSpit = true;
        shouldRemember = true;

        Invoke("openJumpCommnands", 50f);
    }

    // Update is called once per frame
    void Update()
    {
        fxVol = MasterSettings.SFXVolume;
        playerShootSound.volume = fxVol;
        playerDieSound.volume = fxVol;
        playerJumpSound.volume = fxVol;
        playerLandSound.volume = fxVol;

        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m * 0, Space.World);
        playerMove();

        if(isGrounded == true)
        {
            GetComponent<Animator> ().SetBool ("isJumping", false);
        }

        else
        {
            GetComponent<Animator> ().SetBool ("isJumping", true);
        }

        if(transform.position.x > 11.4f)
        {
            ToJumpTxt.SetActive(false);
            JumpCmd.SetActive(false);
            ToSpitTxt.SetActive(false);
            SpitCmd.SetActive(false);
            NowChainTxt.SetActive(false);
            RememberTxt.SetActive(false);
        }

        else
        {
            //Do nothing
        }

        if(transform.position.x < 251f && transform.position.x > 148f)
        {
            RememberTxt2.SetActive(true);
        }

        else
        {
            RememberTxt2.SetActive(false);
        }
    }

    void openJumpCommnands()
    {
        if(shouldJump == true)
        {
            ToJumpTxt.SetActive(true);
            JumpCmd.SetActive(true);
        }   
    }

    void closeJumpCommands()
    {
        ToJumpTxt.SetActive(false);
        JumpCmd.SetActive(false);
        shouldJump = false;
        Invoke("openSpitCommands", 2f);
    }

    void openSpitCommands()
    {
        if(shouldSpit == true)
        {
            ToSpitTxt.SetActive(true);
            SpitCmd.SetActive(true);
        }
    }

    void closeSpitCommands()
    {
        ToSpitTxt.SetActive(false);
        SpitCmd.SetActive(false);
        shouldSpit = false;
        Invoke("openNowChain", 2f);
    }

    void openNowChain()
    {
        if(shouldChain == true)
        {
            NowChainTxt.SetActive(true);
        }
        Invoke("closeNowChain", 5f);
    }

    void closeNowChain()
    {
        NowChainTxt.SetActive(false);
        shouldChain = false;
        if(shouldRemember == true)
        {
            Invoke("openRemember1", 2f);
        }
    }

    void openRemember1()
    {
        if(shouldRemember == true)
        {
            RememberTxt.SetActive(true);   
        }
    }

    void closeRemember1()
    {
        RememberTxt.SetActive(false);
        shouldRemember = false;
    }
    
    void switchMove()
    {
        canMove = true;
    }

    void playerMove()
    {
        if(canMove == true)
        {
            bool isLeftKeyHeld = controls.Gameplay.MoveLeft.ReadValue<float>() > 0.1f;
            bool isRightKeyHeld = controls.Gameplay.MoveRight.ReadValue<float>() > 0.1f;
            bool isSpitKeyHeld = controls.Gameplay.Spit.ReadValue<float>() > 0.1f;

            if(isLeftKeyHeld || move.x < -0.1f)
            {
                transform.position = new Vector2(transform.position.x - 20 * Time.deltaTime, transform.position.y);
                GetComponent<Animator> ().SetBool ("isRunning", true);
                if(facingRight == true)
                {
                    FlipPlayer();
                    facingRight = false;
                }
                stickPRESS.SetActive(true);
                aPRESS.SetActive(true);
                leftArrowPRESS.SetActive(true);
            }

            else if(isRightKeyHeld || move.x > 0.1f)
            {
                transform.position = new Vector2(transform.position.x + 20 * Time.deltaTime, transform.position.y);
                GetComponent<Animator> ().SetBool ("isRunning", true);
                if(facingRight == false)
                {
                    FlipPlayer();
                    facingRight = true;
                }
                stickPRESS.SetActive(true);
                dPRESS.SetActive(true);
                rightArrowPRESS.SetActive(true);
            }

            else
            {
                GetComponent<Animator> ().SetBool ("isRunning", false);
                stickPRESS.SetActive(false);
                aPRESS.SetActive(false);
                dPRESS.SetActive(false);
                rightArrowPRESS.SetActive(false);
                leftArrowPRESS.SetActive(false);
            }

            if(isSpitKeyHeld == true)
            {
                if(Time.time > nextJumpPad)
                {
                    closeSpitCommands();
                    nextJumpPad = Time.time + jumpPadRate;
                    JumpPadSpawn();
                    spitPRESS.SetActive(true);
                    fPRESS.SetActive(true);
                }
            }

            else
            {
                spitPRESS.SetActive(false);
                fPRESS.SetActive(false);
            }

            controls.Gameplay.Jump.performed += ctx => Jump();
        }
    }

    void Jump()
    {
        bool isLeftKeyHeld = controls.Gameplay.MoveLeft.ReadValue<float>() > 0.1f;
        bool isRightKeyHeld = controls.Gameplay.MoveRight.ReadValue<float>() > 0.1f;
        closeJumpCommands();
        if(isGrounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce (Vector2.up * PAJumpPower);
            sbPRESS.SetActive(true);
            jumpPRESS.SetActive(true);
            playerJumpSound.Play();
            if(isLeftKeyHeld || move.x < -0.1)
            {
                GetComponent<Rigidbody2D>().AddForce (Vector2.left * 2000);
            }

            if(isRightKeyHeld || move.x > 0.1)
            {
                GetComponent<Rigidbody2D>().AddForce (Vector2.left * 2000);
            }
            isGrounded = false;
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Ground") 
	    {
            isGrounded = true;
            sbPRESS.SetActive(false);
            jumpPRESS.SetActive(false);
            if(isGrounded == false)
            {
                playerLandSound.Play();
            }
        }

        if (col.gameObject.tag == "Lava") 
	    {
            playerDieSound.Play();
            canMove = false;
            Invoke("switchMove", .2f);
            transform.position = new Vector3(152f, 34f, 0f);
        }

        if (col.gameObject.tag == "TutEnd") 
	    {
            canMove = false;
            openBlacker();
            Invoke("openExcellentTxt", 1f);
            Invoke("closeExcellentTxt", 5f);
            Invoke("openYouReady", 5.5f);
            Invoke("closeYouReady", 10f);
            Invoke("openStaySafe", 10.5f);
            Invoke("backToMM", 15f);

        }
    }

    void backToMM()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void openBlacker()
    {
        blacker.SetActive(true);
    }

    void openExcellentTxt()
    {
        playerLandSound.Play();
        ExcelentTxt.SetActive(true);
    }

    void closeExcellentTxt()
    {
        ExcelentTxt.SetActive(false);
    }

    void openYouReady()
    {
        playerLandSound.Play();
        youReady.SetActive(true);
    }

    void closeYouReady()
    {
        youReady.SetActive(false);
    }

    void openStaySafe()
    {
        playerLandSound.Play();
        staySafeTxt.SetActive(true);
    }

    void JumpPadSpawn()
    {
        JPadPos = transform.position;
        JPadPos += new Vector2(0f, -4f);
        Instantiate(JPad, JPadPos, Quaternion.identity);
        playerShootSound.Play();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
