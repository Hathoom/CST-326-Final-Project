using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using File = UnityEngine.Windows.File;

namespace SceneControllers
{
    public class ScoreManager : MonoBehaviour
    {
        public int playerScore;
        [SerializeField] private List<Initial> initials;
        [SerializeField] private TextMeshProUGUI scoreList;
        [SerializeField] private TextMeshProUGUI playerScoreText;

        private void Awake()
        {
            var path = Application.persistentDataPath + "/scores.sav";
            if (!File.Exists(path)) InitializeScores();
            LoadScores();
            playerScore = 123456;
            DisplayScore(playerScore);
        }
        private void DisplayScore(int score) => playerScoreText.text = score.ToString();
        
        public void SaveScore()
        {
            var playerName = "";
            foreach (var initial in initials)
            {
                playerName += initial.GetInitial();
            }
            
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/scores.sav";
            var stream = new FileStream(path, FileMode.Open);

            var list = formatter.Deserialize(stream) as List<Score>;
            stream.Close();
            if (list == null) return;
            list.Add(new Score(playerName, playerScore));
            
            stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, list);
            stream.Close();
            
            LoadScores();
        }
        
        private void LoadScores()
        {
            var path = Application.persistentDataPath + "/scores.sav";
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            var scores = formatter.Deserialize(stream) as List<Score>;
            stream.Close();
            
            if (scores == null) return;
            scoreList.text = "";
            var i = 0;
            foreach (var score in scores.OrderByDescending(t => t.score))
            {
                i++;
                if (i > 7) break;
                scoreList.text += score.initials[..3] + " - " + score.score.ToString("000000") + "\n";
            }
        }

        private static void InitializeScores()
        {
            var scores = new List<Score>();
            scores.Add(new Score("AJF", 13939));
            scores.Add(new Score("TMK", 5656));
            scores.Add(new Score("GVS", 9398));
            scores.Add(new Score("DCL", 12515));
            scores.Add(new Score("BAM", 11097));
            scores.Add(new Score("NAA", 10308));
            
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/scores.sav";
            var stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, scores);
            stream.Close();
        }
        
    }
}