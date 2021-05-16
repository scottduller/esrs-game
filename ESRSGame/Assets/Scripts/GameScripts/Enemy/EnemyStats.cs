using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        public EnemySO _enemySo;
        [HideInInspector]
        public float health;
        public GameObject deathEffect;

        public string enemyName;
        public float startingHealth;
        public float speed;
        private Image healthBar;
        private void Start()
        {
            healthBar = transform.Find("Canvas/HealthBar/Image").GetComponent<Image>();
            health = startingHealth;
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            healthBar.fillAmount = health / startingHealth;
            if(health <= 0)
            {
                Die();

            }
        }

        public void Die()
        {
            // GameObject effectObject = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            // Destroy(effectObject, 4f);
            Destroy(gameObject);
        }

    }
}
