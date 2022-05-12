using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodLuck : MonoBehaviour
{
    public AudioSource audioSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoodLuck()
    {
        audioSound.Play();
    }
}
