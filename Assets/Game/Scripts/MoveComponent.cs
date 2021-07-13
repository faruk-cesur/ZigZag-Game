using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float objectDistance = -40f;
    [SerializeField] private float despawnDistance = -110f;
    private bool canSpawnGround = true;
    private Rigidbody rb;
    public GameObject cube;
    
    private void Start()
    {
        cube.transform.localRotation = Quaternion.Euler(0,ObjectSpawner.instance.privateInt,0);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position += -transform.forward * speed * Time.deltaTime;
        if (transform.position.z <= objectDistance && transform.tag == "Ground" && canSpawnGround)
        {
            canSpawnGround = false;
        }
        ObjectSpawner.instance.SpawnGround();


        if (transform.position.z <= despawnDistance)
        {
            canSpawnGround = true;
            gameObject.SetActive(false);
        }
    }
}
