﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //THE DOOM - Domination OffTerrain OnTheGo Maquinary, AKA Heavy Dog

    public float _normalSpeed = 5f;
    public new AudioSource audio;
    public AudioClip shootSound, explodeSound;
    public GameObject bullet;
    public GameObject bulletExit;
    public int playerArmor = 100;
    public bool shieldActive = false;
    public bool turboActive = false;
    public bool tripleShootActive = false;

    GameLogicManager manager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TankMovement();
        Shoot();
        DestroyTank();
    }

    void GetPowerUps()
    {

    }

    void TankMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //move the tank
        if (Input.GetKey(KeyCode.RightControl))
        {
            transform.Translate(transform.right * horizontal * Time.deltaTime * _normalSpeed);
            transform.Translate(transform.forward * vertical * Time.deltaTime * _normalSpeed);
        }

        Vector3 newPos = new Vector3(horizontal, 0.0f, vertical);
        transform.LookAt(newPos + transform.position);
        transform.Translate(newPos * _normalSpeed * Time.deltaTime, Space.World);
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShootCooldown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            playerArmor-= 10;
            Destroy(collision.gameObject);
        }

        if (collision.transform.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }

        if (collision.transform.tag == "Powerup")
        {
            Debug.Log(collision.transform.name);
            Destroy(collision.gameObject);
        }
    }

    public int ReturnArmorValue()
    {
        return playerArmor;
    }

    public void DestroyTank()
    {
        if(playerArmor == 0)
        {
            audio.PlayOneShot(explodeSound);
            Destroy(this.gameObject);
        }
    }

    IEnumerator ShootCooldown()
    {
        audio.PlayOneShot(shootSound, 0.45f);
        Instantiate(bullet, bulletExit.transform.position, bulletExit.transform.rotation);
        yield return new WaitForSeconds(5f * Time.deltaTime);
    }
}
