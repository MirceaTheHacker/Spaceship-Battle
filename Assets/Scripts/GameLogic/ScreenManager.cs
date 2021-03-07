using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;
    private ScreenManager()
    {
    }

    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    public enum enMenuFrame { HUD, Pause, GameOver, Leaderboard }

    public MenuFrame[] menuFrames;
    [System.Serializable]
    public class MenuFrame
    {
        public GameObject container;
        public enMenuFrame type;
    }

    private MenuFrame m_currentMenuFrame;

    // Start is called before the first frame update
    void Start()
    {
        foreach (MenuFrame menuFrame in menuFrames)
        {
            if (menuFrame.type == enMenuFrame.HUD)
            {
                m_currentMenuFrame = menuFrame;
                break;
            }
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchFrame(enMenuFrame menuFrameType)
    {
        m_currentMenuFrame.container.SetActive(false);
        foreach (MenuFrame menuFrame in menuFrames)
        {
            if (menuFrame.type == menuFrameType)
            {
                menuFrame.container.SetActive(true);
                m_currentMenuFrame = menuFrame;
                break;
            }
        }
    }
}
