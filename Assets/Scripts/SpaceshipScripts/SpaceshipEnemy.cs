using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BulletScripts;
using Assets.Scripts.SpaceshipScripts;
using UnityEngine;

public class SpaceshipEnemy : SpaceshipObject
{
    private Vector2 m_direction;
    [Tooltip("How many points does is this enemy worth.")]
    public int score;
    // Start is called before the first frame update
    protected override void Start()
    {
        m_direction = new Vector2(0, 1);
        m_bulletTtype = typeof(BulletEnemy);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.Instance.GetState() != GameManager.enGameState.Playing) return;
        base.Update();
        Move(m_direction);
        Fire();
    }

    protected override void PlayFireSFX()
    {
        base.PlayFireSFX();
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyAttack);
    }

    protected override void Death()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        ScoreManager.Instance.score += score;
        base.Death();
    }

    protected override void HandleSpaceshipCollision(SpaceshipObject spaceshipObject)
    {
        if (spaceshipObject is SpaceshipPlayer)
        {
            base.HandleSpaceshipCollision(spaceshipObject);
        }
    }
}
