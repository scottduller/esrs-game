using GameScripts.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public string weaponName;
        public float fireRate;
        public float damage;
        public float radius;
        public float projectileSpeed;
        public float lifeSpan;
        public GameObject bulletPrefab;
        public GameObject weaponPrefab;
    }
}
