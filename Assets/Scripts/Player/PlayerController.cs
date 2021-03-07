using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BulletScripts;
using Assets.Scripts.SpaceshipScripts;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start() 
    {
        GameEvents.Instance.m_onJoystickTouch += OnMove;
        GameEvents.Instance.m_onFire += OnFire;
    }

    void OnMove(Vector2 direction)
    {
        GameManager.Instance.spaceshipPlayer.Move(direction);
    }

    void OnFire()
    {
        GameManager.Instance.spaceshipPlayer.Fire();
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameEvents.Instance.m_onJoystickTouch -= OnMove;
            GameEvents.Instance.m_onFire -= OnFire;
        }
    }
}
