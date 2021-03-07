using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.BulletScripts;
using UnityEngine;

namespace Assets.Scripts.SpaceshipScripts
{
    public class SpaceshipPlayer : SpaceshipObject
    {
        private PlayerConfiner m_playerConfiner;

        protected override void Start()
        {
            GameManager.Instance.spaceshipPlayer = this;
            m_playerConfiner = GetComponent<PlayerConfiner>();
            m_bulletTtype = typeof(BulletPlayer);
        }

        public override void Move(Vector2 direction)
        {
            m_playerConfiner.ClampDirection(ref direction);
            transform.Translate(direction * m_speed * Time.deltaTime);
        }

        protected override void HandleSpaceshipCollision(SpaceshipObject spaceshipObject)
        {
            
        }

        protected override void PlayFireSFX()
        {
            base.PlayFireSFX();
            SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerAttack);
        }

        protected override void Death()
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.PlayerDie);
            GameManager.Instance.GameOver();
            base.Death();
            
        }
    }
}
