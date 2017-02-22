using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;                         
    private Vector3 lastPlayerPosition;                     
    private float distanceToMove;

	void Start ()
    {
        lastPlayerPosition = player.transform.position;     
	}
	
	void FixedUpdate ()
    {
        distanceToMove = player.transform.position.x - lastPlayerPosition.x;  
        lastPlayerPosition = player.transform.position;                       
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
	}
}
