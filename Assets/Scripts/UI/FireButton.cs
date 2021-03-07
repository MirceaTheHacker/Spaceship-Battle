using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IUpdateSelectedHandler
{
    public bool m_isPressed;
    public void OnUpdateSelected(BaseEventData data)
    {
        if (m_isPressed)
        {
            GameEvents.Instance.Fire();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        m_isPressed = true;
    }
    
    public void OnPointerUp(PointerEventData data)
    {
        m_isPressed = false;
    }
}
