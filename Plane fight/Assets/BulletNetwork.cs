using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletNetwork : NetworkBehaviour
{
    float bulletSpeed = 30f;
    float bulletDestroyTime = 4f;
    int bulletDamage = 10;
    Vector3 bulletDirection;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, bulletDestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right* bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the bullet is hitting the enemy
        if(other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
        if(other.tag == "leftWall" || other.tag == "rightWall" || other.tag == "ground")
        {
            Destroy(this.gameObject);
        }
    }
}
