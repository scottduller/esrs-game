using GameScripts.Player;
using UnityEngine;

namespace GameScripts.Enemy
{
    public class EnemyFollowController : MonoBehaviour
    {
        [Header("Target")]
        private GameObject player;

        [HideInInspector]
        private float speed;

        private void Start()
        {
            player = GameObject.Find("Player");
            speed = gameObject.GetComponent<EnemyStats>().speed;

        }
        // Update is called once per frame
        private void Update()
        {
            Vector3 dir = player.transform.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.tag == "Player")
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
