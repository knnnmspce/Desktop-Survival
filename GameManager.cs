using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//The purpose of this script is to act as the Game Manager by loading scenes, controlling the game state and controlling the activity of gameobjects.

public class GameManager : Singleton<GameManager>
{
    #region Declarations

    private bool aoComplete;
    private GameObject[] objInRuntime;
    private PlayerMovement playerMovement;
    private GameObject countDown;

    public GameObject prompt;

    #endregion


    #region State Enum
    //Creation of a small state machine that will determine if the game is on the loading menu, when it should load and switch scenes, when to start the countdown and when the game is running.
    public enum GameState
    {
        LOADING,
        COUNTDOWN,
        RUNNING,
    }

    private GameState currentState = GameState.LOADING;

    public GameState CurrentState
    {
        get
        {
            return currentState;
        }

        private set
        {
            currentState = value;
        }
    }

    #endregion

    #region Start and Disable

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        HandleState(CurrentState);
        EventBroker.CountdownEnded += OnCountDownComplete;
        EventBroker.GameRestarted += Restart;

        EnemyController.blockPowerup = false;
        EnemyController.scoreDoubled = false;
    }

    private void OnDisable()
    {
        EventBroker.CountdownEnded -= OnCountDownComplete;
        EventBroker.GameRestarted -= Restart;
    }

    #endregion

    #region State Handler

    private void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.LOADING:
                LoadLevel();
                Time.timeScale = 1.0f;
                currentState = GameState.LOADING;
                break;
            case GameState.RUNNING:
                EnableRuntimeObjs();
                currentState = GameState.RUNNING;
                break;
            case GameState.COUNTDOWN:
                currentState = GameState.COUNTDOWN;
                countDown = GameObject.FindGameObjectWithTag("Countdown");
                if(countDown != null)
                {
                    countDown.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("GameManager: Object 'Countdown', could not be found.");
                }
                break;
        }
    }

    #endregion

    #region Game Runtime Funcs
    //This section activates and deactivates an array of gameobjects based on the game state. This includes elements of the in-game UI, the enemy managers and the player's ability to shoot.
    //This is important because the these elements of the should not be active during the loading or countdown operations.
    void EnableRuntimeObjs()
    {

        foreach(GameObject obj in objInRuntime)
        {
            obj.SetActive(true);
        }

        playerMovement.enabled = true;
    }

    void DeactivateRuntimeObjs()
    {
        objInRuntime = GameObject.FindGameObjectsWithTag("Runtime Obj");
        foreach (GameObject obj in objInRuntime)
        {
            obj.SetActive(false);
        }

        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        else
        {
            Debug.LogError("GameManager: Component 'playerMovement' could not be found.");
        }
    }

    #endregion

    #region Loading Operations
    //Manages all of the loading operations using async operations
    void OnLoadComplete(AsyncOperation ao) //this is when the game has finished loading in the background
    {
        aoComplete = true;
        prompt.SetActive(true); //prompt to continue past loading screen
        DeactivateRuntimeObjs();
        countDown = GameObject.FindGameObjectWithTag("Countdown");
        if (countDown != null)
        {
            countDown.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("GameManager: Object 'Countdown', could not be found.");
        }
    }

    public void LoadLevel()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("GameManager: Could not load the level.");
            return;
        }
        ao.completed += OnLoadComplete; 
    }

    private void SwitchToGame()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        SceneManager.UnloadSceneAsync("LoadWithInstructions");
        HandleState(GameState.COUNTDOWN);
    }

    #endregion

    private void Update()
    {
        if(currentState == GameState.LOADING && aoComplete && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchToGame();
        }
    }

    private void OnCountDownComplete()
    {
        HandleState(GameState.RUNNING);
    }

    #region Restart Functions
    //When restart is selected, it needs to load everything async again so all the managers, assets and script are flushed and renewed
    public void Restart()
    {
        AsyncOperation aop = SceneManager.LoadSceneAsync("GameScene");
        aop.completed += Aop_completed;
    }

    private void Aop_completed(AsyncOperation obj)
    {
        DeactivateRuntimeObjs();
        HandleState(GameState.COUNTDOWN);
        Time.timeScale = 1f;
    }

    #endregion
}
