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
        [SerializeField] private TextMeshProUGUI scoreList;
        [SerializeField] private TextMeshProUGUI playerScoreText;

        private void Awake()
        {
            LoadScores();
        }

        public void DisplayScore(int score) => playerScoreText.text = score.ToString();
        
        public void SaveScore()
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/score.sav";
            var stream = new FileStream(path, FileMode.Append);
            formatter.Serialize(stream, new Score("AAA", playerScore));
            stream.Close();
            
            LoadScores();
        }
        
        private void LoadScores()
        {
            var path = Application.persistentDataPath + "/scores.save";
            if (!File.Exists(path))
            {
                InitializeScores();
            }
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            var scores = formatter.Deserialize(stream) as List<Score>;
            stream.Close();

            if (scores == null)
            {
                Debug.LogWarning("Something fucked up when loading in scores, sorry dude.");
                return;
            }

            scoreList.text = "";
            var i = 0;
            foreach (var score in scores.OrderBy(t => t.score))
            {
                i++;
                if (i > 10) break;
                scoreList.text += score.initials[..3] + " - " + score.score + "\n";
            }
        }

        private static void InitializeScores()
        {
            var scores = new List<Score>();
            scores.Add(new Score("AJF", 000001));
            scores.Add(new Score("TMK", 000001));
            scores.Add(new Score("GUA", 000001));
            scores.Add(new Score("DYL", 000001));
            scores.Add(new Score("BRE", 000001));
            scores.Add(new Score("MRA", 000001));
            
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + "/score.sav";
            var stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, scores);
            stream.Close();
        }
        
    }
}