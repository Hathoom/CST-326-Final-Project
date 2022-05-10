using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [Header("Rotation Info")] 
    public Vector3 toRotate;
    public float rotationSpeed;
    public float timer;
    public bool playSound;
    public AudioClip sound;
    public AudioSource audio;
    private bool startRotating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (startRotating && timer > 0f)
        {
            transform.Rotate(toRotate * rotationSpeed);
            timer -= Time.deltaTime;
        }
    }

    public void OpenDoor()
    {
        if (playSound)
        {
            audio.clip = sound;
            audio.enabled = true;
        }
        startRotating = true;
    }
    
}
