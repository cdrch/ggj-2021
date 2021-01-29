using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    public int difficultyLevel = 1;

    public float elevatorSpeed = 1f;
    public float elevatorSpeedChangePerDifficultyLevel = 0.25f; 
    // each level, elevator speed increases by elevatorSpeed * elevatorSpeedChangePerDifficultyLevel

    private Camera cam;
    private TreeTrunk treeTrunk;

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

        // caching expensive operations
        cam = Camera.main;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            treeTrunk = GameObject.Find("Trunk").GetComponent<TreeTrunk>();
        }        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
