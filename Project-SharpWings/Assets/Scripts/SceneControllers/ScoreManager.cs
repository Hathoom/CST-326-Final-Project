using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using File = UnityEngine.Windows.File;

namespace SceneControllers
{
    public class ScoreManager : MonoBehaviour
    {
        public int playerScore;
        public bool deathState;
        public AudioSource AS;
        [SerializeField] private List<Initial> initials;
        [SerializeField] private TextMeshProUGUI scoreList;
        [SerializeField] private TextMeshProUGUI playerScoreText;
        [SerializeField] private Button submitButton;
        

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void Initialize()
        {
            var path = Application.persistentDataPath + "/scores.sav";
            if (!File.Exists(path)) InitializeScores();
            LoadScores();
            DisplayScore(playerScore);
        }
        
        private void DisplayScore(int score) => playerScoreText.text = score.ToString("000000");
        
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

            AS.Play();
            LoadScores();
            StartCoroutine(WaitToScene());
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
                if (i > 9) break;
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

        private IEnumerator WaitToScene()
        {
            submitButton.enabled = false;
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(deathState ? "YouLost" : "Credits");
        }
        
    }
}