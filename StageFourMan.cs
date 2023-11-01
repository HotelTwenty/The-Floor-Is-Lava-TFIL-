using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StageFourMan : MonoBehaviour
{
    PlayerControls controls;
    public Animator leftScreen, rightScreen;
    public static bool playerLifeStat;
    public GameObject DScreen1, DScreen2, DScreen3, DScreen4, DScreen5, timeUpScreen, retryButt, quitButt;
    int rnd, rnd2;
    int LevelOn;
    public static float MusVol, SoundFXVol;
    public AudioSource stageVol, bSelect, bPress;
    public bool retrySel, quitSel;

    public float TimeLeft;
    public bool TimerOn = false;
    public Text TimerTxt;
    public GameObject Player1, Player2;

    public Animator timerRunDown;

    void Awake()
    {
        controls = new PlayerControls();
        controls.MenuNav.MoveLeft.performed += ctx => moveButtLeft();
        controls.MenuNav.MoveRight.performed += ctx => moveButRight();
        controls.MenuNav.SelectBut.performed += ctx => buttSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelOn = SceneManager.GetActiveScene().buildIndex;
        rnd = Random.Range(1, 6);
        DScreen1.SetActive(false);
        DScreen2.SetActive(false);
        DScreen3.SetActive(false);
        DScreen4.SetActive(false);
        DScreen5.SetActive(false);
        timeUpScreen.SetActive(false);
        retryButt.SetActive(false);
        quitButt.SetActive(false);
        retrySel = true;
        quitSel = false;
        Invoke("openScreens", 2f);
        rnd2 = Random.Range(1, 3);
        if(rnd2 == 1)
        {
            Player2.SetActive(true);
            Player1.SetActive(false);
        }

        else
        {
            Player2.SetActive(false);
            Player1.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MusVol = MasterSettings.MusicVolume;
        stageVol.volume = MusVol;

        SoundFXVol = MasterSettings.SFXVolume;
        bSelect.volume = SoundFXVol;
        bPress.volume = SoundFXVol;

        LevelOn = SceneManager.GetActiveScene().buildIndex;
        playerLifeStat = PlayerMove.deadStat;
        if(playerLifeStat == true)
        {
            if(rnd == 1)
            {
                DScreen1.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
            }

            else if(rnd == 2)
            {
                DScreen2.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
            }

            else if(rnd == 3)
            {
                DScreen3.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
            }

            else if(rnd == 4)
            {
                DScreen4.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
            }

            else if(rnd == 5)
            {
                DScreen5.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
            }

            else
            {
                DScreen1.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
            }
        }

        else
        {
            DScreen1.SetActive(false);
            DScreen2.SetActive(false);
            DScreen3.SetActive(false);
            DScreen4.SetActive(false);
            DScreen5.SetActive(false);
            retryButt.SetActive(false);
            quitButt.SetActive(false);
        }

        if(retrySel == true)
        {
            quitSel = false;
        }
        
        else
        {
            quitSel = true;
            retrySel = false;
        }

        if(TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            updateTimer(TimeLeft);
            timeUpScreen.SetActive(false);

            if(TimeLeft < 15)
            {
                timerRunDown.SetBool("timeRunningOut", true);
            }

            else
            {
                timerRunDown.SetBool("timeRunningOut", false);
            }
        }

        else
        {
            TimeLeft = 0;
            TimerOn = false;
            timeUpScreen.SetActive(true);
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void moveButRight()
    {
        if(playerLifeStat == true)
        {
            if(retrySel == true)
            {
                retrySel = false;
                quitSel = true;
            }
        }
    }

    public void moveButtLeft()
    {
        bPress.Play();
        if(playerLifeStat == true)
        {
            if(quitSel == true)
            {
                retrySel = true;
                quitSel = false;
            }
        }
    }

    public void buttSelect()
    {
        bPress.Play();
        if(playerLifeStat == true)
        {
            if(retrySel == true)
            {
                reloadGame();
            }

            else
            {
                backToMM();
            }
        }
    }

    public void reloadGame()
    {
        bSelect.Play();
        SceneManager.LoadScene("StageFour");
    }

    public void backToMM()
    {
        bSelect.Play();
        SceneManager.LoadScene("MainMenu");
    }

    void openScreens()
    {
        leftScreen.SetBool("isOpen", true);
        rightScreen.SetBool("isOpen", true);
    }

    void OnEnable()
    {
        controls.MenuNav.Enable();
    }

    void OnDisable()
    {
        controls.MenuNav.Disable();
    }
}
