using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PointManager : MonoBehaviour
{
	public static PointManager Instance;

    [SerializeField]
    private Text scoreHudText;
    [SerializeField]
    private Text bestScoreText;
    [SerializeField]
    private Text lastScoreText;
    [SerializeField]
    private Text coinText;
    [SerializeField]
    private Text coinsCollectedText;
    [SerializeField]
    private GameObject player;

    public int score;
    private int bestScore;
    private int coins;
    private int levelCoinCount;

    private String filename;
    private int platformCounter; 
    private int numberOfGamesPlayed;

	public int Coins { get { return coins; } set { coins = value; } }
    public int PlatformCounter { get { return platformCounter; } set { platformCounter = value; } }
    public int NumberOfGamesPlayed { get { return numberOfGamesPlayed; } }

	void Awake()
	{
		if (Instance == null)
			Instance = this;

		else if (Instance != this)
			Destroy(this.gameObject);
	}

    void Start()
    {
        filename = Application.persistentDataPath + "/" + gameObject.name + ".points";
        Load();

        coinText.text = coins + "x";
    }

    void Update()
    {
        coinText.text = coins + "x";

        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = "BEST SCORE: " + bestScore;
        }

        if (platformCounter == 3)
        {
            CountPoints(0, 1);
            levelCoinCount++;
            platformCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CountPoints(0, 200);
            Save();
        }
    }

    public void CountPoints(int scoreToAdd, int coinsToAdd)
    {
        score += scoreToAdd;
        scoreHudText.text = score.ToString();

        coins += coinsToAdd;
        coinText.text = coins + "x";
        Save();
    }

    public void AdCoinDouble()
    {
        coinsCollectedText.text = "+" + (levelCoinCount * 2);
        coins += levelCoinCount;
    }

    public void ResetPoints()
    {
        score = 0;
        scoreHudText.text = score.ToString();

        levelCoinCount = 0;
        platformCounter = 0;
    }

    public void DeathMenuPoints()
    {
        bestScoreText.text = "BEST SCORE: " + bestScore;
        lastScoreText.text = "LAST SCORE: " + score;
        coinsCollectedText.text = "+" + levelCoinCount;
    }

    public void GameCount()
    {
        numberOfGamesPlayed++;
        Save();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filename);
        PointManagerData data = new PointManagerData();
        data.bestScore = bestScore;
        data.coins = coins;
        data.numberOfGamesPlayed = numberOfGamesPlayed;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);
            PointManagerData data = (PointManagerData)bf.Deserialize(file);
            bestScore = data.bestScore;
            coins = data.coins;
            numberOfGamesPlayed = data.numberOfGamesPlayed;

            file.Close();
        }
    }
}

[Serializable]
class PointManagerData
{
    public int coins;
    public int bestScore;
    public int numberOfGamesPlayed;
}

