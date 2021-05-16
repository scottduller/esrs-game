using System;
using GameScripts.Player;
using SOScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScripts
{
    public class ChestScript : MonoBehaviour
    {
        public WeaponListSO weaponsDB;
        private WeaponSO _storedWeapon;
        private Transform _displayPoint;
        private GameObject _displayObject;
        private void Awake()
        {
            if (SceneManager.GetActiveScene().name.Equals("LevelBuilder"))
            {
                Destroy(this);
                return;
            }
            else
            {
                _displayPoint = transform.Find("DisplayPoint");
                _storedWeapon = weaponsDB.GETRandomWeapon();
                _displayObject = (GameObject) Instantiate(_storedWeapon.weaponPrefab, _displayPoint);
                // _displayObject.layer ;
                // _displayObject.transform.localScale *= 0.5f;
                
            }
        }

        public void FixedUpdate()
        {
            if (_displayObject)
            {
                _displayObject.transform.rotation =  
                    Quaternion.Euler(_displayObject.transform.rotation.eulerAngles.x,
                        _displayObject.transform.rotation.eulerAngles.y +10,_displayObject.transform.rotation.eulerAngles.z);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                other.transform.GetComponent<PlayerStats>().UpdateWeapon(_storedWeapon);
                Destroy(_displayObject);
                Destroy(this);
            }
        }
    }
}
