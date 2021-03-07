using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [HideInInspector]
    public int score;

    [HideInInspector] private Leaderboard m_leaderboard;

    private ScoreManager()
    {
    }

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    public void SetLeaderboard(Leaderboard leaderboard)
    {
        m_leaderboard = leaderboard;
    }

    public void SaveScore(string name)
    {
        m_leaderboard.AddHighscoreEntry(score, name);
    }

}
