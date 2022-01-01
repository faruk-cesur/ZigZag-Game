using System.Collections;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    // Variables Defined.
    public static bool isFall;
    private bool falling;
    public bool isStarted;
    private Rigidbody rb;
    private Vector3 direction;
    private float speed = 500f;
    private float speedRise = 1f;
    private int scorePoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isFall = false;
        scorePoint = 0;
    }

    // Player will move when the game starts and game state is changed to main game
    private void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameManager.GameState.MainGame)
        {
            rb.velocity = direction * speed * Time.deltaTime;
        }

        if (GameManager.instance.currentGameState == GameManager.GameState.MainGame && falling == true)
        {
            rb.velocity = new Vector3(-2, -2, 1) * speed * Time.deltaTime;
        }
    }

    // Controlling player movement and changing its direction by clicking the mouse button or tapping on mobile.
    public void BallMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (direction.x == 0)
            {
                direction = Vector3.left;
            }
            else
            {
                direction = Vector3.forward;
            }

            isStarted = true;
            speed += speedRise; // Each time we click, the player speeds up 
            scorePoint++; // The color of the cubes changes every 20 points (Score %20 == 0)
            GameManager.instance.UpdateScore(); // Each time we click, we gain a score
            SoundManager.Instance.PlaySound(SoundManager.Instance.moveSound, 0.5f);
            if (scorePoint % 20 == 0) //The color of the cubes changes every 20 points 
            {
                GameManager.instance.material.color = Random.ColorHSV().gamma;
                SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 0.5f);
            }
        }
    }

    // Camera will not follow the player when we fall
    public void DeathState()
    {
        if (transform.position.y <= -0.2f)
        {
            isFall = true;
        }

        if (isFall == true)
        {
            GameManager.instance.currentGameState = GameManager.GameState.GameOver;
            return;
        }
    }

    // Cubes are falling down when the player pass by
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ObjectSpawner.instance.SpawnGround();
            StartCoroutine(KinematicOff(other.gameObject));
        }
    }

    // Cubes are falling down after 0.25 seconds
    IEnumerator KinematicOff(GameObject kinematic)
    {
        yield return new WaitForSeconds(0.25f);
        kinematic.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }


    // Collecting diamonds and check if the player has touched the death area 
    private void OnTriggerEnter(Collider other)
    {
        CollectDiamond collectDiamond = other.GetComponent<CollectDiamond>();

        if (collectDiamond)
        {
            GameManager.instance.CollectDiamond();
            SoundManager.Instance.PlaySound(SoundManager.Instance.diamondSound, 0.1f);
            Instantiate(GameManager.instance.particleDiamond, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        DeathArea deathArea = other.GetComponent<DeathArea>();

        if (deathArea)
        {
            falling = true;
            GameManager.instance.DeathArea();
        }
    }
}