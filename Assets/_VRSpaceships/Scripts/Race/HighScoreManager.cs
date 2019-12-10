using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HighScoreManager : MonoBehaviour
{
    private string SAVED_GAMES_FILENAME = "savedRacesScores.gd";
    private Color GOLD_COLOR = new Color(0.8018868f, 0.7000817f, 0.0f, 1.0f);
    private Color SILVER_COLOR = new Color(0.6415094f, 0.6415094f, 0.6415094f, 1.0f);
    private Color BRONZE_COLOR = new Color(0.4339623f, 0.1846895f, 0.0f, 1.0f);

    [SerializeField] private GameObject graphic;

    [SerializeField]
    private TextMeshProUGUI _text;

    private List<PlayerScore> _scores;
    public void ShowScores(PlayerScore newScore)
    {
        LoadCurrentTopScores();
        if (AddNewScore(newScore)) SaveNewTopScores();
        graphic.SetActive(true);
        VisualiseScores();
    }

    private void VisualiseScores()
    {
        _text.text = "";
        foreach (var score in _scores)
        {
            _text.text += score.name.PadRight(14, ' ') + TimeConverter.ConvertTimeToString(score.time) + "\n";
        }
    }

    private bool AddNewScore(PlayerScore newScore)
    {
        if (_scores.Count < 10)
        {
            _scores.Add(newScore);
            _scores.Sort((a, b) => a.time.CompareTo(b.time));
            return true;
        }
        if (_scores[_scores.Count - 1].time > newScore.time)
        {
            _scores[_scores.Count - 1] = newScore;
            _scores.Sort((a, b) => a.time.CompareTo(b.time));
            return true;
        }

        return false;
    }


    private void SaveNewTopScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + SAVED_GAMES_FILENAME);
        bf.Serialize(file, _scores);
        file.Close();
    }

    private void LoadCurrentTopScores()
    {
        if (File.Exists(Application.persistentDataPath +"/"+ SceneManager.GetActiveScene().name+ SAVED_GAMES_FILENAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + SAVED_GAMES_FILENAME, FileMode.Open);
            _scores = (List<PlayerScore>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            _scores = new List<PlayerScore>();
        }
    }
}
[Serializable]
public class PlayerScore
{
    public PlayerScore()
    {

    }
    public PlayerScore(string name, float time)
    {
        this.name = name;
        this.time = time;
    }
    public string name;
    public float time;
}