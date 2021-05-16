using System;
using UnityEngine;

namespace GameScripts
{
    public static class GameEventManager 
    {

        public static EventHandler<EventArgs> OnPlayerHit;
        public static EventHandler<OnWeaponPickUpEventArgs> OnWeaponPickup;
        public static EventHandler<EventArgs> OnLevelComplete;
        public static EventHandler<EventArgs> OnGameOver;
        // public static EventHandler<EventArgs> 
        public static EventHandler<OnUIWindowChangeEventArgs> onUIWindowChanged;

        public class OnUIWindowChangeEventArgs : EventArgs
        {
            public OnUIWindowChangeEventArgs(bool value)
            {
                isWindowOpen = value;
            } 
        
            public readonly bool isWindowOpen;
        }

    
    
        public class OnWeaponPickUpEventArgs : EventArgs
        {
            public OnWeaponPickUpEventArgs(WeaponSO value)
            {
                Debug.Log(value);
                weaponSo = value;
            } 
        
            public readonly WeaponSO weaponSo;
        }

    
    }
}
