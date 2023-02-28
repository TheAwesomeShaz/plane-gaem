using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] Transform playerTransform;
    [SerializeField] float bulletShootDelay;

    Vector3 currPlayerPos;
    float speed = 10f;
    float rotationSpeed = 300f;
    float currentBulletShootDelay = 10f;
    bool canShoot;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<AirPlaneKeyboard>().transform;
        UpdatePlayerTransform();
        StartCoroutine(WaitAndShoot(bulletShootDelay));
    }

    private void UpdatePlayerTransform()
    {
        if(playerTransform) {
       currPlayerPos = playerTransform.position;        
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currPlayerPos, speed * Time.deltaTime);
        var moveDir = transform.position - currPlayerPos;
        var lookRotation = Quaternion.LookRotation(moveDir,Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if(transform.position == currPlayerPos)
        {
            UpdatePlayerTransform();
        }
    }

    IEnumerator WaitAndShoot(float shootDelay)
    {
        yield return new WaitForSeconds(shootDelay);
        shootBullet();
    }

    private void shootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
        currentBulletShootDelay = bulletShootDelay;
        canShoot = false;
        
        StartCoroutine(WaitAndShoot(bulletShootDelay));
    }
}
