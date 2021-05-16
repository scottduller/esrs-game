using System;
using UnityEngine;

namespace GameScripts
{
    
    
    public class GameEndScript : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = transform.GetComponent<Collider>();
        }




        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                GameEventManager.OnLevelComplete(this,EventArgs.Empty);
            }
        }
    }
    
}
