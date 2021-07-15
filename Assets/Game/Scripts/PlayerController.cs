using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 150f;
    private float speedRise = 1f;
    public static bool isFall;
    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isFall = false;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.CurrentGameState == GameManager.GameState.MainGame)
        {
            rb.velocity = direction * speed * Time.deltaTime;
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

            speed += speedRise;
            GameManager.instance.UpdateScore();
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
            //AudioSource.PlayClipAtPoint(diamondSound, transform.position);
            Destroy(other.gameObject);
        }
        
        DeathArea deathArea = other.GetComponent<DeathArea>();
        
        if (deathArea)
        {
            GameManager.instance.DeathArea();
            //AudioSource.PlayClipAtPoint(diamondSound, transform.position);
        }
    }
}