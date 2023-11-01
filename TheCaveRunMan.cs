using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TheCaveRunMan : MonoBehaviour
{
    private Transform target;
    PlayerControls controls;
    public static bool playerLifeStat;
    public GameObject DScreen1, DScreen2, DScreen3, DScreen4, DScreen5, retryButt, quitButt;
    int rnd;
    int LevelOn;
    public static float MusVol, SoundFXVol;
    public AudioSource stageVol, bSelect, bPress, txtSound;
    public bool retrySel, quitSel;

    //Timer stuff
    public static float TimeLeft, HSTS;
    public static float currentScore, MSHS;
    public bool TimerOn;
    public Text scoreText, timeTxt, HScoreTxt, HTimeTxt, currStgTxt, highStgTxt;
    public static int stageNumber, HSNum;
    public static int currLifeStat;
    public Animator playerUIBG;

    public GameObject YouShouldKnow, jumpSpit, heartBeat, dontTouch, theLava, blacker, lvlMusic;
    public int rnds1, rnds2, rnds3, rnds4, rnds5, rnds6, rnds7, rnds8, rnds9, rnds10;
    
    //Stages
    public GameObject s1, s2, s3, s4, s5, s6, s7, s8, s9, s10;

    //StageEnems
    //s1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s1NumSep = 0;
    public GameObject z1LizMaze, z1GeyserSpawns;

    //s2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s2NumSep = 2;
    public GameObject z2LizMaze, z2GeyserSpawns, z2UDHeads, z2UDHeadBulls, z2UDSTSLiz;

    //s3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s3NumSep = 3;
    public GameObject z3LizMaze, z3GeyserSpawns, z3UDHeads, z3UDHeadBulls, z3UDSTSLiz1, z3UDSTSLiz2, z3LavaMaze;

    //s4 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s4NumSep = 4;
    public GameObject z4LizMaze, z4GeyserSpawns, z4UDHeads, z4UDHeadBulls, z4UDSTSLiz1, z4UDSTSLiz2, z4UDSTSLiz3, z4LavaMaze1, z4LavaMaze2, z4SuperFlies;

    //s5 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s5NumSep = 5;
    public GameObject z5LizMaze, z5GeyserSpawns, z5UDHeads, z5UDHeadBulls, z5UDHeadSTS, z5UDSTSLiz1, z5UDSTSLiz2, z5UDSTSLiz3, z5LavaMaze1, z5LavaMaze2, z5LavaMaze3, z5SuperFlies, z5CircLiz;
    
    //s6 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s6NumSep = 6;
    public GameObject z6LizMaze, z6GeyserSpawns, z6UDHeads, z6UDHeadBulls, z6UDHeadSTS, z6UDSTSLiz1, z6UDSTSLiz2, z6UDSTSLiz3, z6LavaMaze1, z6LavaMaze2, z6LavaMaze3;
    public GameObject z6SuperFlies, z6CircLiz, z6UDFlies;

    //s7 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s7NumSep = 7;
    public GameObject z7LizMaze, z7GeyserSpawns, z7UDHeads, z7UDHeadBulls, z7UDHeadSTS, z7UDSTSLiz1, z7UDSTSLiz2, z7UDSTSLiz3, z7LavaMaze1, z7LavaMaze2, z7LavaMaze3;
    public GameObject z7SuperFlies, z7CircLiz, z7UDFlies, z7SuperFrog;

    //s8 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s8NumSep = 8;
    public GameObject Combo1Z8, Combo2Z8, Combo3Z8, Combo4Z8;

    //s8 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public int s9NumSep = 9;
    public GameObject Combo1Z9, Combo2Z9, Combo3Z9, Combo4Z9;

    void Awake()
    {
        controls = new PlayerControls();
        controls.MenuNav.MoveLeft.performed += ctx => moveButtLeft();
        controls.MenuNav.MoveRight.performed += ctx => moveButRight();
        controls.MenuNav.SelectBut.performed += ctx => buttSelect();
    }


    void Start()
    {
        rnds1 = Random.Range(1, 3); // 2 objs
        rnds2 = Random.Range(1, 6); // 5 objs
        rnds3 = Random.Range(1, 8); // 7 objs
        rnds4 = Random.Range(1, 11); // 10 objs
        rnds5 = Random.Range(1, 14); // 13 objs
        rnds6 = Random.Range(1, 15); // 14 objs
        rnds7 = Random.Range(1, 16); // 15 objs
        rnds8 = Random.Range(1, 5); // 4 objs
        rnds9 = Random.Range(1, 5); // 4 objs

        LevelOn = SceneManager.GetActiveScene().buildIndex;
        rnd = Random.Range(1, 6);
        DScreen1.SetActive(false);
        DScreen2.SetActive(false);
        DScreen3.SetActive(false);
        DScreen4.SetActive(false);
        DScreen5.SetActive(false);
        retryButt.SetActive(false);
        quitButt.SetActive(false);
        retrySel = true;
        quitSel = false;
        TimeLeft = 0;
        TimerOn = false;
        YouShouldKnow.SetActive(false);
        jumpSpit.SetActive(false);
        heartBeat.SetActive(false);
        dontTouch.SetActive(false);
        theLava.SetActive(false);
        lvlMusic.SetActive(false);

        Invoke("openYouShould", 2f);
        Invoke("openYouHeartBeat", 3f);
        Invoke("closeYouShould", 5f);
        Invoke("openJumpSpit", 6f);
        Invoke("closeJumpSpit", 11f);
        Invoke("closeYouHeartBeat", 11f);
        Invoke("openDontTouch", 12f);
        Invoke("openLava", 13f);
        Invoke("closeDontTouch", 15f);
        Invoke("closeLava", 16f);
        Invoke("closeBlackerStartMusic", 18f);
        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Stage1 Stuff ==================================================================
        if(rnds1 == 1)
        {
            z1GeyserSpawns.SetActive(true);
            z1LizMaze.SetActive(false);
        }

        else
        {
            z1GeyserSpawns.SetActive(false);
            z1LizMaze.SetActive(true);
        }

        //Stage2 Stuff ==================================================================
        if(rnds2 == 1)
        {
            z2LizMaze.SetActive(true);
            z2GeyserSpawns.SetActive(false);
            z2UDHeads.SetActive(false);
            z2UDHeadBulls.SetActive(false);
            z2UDSTSLiz.SetActive(false);
        }

        else if(rnds2 == 2)
        {
            z2LizMaze.SetActive(false);
            z2GeyserSpawns.SetActive(true);
            z2UDHeads.SetActive(false);
            z2UDHeadBulls.SetActive(false);
            z2UDSTSLiz.SetActive(false);
        }

        else if(rnds2 == 3)
        {
            z2LizMaze.SetActive(false);
            z2GeyserSpawns.SetActive(false);
            z2UDHeads.SetActive(true);
            z2UDHeadBulls.SetActive(false);
            z2UDSTSLiz.SetActive(false);
        }

        else if(rnds2 == 4)
        {
            z2LizMaze.SetActive(false);
            z2GeyserSpawns.SetActive(false);
            z2UDHeads.SetActive(false);
            z2UDHeadBulls.SetActive(true);
            z2UDSTSLiz.SetActive(false);
        }

        else if(rnds2 == 5)
        {
            z2LizMaze.SetActive(false);
            z2GeyserSpawns.SetActive(false);
            z2UDHeads.SetActive(false);
            z2UDHeadBulls.SetActive(false);
            z2UDSTSLiz.SetActive(true);
        }

        else
        {
            z2LizMaze.SetActive(true);
            z2GeyserSpawns.SetActive(false);
            z2UDHeads.SetActive(false);
            z2UDHeadBulls.SetActive(false);
            z2UDSTSLiz.SetActive(false);
        }

        //Stage3 Stuff ==================================================================
        if(rnds3 == 1)
        {
            z3LizMaze.SetActive(true);
            z3GeyserSpawns.SetActive(false);
            z3UDHeads.SetActive(false);
            z3UDHeadBulls.SetActive(false);
            z3UDSTSLiz1.SetActive(false);
            z3UDSTSLiz2.SetActive(false);
            z3LavaMaze.SetActive(false);
        }

        else if(rnds3 == 2)
        {
            z3LizMaze.SetActive(false);
            z3GeyserSpawns.SetActive(true);
            z3UDHeads.SetActive(false);
            z3UDHeadBulls.SetActive(false);
            z3UDSTSLiz1.SetActive(false);
            z3UDSTSLiz2.SetActive(false);
            z3LavaMaze.SetActive(false);
        }

        else if(rnds3 == 3)
        {
            z3LizMaze.SetActive(false);
            z3GeyserSpawns.SetActive(false);
            z3UDHeads.SetActive(true);
            z3UDHeadBulls.SetActive(false);
            z3UDSTSLiz1.SetActive(false);
            z3UDSTSLiz2.SetActive(false);
            z3LavaMaze.SetActive(false);
        }

        else if(rnds3 == 4)
        {
            z3LizMaze.SetActive(false);
            z3GeyserSpawns.SetActive(false);
            z3UDHeads.SetActive(false);
            z3UDHeadBulls.SetActive(true);
            z3UDSTSLiz1.SetActive(false);
            z3UDSTSLiz2.SetActive(false);
            z3LavaMaze.SetActive(false);
        }

        else if(rnds3 == 5)
        {
            z3LizMaze.SetActive(false);
            z3GeyserSpawns.SetActive(false);
            z3UDHeads.SetActive(false);
            z3UDHeadBulls.SetActive(false);
            z3UDSTSLiz1.SetActive(true);
            z3UDSTSLiz2.SetActive(false);
            z3LavaMaze.SetActive(false);
        }

        else if(rnds3 == 6)
        {
            z3LizMaze.SetActive(false);
            z3GeyserSpawns.SetActive(false);
            z3UDHeads.SetActive(false);
            z3UDHeadBulls.SetActive(false);
            z3UDSTSLiz1.SetActive(false);
            z3UDSTSLiz2.SetActive(true);
            z3LavaMaze.SetActive(false);
        }

        else if(rnds3 == 7)
        {
            z3LizMaze.SetActive(false);
            z3GeyserSpawns.SetActive(false);
            z3UDHeads.SetActive(false);
            z3UDHeadBulls.SetActive(false);
            z3UDSTSLiz1.SetActive(false);
            z3UDSTSLiz2.SetActive(false);
            z3LavaMaze.SetActive(true);
        }

        else
        {
            z3LizMaze.SetActive(true);
            z3GeyserSpawns.SetActive(false);
            z3UDHeads.SetActive(false);
            z3UDHeadBulls.SetActive(false);
            z3UDSTSLiz1.SetActive(false);
            z3UDSTSLiz2.SetActive(false);
            z3LavaMaze.SetActive(false);
        }

        //Stage4 Stuff ==================================================================
        if(rnds4 == 1)
        {
            z4LizMaze.SetActive(true);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 2)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(true);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 3)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(true);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 4)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(true);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 5)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(true);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 6)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(true);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 7)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(true);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 8)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(true);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 9)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(true);
            z4SuperFlies.SetActive(false);
        }

        else if(rnds4 == 10)
        {
            z4LizMaze.SetActive(false);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(true);
        }

        else
        {
            z4LizMaze.SetActive(true);
            z4GeyserSpawns.SetActive(false);
            z4UDHeads.SetActive(false);
            z4UDHeadBulls.SetActive(false);
            z4UDSTSLiz1.SetActive(false);
            z4UDSTSLiz2.SetActive(false);
            z4UDSTSLiz3.SetActive(false);
            z4LavaMaze1.SetActive(false);
            z4LavaMaze2.SetActive(false);
            z4SuperFlies.SetActive(false);
        }

        //Stage5 Stuff ==================================================================
        if(rnds5 == 1)
        {
            z5LizMaze.SetActive(true);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 2)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(true);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 3)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(true);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 4)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(true);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 5)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(true);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 6)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(true);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 7)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(true);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 8)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(true);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 9)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(true);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 10)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(true);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 11)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(true);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 12)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(true);
            z5CircLiz.SetActive(false);
        }

        else if(rnds5 == 13)
        {
            z5LizMaze.SetActive(false);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(true);
        }

        else 
        {
            z5LizMaze.SetActive(true);
            z5GeyserSpawns.SetActive(false);
            z5UDHeads.SetActive(false);
            z5UDHeadBulls.SetActive(false);
            z5UDHeadSTS.SetActive(false);
            z5UDSTSLiz1.SetActive(false);
            z5UDSTSLiz2.SetActive(false);
            z5UDSTSLiz3.SetActive(false);
            z5LavaMaze1.SetActive(false);
            z5LavaMaze2.SetActive(false);
            z5LavaMaze3.SetActive(false);
            z5SuperFlies.SetActive(false);
            z5CircLiz.SetActive(false);
        }

        //Stage6 Stuff ==================================================================
        if(rnds6 == 1)
        {
            z6LizMaze.SetActive(true);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 2)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(true);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 3)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(true);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 4)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(true);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 5)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(true);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 6)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(true);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 7)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(true);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 8)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(true);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 9)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(true);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 10)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(true);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 11)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(true);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 12)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(true);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 13)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(true);
            z6UDFlies.SetActive(false);
        }

        else if(rnds6 == 14)
        {
            z6LizMaze.SetActive(false);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(true);
        }

        else 
        {
            z6LizMaze.SetActive(true);
            z6GeyserSpawns.SetActive(false);
            z6UDHeads.SetActive(false);
            z6UDHeadBulls.SetActive(false);
            z6UDHeadSTS.SetActive(false);
            z6UDSTSLiz1.SetActive(false);
            z6UDSTSLiz2.SetActive(false);
            z6UDSTSLiz3.SetActive(false);
            z6LavaMaze1.SetActive(false);
            z6LavaMaze2.SetActive(false);
            z6LavaMaze3.SetActive(false);
            z6SuperFlies.SetActive(false);
            z6CircLiz.SetActive(false);
            z6UDFlies.SetActive(false);
        }

        //Stage7 Stuff ==================================================================
        if(rnds7 == 1)
        {
            z7LizMaze.SetActive(true);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 2)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(true);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 3)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(true);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 4)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(true);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 5)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(true);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 6)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(true);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 7)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(true);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 8)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(true);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 9)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(true);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 10)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(true);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 11)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(true);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 12)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(true);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 13)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(true);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 14)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(true);
            z7SuperFrog.SetActive(false);
        }

        else if(rnds7 == 15)
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(true);
        }

        else
        {
            z7LizMaze.SetActive(false);
            z7GeyserSpawns.SetActive(false);
            z7UDHeads.SetActive(false);
            z7UDHeadBulls.SetActive(false);
            z7UDHeadSTS.SetActive(false);
            z7UDSTSLiz1.SetActive(false);
            z7UDSTSLiz2.SetActive(false);
            z7UDSTSLiz3.SetActive(false);
            z7LavaMaze1.SetActive(false);
            z7LavaMaze2.SetActive(false);
            z7LavaMaze3.SetActive(false);
            z7SuperFlies.SetActive(false);
            z7CircLiz.SetActive(false);
            z7UDFlies.SetActive(false);
            z7SuperFrog.SetActive(true);
        }

        //Stage8 Stuff ==================================================================
        if(rnds8 == 1)
        {
            Combo1Z8.SetActive(true);
            Combo2Z8.SetActive(false);
            Combo3Z8.SetActive(false);
            Combo4Z8.SetActive(false);
        }

        else if(rnds8 == 2)
        {
            Combo1Z8.SetActive(false);
            Combo2Z8.SetActive(true);
            Combo3Z8.SetActive(false);
            Combo4Z8.SetActive(false);
        }

        else if(rnds8 == 3)
        {
            Combo1Z8.SetActive(false);
            Combo2Z8.SetActive(false);
            Combo3Z8.SetActive(true);
            Combo4Z8.SetActive(false);
        }

        else if(rnds8 == 4)
        {
            Combo1Z8.SetActive(false);
            Combo2Z8.SetActive(false);
            Combo3Z8.SetActive(false);
            Combo4Z8.SetActive(true);
        }

        else
        {
            Combo1Z8.SetActive(true);
            Combo2Z8.SetActive(false);
            Combo3Z8.SetActive(false);
            Combo4Z8.SetActive(false);
        }

        //Stage9 Stuff ==================================================================
        if(rnds9 == 1)
        {
            Combo1Z9.SetActive(true);
            Combo2Z9.SetActive(false);
            Combo3Z9.SetActive(false);
            Combo4Z9.SetActive(false);
        }

        else if(rnds9 == 2)
        {
            Combo1Z9.SetActive(false);
            Combo2Z9.SetActive(true);
            Combo3Z9.SetActive(false);
            Combo4Z9.SetActive(false);
        }

        else if(rnds9 == 3)
        {
            Combo1Z9.SetActive(false);
            Combo2Z9.SetActive(false);
            Combo3Z9.SetActive(true);
            Combo4Z9.SetActive(false);
        }

        else if(rnds9 == 4)
        {
            Combo1Z9.SetActive(false);
            Combo2Z9.SetActive(false);
            Combo3Z9.SetActive(false);
            Combo4Z9.SetActive(true);
        }

        else
        {
            Combo1Z9.SetActive(true);
            Combo2Z9.SetActive(false);
            Combo3Z9.SetActive(false);
            Combo4Z9.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        currLifeStat = PLayerMoveMFCR.livesLeft;
        MusVol = MasterSettings.MusicVolume;
        stageVol.volume = MusVol;

        SoundFXVol = MasterSettings.SFXVolume;
        bSelect.volume = SoundFXVol;
        bPress.volume = SoundFXVol;
        txtSound.volume = SoundFXVol;

        LevelOn = SceneManager.GetActiveScene().buildIndex;
        playerLifeStat = PLayerMoveMFCR.deadStat;

        HSTS = MasterSettings.timeGone;
        MSHS = MasterSettings.HighScore;
        HSNum = MasterSettings.highStage;
        
        float minutes = Mathf.FloorToInt(HSTS / 60);
        float seconds = Mathf.FloorToInt(HSTS % 60);
        HTimeTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        
        //scoreText.text = currentScore.ToString();
        //HScoreTxt.text = MSHS.ToString();
        HScoreTxt.text = ((int)MSHS).ToString();
        currStgTxt.text = stageNumber.ToString();
        highStgTxt.text = HSNum.ToString();

        Mathf.RoundToInt(currentScore);
        Mathf.RoundToInt(MSHS);

        if(playerLifeStat == true)
        {
            if(rnd == 1)
            {
                DScreen1.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
                TimerOn = false;
            }

            else if(rnd == 2)
            {
                DScreen2.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
                TimerOn = false;
            }

            else if(rnd == 3)
            {
                DScreen3.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
                TimerOn = false;
            }

            else if(rnd == 4)
            {
                DScreen4.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
                TimerOn = false;
            }

            else if(rnd == 5)
            {
                DScreen5.SetActive(true);
                retryButt.SetActive(true);
                quitButt.SetActive(true);
                TimerOn = false;
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

        if(currLifeStat == 1)
        {
            playerUIBG.SetBool("onLastLife", true);
        }

        else
        {
            playerUIBG.SetBool("onLastLife", false);
        }

        if(TimerOn == true)
        {
            TimeLeft += Time.deltaTime;
            currentScore += Time.deltaTime;
            updateTimer(TimeLeft);
            updateScore(currentScore);
        }

        else
        {
            //do nothing
        }

        if(target.position.x < 10000f && target.position.x > 7030.5f)
        {
            stageNumber = 10;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(true);
        }

        else if(target.position.x <= 7030.5f && target.position.x > 6231.4f)
        {
            stageNumber = 9;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(true);
            s10.SetActive(false);
        }

        else if(target.position.x <= 6231.4f && target.position.x > 5433.4f)
        {
            stageNumber = 8;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(true);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else if(target.position.x <= 5433.4f && target.position.x > 4635.8f)
        {
            stageNumber = 7;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(true);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else if(target.position.x <= 4635.8f && target.position.x > 3838.3f)
        {
            stageNumber = 6;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(true);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else if(target.position.x <= 3838.3f && target.position.x > 3039.2f)
        {
            stageNumber = 5;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(true);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else if(target.position.x <= 3039.2f && target.position.x > 2240.1f)
        {
            stageNumber = 4;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(true);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else if(target.position.x <= 2240.1f && target.position.x > 1443.3f)
        {
            stageNumber = 3;
            s1.SetActive(false);
            s2.SetActive(false);
            s3.SetActive(true);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else if(target.position.x <= 1443.3f && target.position.x > 644.9f)
        {
            stageNumber = 2;
            s1.SetActive(false);
            s2.SetActive(true);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else if(target.position.x <= 644.9f && target.position.x > -100f)
        {
            stageNumber = 1;
            s1.SetActive(true);
            s2.SetActive(false);
            s3.SetActive(false);
            s4.SetActive(false);
            s5.SetActive(false);
            s6.SetActive(false);
            s7.SetActive(false);
            s8.SetActive(false);
            s9.SetActive(false);
            s10.SetActive(false);
        }

        else
        {
            //do nothing
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void updateScore(float currentTime2)
    {
        currentTime2 += 10;

        //float minutes = Mathf.FloorToInt(currentTime / 60);
        //float seconds = Mathf.FloorToInt(currentTime % 60);

        //timeTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        //scoreText.text = currentScore.ToString();
        scoreText.text = ((int)currentScore).ToString();
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

        else
        {
            //do nothing
        }
    }

    public void reloadGame()
    {
        bSelect.Play();
        SceneManager.LoadScene("TheCaveRun");
    }

    public void backToMM()
    {
        bSelect.Play();
        SceneManager.LoadScene("MainMenCR");
    }

    void OnEnable()
    {
        controls.MenuNav.Enable();
    }

    void OnDisable()
    {
        controls.MenuNav.Disable();
    }

    void openYouShould()
    {
        txtSound.Play();
        YouShouldKnow.SetActive(true);
    }

    void closeYouShould()
    {
        YouShouldKnow.SetActive(false);
    }

    void openYouHeartBeat()
    {
        txtSound.Play();
        heartBeat.SetActive(true);
    }

    void closeYouHeartBeat()
    {
        heartBeat.SetActive(false);
    }

    void openJumpSpit()
    {
        txtSound.Play();
        jumpSpit.SetActive(true);
    }

    void closeJumpSpit()
    {
        jumpSpit.SetActive(false);
    }

    void openDontTouch()
    {
        txtSound.Play();
        dontTouch.SetActive(true);
    }

    void closeDontTouch()
    {
        dontTouch.SetActive(false);
    }

    void openLava()
    {
        txtSound.Play();
        theLava.SetActive(true);
    }

    void closeLava()
    {
        theLava.SetActive(false);
    }

    void closeBlackerStartMusic()
    {
        blacker.SetActive(false);
        lvlMusic.SetActive(true);
        TimerOn = true;
    }
}
