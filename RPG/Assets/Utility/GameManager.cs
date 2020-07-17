using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Pregame, Running, Paused
    }

    [SerializeField] GameObject[] systemPrefabs = null;    

    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    public System.Action<GameState, GameState> OnGameStateChanged;

    List<AsyncOperation> loadOperations;
    List<GameObject> instancedSystemPrefabs;

    string currentLevel = string.Empty;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();
    }


    public void LoadLevel(string levelName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (asyncOperation == null)
        {
            Debug.LogError("Unable to load level " + levelName);
            return;
        }

        asyncOperation.completed += OnLoadOperationCompleted;
        loadOperations.Add(asyncOperation);

        currentLevel = levelName;
    }
    public void UnloadLevel(string levelName)
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(levelName);
        if (asyncOperation == null)
        {
            Debug.LogError("Unable to unload level " + levelName);
            return;
        }

        asyncOperation.completed += OnUnloadOperationCompleted;
    }
    public void StartGame()
    {
        //LoadLevel("name goes here");
    }
    public void TogglePause()
    {
        if(CurrentGameState == GameState.Running)
        {
            UpdateGameState(GameState.Paused);
        }
        else if(CurrentGameState == GameState.Paused)
        {
            UpdateGameState(GameState.Running);
        }
    }
    public void RestartGame()
    {
        UpdateGameState(GameState.Pregame);
    }
    public void QuitGame()
    {
        // implement features for quitting
        Application.Quit();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var instance in instancedSystemPrefabs)
        {
            Destroy(instance);
        }
        instancedSystemPrefabs.Clear();
    }

    void OnLoadOperationCompleted(AsyncOperation asyncOperation)
    {
        if (loadOperations.Contains(asyncOperation))
        {
            loadOperations.Remove(asyncOperation);
            if(loadOperations.Count == 0)
            {
                UpdateGameState(GameState.Running);
            }            
        }
        Debug.Log("Load Complete");
    }
    void OnUnloadOperationCompleted(AsyncOperation asyncOperation)
    {
        Debug.Log("Unload Complete");
    }
    void InstantiateSystemPrefabs()
    {
        foreach (var systemPrefab in systemPrefabs)
        {
            if(systemPrefab != null)
            {
                instancedSystemPrefabs.Add(Instantiate(systemPrefab));
            }
        }
    }
    void UpdateGameState(GameState newGameState)
    {
        GameState previousGameState = CurrentGameState;
        CurrentGameState = newGameState;

        switch (CurrentGameState)
        {
            case GameState.Pregame:
                Time.timeScale = 1f;
                break;
            case GameState.Running:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            default:
                break;
        }
        // main menu will register to this event
        OnGameStateChanged?.Invoke(previousGameState, CurrentGameState);
        // transition between scenes
    }
}
