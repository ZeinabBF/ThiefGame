using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject pollicePrefab;
    public GameObject bulletPrefab;
    public GameObject goldPrefab;
    private float startDelay = 3f;
    //private float spawnInterval = 5f;
    private GameObject player;
    private PlayerController playerControllerScript;
    private float polliceRangeX = 20;
    private float polliceSpawnPosY = 0;
    private float polliceRangeZ = 17;
    private float goldSpawnRangeX = 15;
    private float goldSpawnRangeY = 1;
    private float goldSpawnRangeZ = -12;
    private float bulletSpawnPosX = 1;
    private float bulletSpawnPosY = 7;
    public bool powerupIndicator = true;
    public int polliceWaveNum = 1;



    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        StartCoroutine(SpawnGoldWithIndicator());

    }
    void Update()
    {


    }

    public void PolliceSpawn()
    {
        Vector3 polliceSpawnPos = new Vector3(UnityEngine.Random.Range(-polliceRangeX, polliceRangeX), polliceSpawnPosY, UnityEngine.Random.Range(-polliceRangeZ, polliceRangeZ));
        Vector3 offset = new Vector3(bulletSpawnPosX, bulletSpawnPosY, 0);
        Vector3 bulletSpawnPos = polliceSpawnPos + offset;
        Instantiate(pollicePrefab, polliceSpawnPos, pollicePrefab.transform.rotation);
        BulletSpawn(bulletSpawnPos); // Pass polliceSpawnPos to BulletSpawn

    }

    void PolliceWave(int polliceToSpawn)
    {
        for (int i = 0; i < polliceToSpawn; i++)
        {
            Vector3 polliceSpawnPos = new Vector3(UnityEngine.Random.Range(-polliceRangeX, polliceRangeX), polliceSpawnPosY, UnityEngine.Random.Range(-polliceRangeZ, polliceRangeZ));
            Vector3 offset = new Vector3(bulletSpawnPosX, bulletSpawnPosY, 0);
            Vector3 bulletSpawnPos = polliceSpawnPos + offset;
            Instantiate(pollicePrefab, polliceSpawnPos, pollicePrefab.transform.rotation);
            BulletSpawn(bulletSpawnPos); // Pass polliceSpawnPos to BulletSpawn
        }
    }

    void BulletSpawn(Vector3 bulletSpawnPos) // Accept spawnPos as a parameter
    {
        StartCoroutine(SpawnBullets(bulletSpawnPos));
    }

    IEnumerator SpawnBullets(Vector3 bulletSpawnPos)
    {
        while (playerControllerScript.bulletIndicator)
        {
            Instantiate(bulletPrefab, bulletSpawnPos, transform.rotation);
            yield return new WaitForSeconds(startDelay); // Adjust the delay as needed
        }
    }
    IEnumerator SpawnGoldWithIndicator()
    {
        for (int i = 0; ; i++)
        {
            yield return new WaitUntil(() => powerupIndicator);

            if (playerControllerScript.gameOver == false && powerupIndicator == true)
            {
                powerupIndicator = false;
                GoldSpawn();
                PolliceWave(polliceWaveNum);
                StartCoroutine(WaitForVariableChange());
                polliceWaveNum++;
            }
        }
    }


    IEnumerator WaitForVariableChange()
    {
        yield return new WaitForSeconds(startDelay); // Wait for a specific duration before allowing the loop to continue
    }
    void GoldSpawn()
    {
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-goldSpawnRangeX, goldSpawnRangeX), goldSpawnRangeY, goldSpawnRangeZ);
        Instantiate(goldPrefab, spawnPos, goldPrefab.transform.rotation);
        playerControllerScript.bulletIndicator = true;
    }


}