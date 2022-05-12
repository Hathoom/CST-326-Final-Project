using UnityEditor;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [Header("Rotation Info")] 
    public Vector3 toRotate;
    public Vector3 toMove;
    public bool movePosition;
    public float rotationSpeed;
    public float timer;
    public bool playSound;
    public AudioClip sound;
    public AudioSource audioSource;
    private bool startRotating;
    private bool startMoving;
    
    // Update is called once per frameW
    void Update()
    {
        if (startRotating && timer > 0f && !movePosition)
        {
            transform.Rotate(toRotate * rotationSpeed);
            timer -= Time.deltaTime;
        }
        else if(timer > 0f && startMoving)
        {
            transform.position += toMove * Time.deltaTime;
            timer -= Time.deltaTime;
        }
    }

    public void moveObject()
    {
        startMoving = true;
    }

    public void OpenDoor()
    {
        if (playSound)
        {
            audioSource.clip = sound;
            audioSource.enabled = true;
        }
        startRotating = true;
    }
    
}
