using UnityEngine;

public class DeathColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayerController thePlayer;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            thePlayer.KillPlayer();
        }
    }
}
