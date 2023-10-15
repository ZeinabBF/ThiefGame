using UnityEngine;

public class Gold : MonoBehaviour
{
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver)
        {
            Destroy(gameObject);
        }
    }
}
