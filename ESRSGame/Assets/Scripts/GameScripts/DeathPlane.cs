using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Player;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag.Equals("Player"))
        {
            other.transform.GetComponent<PlayerStats>().TakeDamage(100000000);
        }
    }
}
