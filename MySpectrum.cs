using UnityEngine;

public class MySpectrum : MonoBehaviour
{
    private int numberOfObjects;
    [SerializeField]
    private GameObject prefab;

    private float[] spectrum = new float[1024];

    void Start()
    {
        numberOfObjects = Random.Range(1,12);
    }

    void FixedUpdate()
    {
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 previousScale = prefab.transform.localScale;
            previousScale.y = spectrum[i] * 35;

            if (previousScale.y > 0.01)
            {
                prefab.transform.localScale = previousScale;
            }
        }
    }
}