//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using UnityEngine;



//public class HighScoreManager : MonoBehaviour
//{
//    private Color GOLD_COLOR = new Color(0.8018868f, 0.7000817f,0.0f,1.0f);
//    private Color SILVER_COLOR = new Color(0.6415094f, 0.6415094f, 0.6415094f,1.0f);
//    private Color BRONZE_COLOR = new Color(0.4339623f, 0.1846895f, 0.0f, 1.0f);



//    private List<PlayerScore> _scores;
//    public void ShowScores(PlayerScore newScore)
//    {
//        LoadCurrentTopScores();
//        if(AddNewScore(newScore))SaveNewTopScores();
        
//    }

//    private bool AddNewScore(PlayerScore newScore)
//    {
//        if( _scores[_scores.Count - 1].time > newScore.time)
//        {
//            _scores[_scores.Count - 1] = newScore;
//            _scores.Sort((a,b)=>a.time.CompareTo(b.time));
//            return true;
//        }

//        return false;
//    }


//    private void SaveNewTopScores()
//    {
//        BinaryFormatter bf = new BinaryFormatter();
//        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
//        bf.Serialize(file, _scores);
//        file.Close();
//    }

//    private void LoadCurrentTopScores()
//    {
//        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
//        {
//            BinaryFormatter bf = new BinaryFormatter();
//            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
//            _scores = (List<PlayerScore>) bf.Deserialize(file);
//            file.Close();
//        }
//    }
//}
//[Serializable]
//public class PlayerScore
//{
//    public string name;
//    public float time;
//}



