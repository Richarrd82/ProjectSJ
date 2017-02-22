using UnityEngine;

public class SongScoreManager : MonoBehaviour {

	private AudioSource audioSource;
	private PlayerController playerController;

	public GameObject[] buttons;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	void Update ()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			if (audioSource.clip.name == buttons[i].GetComponent<SongsButton>().song.name)
			{
				//Debug.Log("Clips have same names.");

				if (playerController.GameStarted == true)
				{
					SongsButton songsButton = buttons[i].GetComponent<SongsButton>();

					if(PointManager.Instance.score > songsButton.songHighScore)
					{
						songsButton.songHighScore = PointManager.Instance.score;
						songsButton.songScoreText.text = songsButton.songHighScore.ToString();
						PlayerPrefs.SetInt("SongHighScore" + "/" + buttons[i].name, songsButton.songHighScore);
					}
				}
			}
		}
	}
}
