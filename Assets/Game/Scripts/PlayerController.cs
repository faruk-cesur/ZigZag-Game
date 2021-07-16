using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.UIElements;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public static bool isFall;
    private bool falling;
    public bool isStarted;
    private Rigidbody rb;
    private Vector3 direction;
    private float speed = 300f;
    private float speedRise = 1f;
    private int scorePoint;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isFall = false;
        scorePoint = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.CurrentGameState == GameManager.GameState.MainGame)
        {
            rb.velocity = direction * speed * Time.deltaTime;
        }

        if (GameManager.instance.CurrentGameState == GameManager.GameState.MainGame && falling == true)
        {
            rb.velocity = new Vector3(-2, -2, 1) * speed * Time.deltaTime;
        }
    }

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
            speed += speedRise;
            scorePoint++;
            GameManager.instance.UpdateScore();
            AudioSource.PlayClipAtPoint(GameManager.instance.moveSound,transform.position);
            if (scorePoint % 20 == 0)
            {
                GameManager.instance.material.color = Random.ColorHSV().gamma;
                AudioSource.PlayClipAtPoint(GameManager.instance.multipleScoreSound,transform.position);
            }
        }
    }

    public void DeathState()
    {
        if (transform.position.y <= -0.2f)
        {
            isFall = true;
        }

        if (isFall == true)
        {
            GameManager.instance.CurrentGameState = GameManager.GameState.GameOver;
            return;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ObjectSpawner.instance.SpawnGround();
            StartCoroutine(KinematicOff(other.gameObject));
        }
    }

    IEnumerator KinematicOff(GameObject kinematic)
    {
        yield return new WaitForSeconds(0.25f);
        kinematic.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CollectDiamond collectDiamond = other.GetComponent<CollectDiamond>();

        if (collectDiamond)
        {
            GameManager.instance.CollectDiamond();
            AudioSource.PlayClipAtPoint(GameManager.instance.diamondSound, transform.position,2f);
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