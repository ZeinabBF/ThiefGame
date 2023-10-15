using UnityEngine;

public class Pollice : MonoBehaviour
{
    public GameObject bulletPrefab;
    //private float startDelay = 3f;
    //private float spawnInterval = 5f;
    private GameObject player;
    private PlayerController playerControllerScript;
    // private float bulletSpawnPosX = 1;
    //private float bulletSpawnPosY = 7;
    //public Transform target;

    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        // Look at the player
        Vector3 lookTarget = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        transform.LookAt(lookTarget);

        if (playerControllerScript.gameOver || !playerControllerScript.polliceIndicator)
        {
            Destroy(gameObject);
            playerControllerScript.polliceIndicator = true;
        }
    }



}
