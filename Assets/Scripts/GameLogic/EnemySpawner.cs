using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("How often to spawn a new enemy.")]
    public float m_timer = 2f;
    private float m_timerCurrent;

    private int m_spawnX, m_spawnYmin, m_spawnYmax;
    // Start is called before the first frame update
    void Start()
    {
        int viewportWidthInWorldUnits = (int)Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        int viewportHeightInWorldUnits = (int)Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)).y;
        m_spawnX = (int)(viewportWidthInWorldUnits * 1.2f); // 20% extra to compensate for spaceship width
        m_spawnYmin = -viewportHeightInWorldUnits;
        m_spawnYmax = viewportHeightInWorldUnits;

        m_timerCurrent = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetState() != GameManager.enGameState.Playing) return;
        m_timerCurrent += Time.deltaTime;
        if (m_timerCurrent >= m_timer)
        {
            m_timerCurrent = 0;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        float spawnY = Random.Range(m_spawnYmin, m_spawnYmax);
        Vector3 spawnPos = transform.position;
        spawnPos.x = m_spawnX;
        spawnPos.y = spawnY;
        GameObject spaceshipEnemy = ObjectPooler.Instance.GetPooledObject(typeof(SpaceshipEnemy));
        spaceshipEnemy.transform.position = spawnPos;
        spaceshipEnemy.SetActive(true);
    }
}
