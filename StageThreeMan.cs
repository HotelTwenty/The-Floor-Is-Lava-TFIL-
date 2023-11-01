using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StageThreeMan : MonoBehaviour
{
    PlayerControls controls;
    public Animator leftScreen, rightScreen;
    public static bool playerLifeStat;
    public GameObject DScreen1, DScreen2, DScreen3, DScreen4, DScreen5, timeUpScreen, retryButt, quitButt;
    int rnd;
    int LevelOn;
    public static float MusVol, SoundFXVol;
    public AudioSource stageVol, bSelect, bPress;
    public bool retrySel, quitSel;
    public GameObject MS1, MS2, MS3, MS4, MS5, MS6, MS7, MS8, MS9, MS10, MS11, MS12, MS13, MS14, MS15, MS16, MS17, MS18, MS19, MS20, MS21, MS22, MS23, MS24, MS25, MS26;
    public GameObject MS27, MS28, MS29, MS30, MS31, MS32, MS33, MS34, MS35, MS36, MS37;

    public float TimeLeft;
    public bool TimerOn = false;
    public Text TimerTxt;

    public Animator timerRunDown;

    void Awake()
    {
        controls = new PlayerControls();
        controls.MenuNav.MoveLeft.performed += ctx => moveButtLeft();
        controls.MenuNav.MoveRight.performed += ctx => moveButRight();
        controls.MenuNav.SelectBut.performed += ctx => buttSelect();
        MS1.SetActive(false);
        MS2.SetActive(false);
        MS3.SetActive(false);
        MS4.SetActive(false);
        MS5.SetActive(false);
        MS6.SetActive(false);
        MS7.SetActive(false);
        MS8.SetActive(false);
        MS9.SetActive(false);
        MS10.SetActive(false);
        MS11.SetActive(false);
        MS12.SetActive(false);
        MS13.SetActive(false);
        MS14.SetActive(false);
        MS15.SetActive(false);
        MS16.SetActive(false);
        MS17.SetActive(false);
        MS18.SetActive(false);
        MS19.SetActive(false);
        MS20.SetActive(false);
        MS21.SetActive(false);
        MS22.SetActive(false);
        MS23.SetActive(false);
        MS24.SetActive(false);
        MS25.SetActive(false);
        MS26.SetActive(false);
        MS27.SetActive(false);
        MS28.SetActive(false);
        MS29.SetActive(false);
        MS30.SetActive(false);
        MS31.SetActive(false);
        MS32.SetActive(false);
        MS33.SetActive(false);
        MS34.SetActive(false);
        MS35.SetActive(false);
        MS36.SetActive(false);
        MS37.SetActive(false);
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
        SceneManager.LoadScene("StageThree");
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
