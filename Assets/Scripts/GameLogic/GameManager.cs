using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.SpaceshipScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameManager(){}


    private SpaceshipPlayer m_spaceshipPlayerObject;
    public enum enGameState {Preload, MainMenu, Playing, Pause, GameOver}

    private enGameState enCurrentGameState;


    private void Awake() {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
        DontDestroyOnLoad(gameObject);
        enCurrentGameState = enGameState.Preload;
    }

    public SpaceshipPlayer spaceshipPlayer
    {
        get
        {
            if (m_spaceshipPlayerObject == null) m_spaceshipPlayerObject = FindObjectOfType<SpaceshipPlayer>();
            return m_spaceshipPlayerObject;
        }
        set { m_spaceshipPlayerObject = value; }
    }

    private void Update() {
        switch (enCurrentGameState)
        {
            case enGameState.Preload:
            {
                //SceneManager.LoadScene("SampleScene");
                //enCurrentGameState = enGameState.Playing;
                SceneManager.LoadScene("MainMenu");
                enCurrentGameState = enGameState.MainMenu;
                    break;
            }
            case enGameState.MainMenu:
            {
                break;
            }
            case enGameState.Playing:
            {
                break;
            }
            case enGameState.Pause:
            {
                break;
            }
            case enGameState.GameOver:
            {
                break;
            }
        }
    }

    public void StartGame()
    {
        enCurrentGameState = enGameState.Playing;
        SceneManager.LoadScene("SampleScene");
    }

    public void PauseGame()
    {
        enCurrentGameState = enGameState.Pause;
        ScreenManager.Instance.SwitchFrame(ScreenManager.enMenuFrame.Pause);
    }

    public void ResumeGame()
    {
        enCurrentGameState = enGameState.Playing;
        ScreenManager.Instance.SwitchFrame(ScreenManager.enMenuFrame.HUD);
    }

    public void GameOver()
    {
        enCurrentGameState = enGameState.GameOver;
        ScreenManager.Instance.SwitchFrame(ScreenManager.enMenuFrame.GameOver);
    }

    public enGameState GetState()
    {
        return enCurrentGameState;
    }
}
