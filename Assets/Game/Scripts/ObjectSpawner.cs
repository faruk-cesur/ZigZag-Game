using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public PlayerController player;
    public GameObject starterGrounds;
    private Vector3 startDirection;
    public GameObject diamond;

    private void Start()
    {
        startDirection.z += 6;
        Instantiate(starterGrounds, starterGrounds.transform.position, starterGrounds.transform.rotation,
            transform.parent);

        for (int i = 0; i < 15; i++)
        {
            Vector3 _direction;
            if (Random.Range(0, 2) == 0)
            {
                _direction = new Vector3(-2, 0, 0);
            }
            else
            {
                _direction = new Vector3(0, 0, 2);
            }

            startDirection += _direction;
            starterGrounds = Instantiate(starterGrounds, starterGrounds.transform.position + _direction,
                starterGrounds.transform.rotation, transform.parent);
        }
    }

    public void SpawnGround()
    {
        Vector3 _direction;
        if (Random.Range(0, 2) == 0)
        {
            _direction = new Vector3(-2, 0, 0);
            diamond = Instantiate(diamond, startDirection+_direction + new Vector3(0, 5.6f, 0), diamond.transform.rotation);
        }
        else
        {
            _direction = new Vector3(0, 0, 2);
        }

        ObjectPooler.instance.SpawnFromPool("ground", startDirection + _direction, transform.rotation);
        startDirection += _direction;
    }
}