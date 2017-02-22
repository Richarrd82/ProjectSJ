using UnityEngine;

public class SpectrumCheck : MonoBehaviour
{
    [SerializeField]
    MySpectrum mySpec;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            mySpec.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("EnablingSpecs", 0.35f);
        }
    }

    //Invoke 
    void EnablingSpecs()
    {
        mySpec.enabled = true;
    }
}
