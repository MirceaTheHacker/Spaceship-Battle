using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfiner : MonoBehaviour
{
    private Camera m_camera;
    private int m_marginLeft, m_marginRight, m_marginTop, m_marginBottom;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        Rect playerRect = GetComponent<SpriteRenderer>().sprite.rect;
        int playerWidth = (int) (playerRect.width * transform.localScale.x);
        int playerHeight = (int)(playerRect.height * transform.localScale.y);
        m_marginLeft = 0 + playerWidth / 2;
        m_marginBottom = 0 + playerHeight / 2;
        m_marginRight = m_camera.pixelWidth - playerWidth / 2;
        m_marginTop = m_camera.pixelHeight - playerHeight / 2;
    }

    public void ClampDirection(ref Vector2 direction)
    {
        Vector3 screenPos = m_camera.WorldToScreenPoint(transform.position);
        screenPos.x += direction.x;
        screenPos.y += direction.y;
        if (screenPos.x < m_marginLeft && direction.x < 0)
        {
            direction.x = 0;
        }
        else if (screenPos.x > m_marginRight && direction.x > 0)
        {
            direction.x = 0;
        }
        if (screenPos.y < m_marginBottom && direction.y < 0)
        {
            direction.y = 0;
        }
        else if (screenPos.y > m_marginTop && direction.y > 0)
        {
            direction.y = 0;
        }
    }
}
