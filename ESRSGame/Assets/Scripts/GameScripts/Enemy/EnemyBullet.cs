using GameScripts.Player;
using UnityEngine;

namespace GameScripts.Enemy
{
    public class EnemyBullet : MonoBehaviour
    {
        private float radius;
        private float projectileSpeed;
        private Material material;
        private float _lifetime;
        private float _lifespan = 10;

        Vector3 direction;

        public GameObject ImpactEffect;



        public void SetStats(Vector3 direction, float radius, float projectileSpeed, Material material)
        {
            this.direction = direction;
            this.radius = radius;
            this.projectileSpeed = projectileSpeed;
            this.material = material;
            GetComponent<Renderer>().material = this.material;
            _lifetime = _lifespan;
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifespan <= 0)
            {
                _lifetime = _lifespan;
                playEffect();
            }
        }
    
        private void FixedUpdate()
        {
            Vector3 velocity = direction * Time.deltaTime;

            GetComponent<Rigidbody>().velocity = velocity.normalized * projectileSpeed;
        }


        private void OnCollisionEnter(Collision collision)
        {

            if (collision.collider.CompareTag("Bullet"))
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider, true);
            }

            else if (collision.collider.CompareTag("Player"))
            {
                collision.collider.transform.GetComponent<PlayerStats>().TakeDamage();
                playEffect();
            }
            else
            {
                playEffect();
            }
        }


        void playEffect(){
            GameObject effectObject = (GameObject)Instantiate(ImpactEffect, transform.position, Quaternion.Euler(0f,(int)direction.magnitude*-1f, 0f));
            effectObject.GetComponent<Renderer>().material = material;
            Destroy(effectObject, 2f);
            Destroy(gameObject);
            
        }
    }
}

