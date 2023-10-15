using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float jumpForce = 1000f;
    private float gravityModifier = 5f;
    private float movementSpeed = 10f;
    private float turnSpeed = 200f;
    private float zRange = 23f;
    private float xRange = 20f;
    public bool gameOver;
    private bool isOnGround = true;
    public GameObject powerupPrefab;
    public GameObject enemyPrefab;
    private GameManager gameManager;
    public SpawnManager spawnManager;
    public bool powerupIndicator;
    public bool bulletIndicator = true;
    public bool polliceIndicator = true;
    private float interval = 4;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public AudioClip goldSound;
    public AudioClip bulletSound;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Allow the player to jump
        inbounds();
        if (!gameOver)
        {
            move();
            jump();
        }
    }

    void inbounds()
    {
        //Keep the player inbounds
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }
    void move()
    {
        //Allow the player to move in all directions
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.back * horizontalInput * movementSpeed * Time.deltaTime);
        float verticalInput = Input.GetAxis("Vertical");
        //Allow the player to rotate
        transform.Rotate(Vector3.up, verticalInput * turnSpeed * Time.deltaTime);
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            bulletIndicator = false;
            Destroy(other.gameObject);
            StartCoroutine(NextLevelCoroutine());
            polliceIndicator = false;
            playerAudio.PlayOneShot(goldSound, 1.0f);
        }
        else if (other.CompareTag("Enemy"))
        {
            gameOver = true;
            gameManager.GameOver();
            explosionParticle.Play();
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(bulletSound, 1.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
        else if (other.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    IEnumerator NextLevelCoroutine()
    {
        yield return new WaitForSeconds(interval);
        spawnManager.powerupIndicator = true;

    }
}


