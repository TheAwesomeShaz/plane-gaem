using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlaneController : NetworkBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPosition;
    [SerializeField] float bulletShootDelay = 0.4f;
    float currentBulletShootDelay = 0;

    bool canShoot = false;

    //[SerializeField] bool isPlayerOne = false;

    // Start is called before the first frame update
    void Start()
    {
        canShoot= true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
        HandleShooting();
        HandlePlayerOneMovementInput();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }

    private void HandlePlayerOneMovementInput()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles += new Vector3(0f, 0f, rotateSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles -= new Vector3(0f, 0f, rotateSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootBullet();
        }
    }

    private void HandleShooting()
    {
        if (!canShoot)
        {
            currentBulletShootDelay -= Time.deltaTime;
        }
        if (currentBulletShootDelay < 0)
        {
            canShoot = true;
            currentBulletShootDelay = 0;
        }
    }

    private void shootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab,bulletSpawnPosition.position,bulletSpawnPosition.rotation);
        currentBulletShootDelay = bulletShootDelay;
        canShoot= false;

        //bullet.tag = "bulletPlayer1";

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "leftWall")
        {
            transform.eulerAngles -= new Vector3(0f, 0f, 90);
        }
        if (other.tag == "rightWall")
        {
            transform.eulerAngles += new Vector3(0f, 0f, 90);
        }
        if(other.tag == "ground")
        {
            Die();
            Destroy(this.gameObject);
        }
    }

    public void Die()
    {    
        GameManager.instance.ShowWin(false);   
    }
}
