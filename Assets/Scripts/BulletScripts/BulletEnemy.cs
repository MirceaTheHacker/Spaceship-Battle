using Assets.Scripts.SpaceshipScripts;
using UnityEngine;

namespace Assets.Scripts.BulletScripts
{
    class BulletEnemy : Bullet
    {
        void Start()
        {
            m_direction = new Vector3(-1, 0, 0);
        }

        protected override void HandleSpaceshipCollision(SpaceshipObject spaceshipObject)
        {
            if (spaceshipObject as SpaceshipEnemy)
            {
                return;
            }
            base.HandleSpaceshipCollision(spaceshipObject);
        }
    }
}
