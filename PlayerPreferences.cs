using UnityEngine;
using UnityEngine.UI;

public class PlayerPreferences : MonoBehaviour
{
	[SerializeField]
	private Text songNameText;
	[SerializeField]
	private AudioClip[] songs;
	[SerializeField]
	private AnimatorOverrideController[] skins;

	private Animator playerAnimator;
	public int currentSkin;

	private AudioSource audioSource;
	public int currentSong;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		playerAnimator = GameObject.Find("Player").GetComponent<Animator>();

		if(PlayerPrefs.HasKey("CurrentSkin"))
		{
			currentSkin = PlayerPrefs.GetInt("CurrentSkin");
			playerAnimator.runtimeAnimatorController = skins[currentSkin];
		}

		if (PlayerPrefs.HasKey("CurrentSong"))
		{
			currentSong = PlayerPrefs.GetInt("CurrentSong");
			audioSource.clip = songs[currentSong];
			audioSource.Play();
			songNameText.text = songs[currentSong].name;
		}

		else if (!PlayerPrefs.HasKey("CurrentSong"))
		{
			audioSource.clip = songs[0];
			audioSource.Play();
		}
	}

	public void SelectSkin(int skinNumber)
	{
		PlayerPrefs.SetInt("CurrentSkin", skinNumber);
	}

	public void SelectSong(int songNumber)
	{
		PlayerPrefs.SetInt("CurrentSong", songNumber);
	}
}

