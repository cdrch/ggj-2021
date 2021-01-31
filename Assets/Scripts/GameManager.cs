using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;

    public GameStage stage = GameStage.None;

    public int difficultyLevel = 1;

    public float transitionHeightToStage2;
    public float transitionHeightToStage3;
    public float treeTopHeight;
    public float treeTotalHeight;

    public float elevatorSpeed = 1f;
    public float elevatorSpeedChangePerDifficultyLevel = 0.25f; 
    // each level, elevator speed increases by elevatorSpeed * elevatorSpeedChangePerDifficultyLevel

    public Camera cam;
    private TreeTrunk treeTrunk;
    private PlayerController player;

    private Canvas canvas;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI stageText;

    private bool init = false;

    public static float SQUIRREL_LENGTH_IN_METERS = 0.3048f;

    public MusicSystem music;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton pattern
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;


        if (!init)
            Init();
    }

    // caching expensive operations
    void Init()
    {
        cam = Camera.main;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                stage = GameStage.None;
                break;
            case 1:
                treeTrunk = GameObject.Find("Trunk").GetComponent<TreeTrunk>();
                player = GameObject.Find("Player Squirrel").GetComponent<PlayerController>();

                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                scoreText = canvas.transform.Find("Distance Text").GetComponent<TextMeshProUGUI>();
                stageText = canvas.transform.Find("Stage Text").GetComponent<TextMeshProUGUI>();

                stage = GameStage.One;
                music.TriggerMusicSwitch(MusicTrack.A_FULL);

                if (treeTrunk != null && player != null && canvas != null && scoreText != null)
                    init = true;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case GameStage.None:
                break;
            case GameStage.One:
                if (!init)
                    Init();
                else
                {
                    scoreText.text = "Distance: " + player.GetMetersOffGroundRounded() + "m";
                }

                if (player.GetMetersOffGroundRounded() > transitionHeightToStage2)
                {
                    stage = GameStage.Two;
                    stageText.text = "Stage: 2";
                    music.TriggerMusicSwitch(MusicTrack.B_FULL);
                }                   
                break;
            case GameStage.Two:
                scoreText.text = "Distance: " + player.GetMetersOffGroundRounded() + "m";
                if (player.GetMetersOffGroundRounded() < transitionHeightToStage2 - 1)
                {
                    stage = GameStage.One;
                    stageText.text = "Stage: 1";
                    music.TriggerMusicSwitch(MusicTrack.A_FULL);
                }
                else if (player.GetMetersOffGroundRounded() > transitionHeightToStage3)
                {
                    stage = GameStage.Three;
                    stageText.text = "Stage: 3";
                    music.TriggerMusicSwitch(MusicTrack.C_FULL);
                }
                break;
            case GameStage.Three:
                scoreText.text = "Distance: " + player.GetMetersOffGroundRounded() + "m";
                if (player.GetMetersOffGroundRounded() < transitionHeightToStage3 - 1)
                {
                    stage = GameStage.Two;
                    stageText.text = "Stage: 2";
                    music.TriggerMusicSwitch(MusicTrack.B_FULL);
                }
                    
                break;
        }
    }

    public void GoToMainMenu()
    {
        stage = GameStage.None;
        init = false;
        SceneManager.LoadScene(0);
    }

    public void ActivateGameScene()
    {
        init = false;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

public enum GameStage
{
    None, // when not in game or the like
    One,
    Two,
    Three
}