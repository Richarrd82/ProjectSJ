using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private GameObject player;
    private Vector3 playerStartPos;
    private PlatformDestroyer[] platformList;

    [SerializeField]
    private Transform platformGenerator;
    private Vector3 platformStartPos;

    [SerializeField]
    private Transform frontPlatformGenerator;             
    private Vector3 frontplaformStartPos;

    private MySpectrum[] spectrums = new MySpectrum[9];

	//Out of here
    private string likeUrl = "https://www.facebook.com/Blizzard/?fref=ts";
    private string rateUrl = "https://play.google.com/store/apps/details?id=com.lima.doodlejump";

	void Awake()
	{
		if (Instance == null)
			Instance = this;

		else if (Instance != this)
			Destroy(this.gameObject);
	}

	void Start ()
    {
		player = GameObject.Find("Player");
        playerStartPos = player.transform.position;
        platformStartPos = platformGenerator.position;
        frontplaformStartPos = frontPlatformGenerator.position;
    }

    public void RestartGame ()
    {
        spectrums = FindObjectsOfType<MySpectrum>();
        foreach (MySpectrum spectrum in spectrums)
        {
            spectrum.enabled = true;
        }

        //platform and pool reset
        platformList = FindObjectsOfType<PlatformDestroyer>();

        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        platformGenerator.position = platformStartPos;
        frontPlatformGenerator.position = frontplaformStartPos;    

        player.transform.position = playerStartPos;
        player.gameObject.SetActive(true);

        PointManager.Instance.ResetPoints();
    }

	//Get these two out of here
    public void ShareBtnPressed()
    {
        Application.OpenURL(likeUrl);
    }

    public void RateBtnPressed()
    {
        Application.OpenURL(rateUrl);
    }
}

