using Assets.Scripts.SpaceshipScripts;
using UnityEngine;

namespace Assets.Scripts.BulletScripts
{
    public abstract class Bullet : MonoBehaviour {

        public float m_speed = 5f;
        public float m_deactivateTimer = 3f;
        private float m_currentDeactivateTimer;
        protected Vector3 m_direction;
        private float m_damage;

        // Start is called before the first frame update
        void OnEnable() {
            m_currentDeactivateTimer = 0;
        }

        // Update is called once per frame
        void Update() {
            if (GameManager.Instance.GetState() != GameManager.enGameState.Playing) return;
            Move();
            UpdateDeactivator();
        }

        void UpdateDeactivator()
        {
            m_currentDeactivateTimer += Time.deltaTime;
            if(m_currentDeactivateTimer >= m_deactivateTimer)
            {
                if(IsOffScreen())
                {
                    gameObject.SetActive(false);
                }
                m_currentDeactivateTimer = 0;
            }
        }

        bool IsOffScreen()
        {
            if (!GetComponent<Renderer>().isVisible)
            {
                return true;
            }
            return false;
        }

        void Move() {
            transform.position += m_direction * m_speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            SpaceshipObject spaceshipObject = other.gameObject.GetComponent<SpaceshipObject>();
            if (spaceshipObject)
            {
                HandleSpaceshipCollision(spaceshipObject);
            }
            Bullet bulletObject = other.gameObject.GetComponent<Bullet>();
            if (bulletObject)
            {
                bulletObject.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        protected virtual void HandleSpaceshipCollision(SpaceshipObject spaceshipObject)
        {
            spaceshipObject.OnHit(m_damage);
            gameObject.SetActive(false);
        }

        public void SetDamage(float damage)
        {
            m_damage = damage;
        }
    }
}





































