using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    private static GameEvents _i;

    public static GameEvents Instance
    {
        get
        {
            if(_i == null) _i = new GameEvents();
            return _i;
        }
    }

    private GameEvents()
    {
    }

    public event Action<Vector2> m_onJoystickTouch;
    public event Action m_onFire;

    public void JoystickTouch(Vector2 direction)
    {
        if (m_onJoystickTouch != null)
        {
            m_onJoystickTouch(direction);
        }
    }

    public void Fire()
    {
        if (m_onFire != null)
        {
            m_onFire();
        }
    }
}
