using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{

    private float timePassed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= 20f)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
}
