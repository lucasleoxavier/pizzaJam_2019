﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour
{


    public Text scoreUI;
    public int score = 0;

    public Text armorUI;

    public Image armor;

    public bool isGameOver = false, isPaused = false;

    public SpawnManager spawn;

    public PlayerMovement player;

    public Image tripleShootPU, turboPU, shieldPU, tripleBG, turboBG, shieldBG;

    public ParticleSystem explosion;

    float shieldTime = 0f, tripleTime = 0f, turboTime = 0f;
    float maxPowerUpTime = 15f;

    public GameObject pauseMenuUI, gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        spawn.StartSpawn(isGameOver);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateArmor();
        UpdateScore();
        PopulatePowerUp_UI();
        PauseMenu();
        GameOver();
    }

    public void AddScore()
    {
        score += 100;
    }

    void UpdateArmor()
    {
        armor.fillAmount = player.ReturnArmorValue() / 100;
        armorUI.text = player.ReturnArmorValue() + "%";
    }

    void UpdateScore()
    {
        scoreUI.text = score.ToString();
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isGameOver)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            isPaused = true;
        }
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.B) && isPaused)
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            isPaused = false;
        }
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.M) && isPaused)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void MakeExplode(Transform pos)
    {
        Instantiate(explosion, pos.position, Quaternion.identity);
        explosion.Play();
    }

    public void GameOver()
    {
        if(isGameOver)
        {
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);

            if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.B) && isGameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.M) && isGameOver)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void PopulatePowerUp_UI()
    {
        if(player.shieldActive)
        {
            shieldTime += Time.deltaTime;
            float percent = shieldTime / maxPowerUpTime;
            shieldPU.fillAmount = Mathf.Lerp(1, 0, percent);
        }
        else
        {
            shieldTime = 0f;
        }

        if (player.tripleShootActive)
        {
            tripleTime += Time.deltaTime;
            float percent = tripleTime / maxPowerUpTime;
            tripleShootPU.fillAmount = Mathf.Lerp(1, 0, percent);
        }
        else
        {
            tripleTime = 0f;
        }

        if (player.turboActive)
        {
            turboTime += Time.deltaTime;
            float percent = turboTime / maxPowerUpTime;
            turboPU.fillAmount = Mathf.Lerp(1, 0, percent);
        }
        else
        {
            turboTime = 0f;
        }
    }

}
