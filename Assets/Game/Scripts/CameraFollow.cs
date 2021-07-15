using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followPlayer;
    private Vector3 cameraPosition;
    [Range(0.01f, 1.0f)]
    public float cameraSpeed = 0.5f;
    
    void Start()
    {
        cameraPosition = transform.position - followPlayer.position;
    }

    void LateUpdate()
    {
        if(PlayerController.isFall == false)
        {
            Vector3 newPos = followPlayer.position + cameraPosition;

            transform.position = Vector3.Slerp(transform.position, newPos, cameraSpeed);
        }
    }
}
