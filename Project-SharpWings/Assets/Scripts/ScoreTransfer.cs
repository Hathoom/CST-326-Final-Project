using System.Collections;
using SceneControllers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTransfer : MonoBehaviour
{
    public int score;
    public string waitingScene;
    public bool deathState;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartThing()
    {
        StartCoroutine(PassToScene(waitingScene));
    }
    
    private IEnumerator PassToScene(string scene)
    {
        while (SceneManager.GetActiveScene().name != scene)
        {
            yield return null;
        }
            
        var scoreManager = FindObjectOfType<ScoreManager>();
        if (!scoreManager)
        {
            Debug.LogWarning("test1");
        }

        scoreManager.playerScore = score;
        scoreManager.deathState = deathState;
        Debug.Log(scoreManager.playerScore);
        scoreManager.Initialize();
        Destroy(gameObject);
    } 
        
}