using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    int health = 100;

    public void TakeDamage(int damage)
    {
        if (health <= 0)
        {
            GetComponent<PlaneController>().Die();
            Destroy(this.gameObject);
            return;
        }
        health -= damage;
    }
}
