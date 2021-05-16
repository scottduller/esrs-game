using GameScripts.Player;
using UnityEngine;

namespace GameScripts.Enemy
{
    public class EnemyOrbitController : MonoBehaviour
    {
        [Header("Target")]
        private GameObject player;

        [HideInInspector]
        public float speed;

        public float orbitDistanceMax;
        public float orbitDistanceMin;
        public float timeBetweenBullet;
        private float _currentTimeToBullet;

        public GameObject bullet;

        public Material material;

        void Start()
        {
            speed = gameObject.GetComponent<EnemyStats>().speed;
            player = GameObject.Find("Player");
            _currentTimeToBullet = timeBetweenBullet;
        }
        // Update is called once per frame
        void Update()
        {
            Vector3 dir = player.transform.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            if (dir.magnitude > orbitDistanceMax)
            {
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            }
            else if (dir.magnitude < orbitDistanceMin)
            {
                transform.Translate(-dir.normalized * distanceThisFrame, Space.World);
            }
            else
            {
                transform.RotateAround(player.transform.position, Vector3.up, distanceThisFrame/dir.magnitude * 100);
                transform.LookAt(player.transform.position);
                _currentTimeToBullet -= Time.deltaTime;
                if (_currentTimeToBullet <= 0)
                {        
                    _currentTimeToBullet = timeBetweenBullet;
                    GameObject newBullet = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);
                    newBullet.transform.LookAt(player.transform.position);
                    newBullet.GetComponent<EnemyBullet>().SetStats(transform.forward, 5, 30, material);
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                HitPlayer();
            }
        }

        private void HitPlayer()
        {
            player.GetComponent<PlayerStats>().TakeDamage();
            
        }


    }
}
