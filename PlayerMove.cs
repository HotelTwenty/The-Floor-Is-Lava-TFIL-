using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    public bool isGrounded = false;
    public static bool facingRight = true;
    int PAJumpPower = 10000;

    public GameObject JPad;
    Vector2 JPadPos;
    float nextJumpPad = 0f;
    float jumpPadRate = .2f;
    public static float fxVol;
    public AudioSource playerShootSound, playerDieSound, playerLandSound, playerJumpSound;
    public int LevelOn;
    public static int livesLeft;
    public GameObject h1, h2, h3, h4, h5, relifeFlash;
    public static bool deadStat;
    bool canMove;
    Vector3 currTransPos;
    public GameObject chckPnt;
    
    public float TimeLeftS1, TimeLeftS2, TimeLeftS3, TimeLeftS4, TimeLeftS5;
    bool hitChck;
    

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
        livesLeft = 5;
        LevelOn = SceneManager.GetActiveScene().buildIndex;
        canMove = false;
        Invoke("moveSwitch", 2f);
        relifeFlash.SetActive(false);
        currTransPos = new Vector3(0f, 9f, 0f);
        TimeLeftS1 = 62f;
        TimeLeftS2 = 182f;
        TimeLeftS3 = 257f;
        TimeLeftS4 = 212f;
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
            }

            if(livesLeft == 5)
            {
                h1.SetActive(true);
                h2.SetActive(true);
                h3.SetActive(true);
                h4.SetActive(true);
                h5.SetActive(true);

                deadStat = false;
            }

            else if(livesLeft == 4)
            {
                h1.SetActive(true);
                h2.SetActive(true);
                h3.SetActive(true);
                h4.SetActive(true);
                h5.SetActive(false);

                deadStat = false;
            }

            else if(livesLeft == 3)
            {
                h1.SetActive(true);
                h2.SetActive(true);
                h3.SetActive(true);
                h4.SetActive(false);
                h5.SetActive(false);

                deadStat = false;
            }

            else if(livesLeft == 2)
            {
                h1.SetActive(true);
                h2.SetActive(true);
                h3.SetActive(false);
                h4.SetActive(false);
                h5.SetActive(false);

                deadStat = false;
            }

            else if(livesLeft == 1)
            {
                h1.SetActive(true);
                h2.SetActive(false);
                h3.SetActive(false);
                h4.SetActive(false);
                h5.SetActive(false);

                deadStat = false;
            }

            else if(livesLeft == 0)
            {
                h1.SetActive(false);
                h2.SetActive(false);
                h3.SetActive(false);
                h4.SetActive(false);
                h5.SetActive(false);

                deadStat = true;
            }

            else
            {
                //do nothing
            }
        }

        else
        {
            //Do nothing
        }

        if(LevelOn == 3)
        {
            TimeLeftS1 -= Time.deltaTime;
            if(TimeLeftS1 <= 0)
            {
                canMove = false;
                deadStat = true;
            }
        }

        else if(LevelOn == 4)
        {
            TimeLeftS2 -= Time.deltaTime;
            if(TimeLeftS2 <= 0)
            {
                canMove = false;
                deadStat = true;
            }
        }

        else if(LevelOn == 5)
        {
            TimeLeftS3 -= Time.deltaTime;
            if(TimeLeftS3 <= 0)
            {
                canMove = false;
                deadStat = true;
            }
        }

        else if(LevelOn == 6)
        {
            TimeLeftS4 -= Time.deltaTime;
            if(TimeLeftS4 <= 0)
            {
                canMove = false;
                deadStat = true;
            }
        }
    }

    void playerMove()
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
            Invoke("deactHitChck", .2f);
            if(LevelOn == 3)
            {
                transform.position = currTransPos;
            }

            else if(LevelOn == 4)
            {
                transform.position = currTransPos;
            }

            else if(LevelOn == 5)
            {
                transform.position = currTransPos;
            }

            else if(LevelOn == 6)
            {
                transform.position = currTransPos;
            }

            else if(LevelOn == 7)
            {
                transform.position = currTransPos;
            }
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
            if(LevelOn == 3)
            {
                SceneManager.LoadScene("StageTwo");
            }

            else if(LevelOn == 4)
            {
                SceneManager.LoadScene("StageThree");
            }

            else if(LevelOn == 5)
            {
                SceneManager.LoadScene("StageFour");
            }

            else if(LevelOn == 6)
            {
                SceneManager.LoadScene("StageFive");
            }

            else if(LevelOn == 7)
            {
                SceneManager.LoadScene("TheEnd");
            }
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

    void closeChck()
    {
        chckPnt.SetActive(false);
    }

    void moveSwitch()
    {
        canMove = !canMove;
    }

    void relifeScreenOff()
    {
        relifeFlash.SetActive(false);
    }

    void deactHitChck()
    {
        hitChck = false;
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
