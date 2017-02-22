using UnityEngine;

public class TopCheck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PointManager.Instance.CountPoints(1, 0);
        PointManager.Instance.PlatformCounter += 1;
    }
}
