using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public TMP_Text timer;

    public float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer.SetText("Mission Timer: " + counter);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        timer.SetText("Mission Timer: " + (int)counter);
    }
}
