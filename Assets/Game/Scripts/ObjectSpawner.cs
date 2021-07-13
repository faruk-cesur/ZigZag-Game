using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private bool spawningObject = false;
    [SerializeField] private float groundSpawnDistance = 50f;
    public int privateInt = 45;

    public static ObjectSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnGround()
    {
        ObjectPooler.instance.SpawnFromPool("ground", new Vector3(0, 0, groundSpawnDistance),Quaternion.identity);
        privateInt *= -1;
    }
}
