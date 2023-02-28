using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneKeyboard : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPosition;
    [SerializeField] float bulletShootDelay = 0.4f;
    float currentBulletShootDelay = 0;

    bool canShoot = false;

    [SerializeField] bool isPlayerOne = false;

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

        if(isPlayerOne)
        {
            HandlePlayerOneMovementInput();
        }
        else
        {
            HandlePlayerTwoMovementInput();
        }


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

    private void HandlePlayerTwoMovementInput()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += new Vector3(0f, 0f, rotateSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles -= new Vector3(0f, 0f, rotateSpeed);
        }
        if (Input.GetMouseButtonDown(0))
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
        if (isPlayerOne)
        {
        bullet.tag = "bulletPlayer1";

        }
        else
        {
        bullet.tag = "bulletPlayer2";

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "leftWall")
        {
            transform.eulerAngles -= new Vector3(0f, 0f, 180);
        }
        if (other.tag == "rightWall")
        {
            transform.eulerAngles += new Vector3(0f, 0f, 180);
        }
        if(other.tag == "ground")
        {
            Die();
            Destroy(this.gameObject);
        }
    }

    public void Die()
    {
        if (isPlayerOne)
        {
            GameManager.instance.ShowWinAccrossNetwork(false);
        }
        else
        {
            GameManager.instance.ShowWinAccrossNetwork(true);
        }

    }
}
