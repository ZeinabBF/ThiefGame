using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 5f; // Adjust the bullet speed as needed
    private GameObject player;
    private PlayerController playerControllerScript;
    private Vector3 lookDir;
    private float topBound = 27f;
    private float rightBound = 23f;
    private float belowBound = 0f;
    //private float startDelay = 3f;
    private float spawnInterval = 5f;
    private Vector3 initialTargetPosition;
    private Vector3 initialBulletPosition;




    private void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        initialTargetPosition = player.transform.position;
        initialBulletPosition = gameObject.transform.position;
        lookDir = (initialTargetPosition - initialBulletPosition).normalized;
        lookDir *= bulletSpeed;
        WaitToShoot();
        //InvokeRepeating("Shoot", startDelay, spawnInterval);

    }

    private void Update()
    {
        Shoot();
        BulletBoundry();
        if (playerControllerScript.gameOver)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        // Make the bullet follow the player's position
        //Vector3 offset = new Vector3(0, poseY, 0);
        //  lookDir += offset;
        transform.Translate(lookDir * Time.deltaTime);



    }

    void BulletBoundry()
    {
        // Set the boundary for the bullet
        if (transform.position.z > topBound || transform.position.x > rightBound || transform.position.y < belowBound)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(spawnInterval);
    }
}
