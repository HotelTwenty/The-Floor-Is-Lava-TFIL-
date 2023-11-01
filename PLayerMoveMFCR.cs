using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PLayerMoveMFCR : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    public bool isGrounded = false;
    public static bool facingRight = true;
    int PAJumpPower = 11000;

    public GameObject JPad;
    Vector2 JPadPos;
    float nextJumpPad = 0f;
    float jumpPadRate = .08f;
    public static float fxVol;
    public AudioSource playerShootSound, playerDieSound, playerLandSound, playerJumpSound;
    public int LevelOn;
    public static int livesLeft;
    public GameObject h1, h2, h3, relifeFlash;
    public static bool deadStat;
    bool canMove;
    Vector3 currTransPos;
    public static bool hitLava;
    bool hitChck;
    public GameObject chckPnt;
    bool gameIngProg;
    

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        deadStat = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        livesLeft = 3;
        LevelOn = SceneManager.GetActiveScene().buildIndex;
        canMove = false;
        Invoke("moveSwitch", 18f);
        relifeFlash.SetActive(false);
        currTransPos = new Vector3(0f, 9f, 0f);
        hitLava = false;
        hitChck = false;
        chckPnt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        LevelOn = SceneManager.GetActiveScene().buildIndex;
        fxVol = MasterSettings.SFXVolume;
        playerShootSound.volume = fxVol;
        playerDieSound.volume = fxVol;
        playerJumpSound.volume = fxVol;
        playerLandSound.volume = fxVol;

        if(canMove == true)
        {
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
            }if(livesLeft == 3)
            {
                h1.SetActive(true);
                h2.SetActive(true);
                h3.SetActive(true);

                deadStat = false;
            }

            else if(livesLeft == 2)
            {
                h1.SetActive(true);
                h2.SetActive(true);
                h3.SetActive(false);

                deadStat = false;
            }

            else if(livesLeft == 1)
            {
                h1.SetActive(true);
                h2.SetActive(false);
                h3.SetActive(false);

                deadStat = false;
            }

            else if(livesLeft <= 0)
            {
                h1.SetActive(false);
                h2.SetActive(false);
                h3.SetActive(false);

                deadStat = true;
            }

            else
            {
                //do nothing
            }
        }

        if(deadStat == true)
        {
            canMove = false;
        }
    }

    void playerMove()
    {
        bool isLeftKeyHeld = controls.Gameplay.MoveLeft.ReadValue<float>() > 0.1f;
        bool isRightKeyHeld = controls.Gameplay.MoveRight.ReadValue<float>() > 0.1f;
        bool isSpitKeyHeld = controls.Gameplay.Spit.ReadValue<float>() > 0.1f;

        if(isLeftKeyHeld || move.x < -0.1f)
        {
            transform.position = new Vector2(transform.position.x - 35 * Time.deltaTime, transform.position.y);
            GetComponent<Animator> ().SetBool ("isRunning", true);
            if(facingRight == true)
            {
                FlipPlayer();
                facingRight = false;
            }
        }

        else if(isRightKeyHeld || move.x > 0.1f)
        {
            transform.position = new Vector2(transform.position.x + 35 * Time.deltaTime, transform.position.y);
            GetComponent<Animator> ().SetBool ("isRunning", true);
            if(facingRight == false)
            {
                FlipPlayer();
                facingRight = true;
            }
        }

        else
        {
            GetComponent<Animator> ().SetBool ("isRunning", false);
        }
        if(isSpitKeyHeld == true)
        {
            if(Time.time > nextJumpPad)
            {
                nextJumpPad = Time.time + jumpPadRate;
                JumpPadSpawn();
            }
        }

        controls.Gameplay.Jump.performed += ctx => Jump();
    }

    void Jump()
    {
        bool isLeftKeyHeld = controls.Gameplay.MoveLeft.ReadValue<float>() > 0.1f;
        bool isRightKeyHeld = controls.Gameplay.MoveRight.ReadValue<float>() > 0.1f;

        if(isGrounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce (Vector2.up * PAJumpPower);
            playerJumpSound.Play();
            if(isLeftKeyHeld || move.x < -0.1)
            {
                GetComponent<Rigidbody2D>().AddForce (Vector2.left * 2000);
                playerJumpSound.Play();
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
            if(isGrounded == false)
            {
                playerLandSound.Play();
            }
        }

        if (col.gameObject.tag == "Lava") 
	    {
            /*isGrounded = true;
            if(isGrounded == false)
            {
                playerLandSound.Play();
            }*/
            playerDieSound.Play();
            if(hitChck == false)
            {
                livesLeft -=1;
                hitChck = true;
            }
            relifeFlash.SetActive(true);
            Invoke("relifeScreenOff", .2f);
            hitLava = true;
            Invoke("deactLavaHit", .2f);
            Invoke("deactHitChck", .2f);
            transform.position = currTransPos;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "RUpdater":
            currTransPos = transform.position;
            chckPnt.SetActive(true);
            Invoke("closeChck", 1f);
            break;

            case "LvlEnd":
            SceneManager.LoadScene("TheEndTwo");
            break;
        }
    }

    void JumpPadSpawn()
    {
        JPadPos = transform.position;
        JPadPos += new Vector2(0f, -4f);
        Instantiate(JPad, JPadPos, Quaternion.identity);
        playerShootSound.Play();
    }

    void moveplayerBack()
    {
        transform.position = currTransPos;
    }

    void closeChck()
    {
        chckPnt.SetActive(false);
    }

    void moveSwitch()
    {
        canMove = !canMove;
    }

    void deactLavaHit()
    {
        hitLava = false;
    }

    void deactHitChck()
    {
        hitChck = false;
    }

    void relifeScreenOff()
    {
        relifeFlash.SetActive(false);
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
