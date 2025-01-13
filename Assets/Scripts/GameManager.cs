using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreLabel;
    public Text ammoText;

    public GameObject[] enemySpawnPos;
    public GameObject[] shipLives;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public GameObject gameOverPanel;
    public GameObject bulletRefillPrefab;
    public GameObject[] bulletSpawnPos;

    public int playerLives = 3;
    private int lifeCount = 0;
    private int scoreCount = 0;

    public float enemySpawnTime = 2.0f;
    private float counter = 0.0f;

    public float bulletSpawnTime = 10.0f;
    private float bulletTimeCounter = 0.0f;

    private bool isGameOver = false;

    public static GameManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GameObject.Instantiate(playerPrefab, new Vector2(0, -3.57f), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            counter += Time.deltaTime;
            if (counter >= enemySpawnTime)
            {
                int rndPoint = Random.Range(0, 2);
                GameObject.Instantiate(enemyPrefab, enemySpawnPos[rndPoint].transform.position, Quaternion.identity);
                counter = 0;
            }

            bulletTimeCounter += Time.deltaTime;
            if (bulletTimeCounter >= bulletSpawnTime) {
                int bulletRndPoint = Random.Range(0, 2);
                GameObject.Instantiate(bulletRefillPrefab, bulletSpawnPos[bulletRndPoint].transform.position, Quaternion.identity);
                bulletTimeCounter = 0;
                bulletSpawnTime = Random.Range(10, 20);
            }
            
        }
        
    }

    public void playerDied()
    {

        shipLives[lifeCount].SetActive(false);
        lifeCount++;
        StartCoroutine(SpawnShip());
    }

    IEnumerator SpawnShip()
    {
        yield return new WaitForSeconds(2.5f);
        if (lifeCount < playerLives)
        {
            GameObject.Instantiate(playerPrefab, new Vector2(0, -3.57f), Quaternion.identity);
        }
        else
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
        }
    }

    public void addPoint()
    {
        scoreCount++;
        scoreLabel.text = "Score: "+scoreCount;
    }

    /// <summary>
    /// Connects to BUTTON in Game Scene
    /// </summary>
    public void _restartGame()
    {
        isGameOver = false;
        scoreCount = 0;
        lifeCount = 3;
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }
}
