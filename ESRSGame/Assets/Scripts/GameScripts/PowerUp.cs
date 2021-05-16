using GameScripts.Player;
using UnityEngine;

namespace GameScripts
{
    public class PowerUp : MonoBehaviour
    {

        public GameObject powerUp;

        private void OnTriggerEnter(Collider other)
        {

            if(other.gameObject.CompareTag("Player"))
            {

                // other.gameObject.GetComponent<PlayerStats>().PowerUpPickUp(powerUp);
                Destroy(gameObject);
            }
        }





    }
}
