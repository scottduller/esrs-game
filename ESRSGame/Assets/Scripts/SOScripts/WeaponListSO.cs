using System.Collections.Generic;
using GameScripts;
using UnityEngine;

namespace SOScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WeaponListSO")]
    public class WeaponListSO : ScriptableObject
    {

        [SerializeField]
        public List<WeaponSO> WeaponSos;


        public  WeaponSO GETRandomWeapon()
        {
            int r = Mathf.FloorToInt(Random.Range(0, WeaponSos.Count));
            return  WeaponSos[r];
        }
    }
}
