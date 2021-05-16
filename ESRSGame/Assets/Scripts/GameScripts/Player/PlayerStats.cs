using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Player
{
    public  class PlayerStats : MonoBehaviour
    {

        public int startHealth = 3;
        public float iFrames = 10f;
        public WeaponSO startingGun;
        [HideInInspector]
        public  int health;
        public  float playerSpeed;

        
        public HealthDisplay healthBar;
        
        [HideInInspector]private GameObject gameOver;
        [HideInInspector]public WeaponSO currentWeaponSo;

        // Start is called before the first frame update

        private float timeRemaining;

        private bool beenHit;
        void Awake ()
        {
            health = startHealth;
            beenHit = false;
          
        }


        private void Start()
        {  
            if (currentWeaponSo == null)
            { 
                healthBar.setHealth(health);
                
                currentWeaponSo = startingGun;


                Instantiate(startingGun.weaponPrefab, transform.Find("GunSpawn").transform);
                GameEventManager.OnWeaponPickup?.Invoke( this,new GameEventManager.OnWeaponPickUpEventArgs(currentWeaponSo));

            }
            // healthBar = GameObject.Find("HealthBarPlayer");
            // gameOver = GameObject.Find("GameManager");

        }
        // Update is called once per frame
        public void TakeDamage(int damage = 1)
        {
            if (!beenHit)
            {
                health -= damage;
                healthBar.UpdateDisplay(health);
                if (health <= 0)
                {
                    GameEventManager.OnGameOver.Invoke(this,EventArgs.Empty);
                }
                beenHit = true;
                timeRemaining = iFrames;
            }

        }

        public void Update()
        {
            if (beenHit)
            {
            
                if(timeRemaining <= 0)
                {
                    beenHit = false;
                }

                timeRemaining -= Time.deltaTime;
            }
        }

        private void UpdateWeapon(WeaponSO weapon)
        {
            Destroy(this.currentWeaponSo);
            GameEventManager.OnWeaponPickup?.Invoke( this,new GameEventManager.OnWeaponPickUpEventArgs(weapon));
            this.currentWeaponSo = weapon;
        }

        // public void PowerUpPickUp(GameObject item)
        // {
        //     if(item.tag == "Weapon")
        //     {
        //         Debug.Log("pickedUp");
        //         UpdateWeapon(item);
        //     }
        // }

    }
}
