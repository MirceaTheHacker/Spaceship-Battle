using Assets.Scripts.BulletScripts;
using UnityEngine;

namespace Assets.Scripts.SpaceshipScripts
{
    public abstract class SpaceshipObject : MonoBehaviour
    {
        public float m_speed;
        public float m_weaponCooldownTime = 0.35f;
        public float m_healthMax;
        private float m_healthCurrent;
        public float m_damage;

        [SerializeField]
        protected Transform m_firePoint;

        protected bool m_weaponIsOnCooldown;
        protected float m_currentFireCooldownTimer;

        protected System.Type m_bulletTtype;

        protected virtual void OnEnable()
        {
            m_healthCurrent = m_healthMax;
            m_currentFireCooldownTimer = m_weaponCooldownTime;
        }

        protected virtual void Start()
        {
            m_bulletTtype = typeof(Bullet);
        }

        protected virtual void Update()
        {
            if (GameManager.Instance.GetState() != GameManager.enGameState.Playing) return;
            UpdateCheckDeath();
            UpdateWeaponCooldown();
        }

        private void UpdateCheckDeath()
        {
            if (m_healthCurrent <= 0)
            {
                Death();
            }
        }

        protected virtual void Death()
        {
            gameObject.SetActive(false);
        }

        private void UpdateWeaponCooldown()
        {
            m_currentFireCooldownTimer += Time.deltaTime;
            if (m_currentFireCooldownTimer > m_weaponCooldownTime)
            {
                m_weaponIsOnCooldown = false;
            }
        }

        public void Fire()
        {
            if (m_bulletTtype == typeof(Bullet))
            {
                Debug.LogError("Bullet type not set!");
            }
            if (!m_weaponIsOnCooldown)
            {
                m_weaponIsOnCooldown = true;
                m_currentFireCooldownTimer = 0f;
                GameObject bulletPooled = ObjectPooler.Instance.GetPooledObject(m_bulletTtype);
                if (bulletPooled != null)
                {
                    bulletPooled.transform.position = m_firePoint.transform.position;
                    bulletPooled.transform.rotation = m_firePoint.transform.rotation;
                    bulletPooled.SetActive(true);

                    Bullet bullet = bulletPooled.GetComponent<Bullet>();
                    if(bullet)
                    {
                        bullet.SetDamage(m_damage);
                    }
                    PlayFireSFX();
                }
            }
        }

        protected virtual void PlayFireSFX()
        {
            
        }

        public virtual void Move(Vector2 direction)
        {
            transform.Translate(direction * m_speed * Time.deltaTime);
        }

        public virtual void OnHit(float damage)
        {
            m_healthCurrent -= damage;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            SpaceshipObject spaceshipObject = other.gameObject.GetComponent<SpaceshipObject>();
            if (spaceshipObject)
            {
                HandleSpaceshipCollision(spaceshipObject);
            }
        }

        protected virtual void HandleSpaceshipCollision(SpaceshipObject spaceshipObject)
        {
            spaceshipObject.OnHit(m_damage);
            Death();
        }
        
    }
}
