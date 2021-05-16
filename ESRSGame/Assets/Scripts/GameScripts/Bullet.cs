using System;
using GameScripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {

        private float _fireRate;
        private float _damage;
        private float _radius;
        private float _projectileSpeed;
        private float _color;
        private Vector3 _direction;
        private float _lifespan;
        private float _lifetime;
        private Vector3 _velocity;
        
        public GameObject impactEffect;

        public void GetGunStats(Vector3 _direction, float _damage, float _radius, float _projectileSpeed, float _lifespan)
        {
            this._direction = _direction;
            this._damage = _damage;
            this._radius = _radius;
            this._lifespan = _lifespan;
            this._projectileSpeed = _projectileSpeed;
            transform.localScale =new Vector3 (this._radius,this._radius,this._radius);
            _lifetime = _lifespan;

        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifespan <= 0)
            {
                _lifetime = _lifespan;
                PlayEffect();
            }
        }

        private void FixedUpdate()
        {
            Vector3 velocity = Vector3.zero;
            velocity += _direction;
            _velocity = velocity;
            GetComponent<Rigidbody>().velocity = velocity.normalized * _projectileSpeed;
        }


        private void OnCollisionEnter(Collision collision)
        {
        
            if (collision.collider.CompareTag("Bullet"))
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider, true);
            }
            else if (collision.collider.CompareTag($"Enemy"))
            {
                collision.collider.GetComponent<EnemyStats>().TakeDamage(_damage);
                PlayEffect();
            }
            else
            {
                PlayEffect();
            }


           
        }
        void PlayEffect()
        {
            float angle = Vector3.Angle(_velocity, new Vector3(0,0,-1));
            GameObject effectObject = (GameObject)Instantiate(impactEffect,
                transform.position, Quaternion.Euler(0f,angle, 0f));
            Destroy(effectObject, 2f);
            Destroy(gameObject);
            
        }
    }
}
