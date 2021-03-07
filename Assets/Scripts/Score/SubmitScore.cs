using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitScore : MonoBehaviour
{
    public InputField m_inputField;
    public GameObject m_leaderboardObject;
    private Leaderboard m_leaderboard;
    private string m_name;

    void Start()
    {
        m_inputField.onValueChanged.AddListener(OnNameChanged);
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickSubmit);
        m_leaderboard = m_leaderboardObject.GetComponent<Leaderboard>();
        ScoreManager.Instance.SetLeaderboard(m_leaderboard);
    }

    private void OnNameChanged(string arg0)
    {
        m_name = arg0;
    }

    private void OnClickSubmit()
    {
        ScoreManager.Instance.SaveScore(m_name);
        ScreenManager.Instance.SwitchFrame(ScreenManager.enMenuFrame.Leaderboard);
    }
}
