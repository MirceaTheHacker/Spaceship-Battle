using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCustom : MonoBehaviour
{
    private bool m_touchStart;
    private Vector2 m_pointA;
    private Vector2 m_pointB;

    public Transform m_circle;
    public Transform m_outerCircle;

    private Color m_colorOn;
    private Color m_colorOff;


    void Start()
    {
        m_touchStart = false;
        m_colorOn = Color.white;
        m_colorOff = new Color(1, 1, 1, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_pointA = new Vector2(m_outerCircle.transform.position.x, m_outerCircle.transform.position.y);
        }
        if (Input.GetMouseButton(0))
        {
            m_pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            if(IsPointInControlArea(m_pointB))
            {
                m_touchStart = true;
                m_outerCircle.GetComponent<SpriteRenderer>().color = m_colorOn;
                m_circle.GetComponent<SpriteRenderer>().color = m_colorOn;
            }
        }
        else
        {
            m_outerCircle.GetComponent<SpriteRenderer>().color = m_colorOff;
            m_circle.GetComponent<SpriteRenderer>().color = m_colorOff;
            m_circle.transform.position = m_outerCircle.transform.position;
            m_touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        if (m_touchStart)
        {
            Vector2 offset = m_pointB - m_pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            OnJoystickTouch(direction);

            m_circle.transform.position = new Vector2(m_pointA.x + direction.x, m_pointA.y + direction.y);
        }
    }
    private void OnJoystickTouch(Vector2 direction)
    {
        GameEvents.Instance.JoystickTouch(direction);
    }

    // only allow joystick controll for the right side of the screen.
    // the left side will be used for the fire button
    private bool IsPointInControlArea(Vector3 point)
    {
        if (point.x > 0)
        {
            return true;
        }
        return false;
    }
}