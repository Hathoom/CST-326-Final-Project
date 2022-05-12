using UnityEngine;

public class rotateObject : MonoBehaviour
{
    [Header("Rotation Info")] 
    public Vector3 toRotate;
    public float rotationSpeed;
    public float timer;
    public bool playSound;
    public AudioClip sound;
    public AudioSource audioSource;
    private bool startRotating = false;
    
    // Update is called once per frameW
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
            audioSource.clip = sound;
            audioSource.enabled = true;
        }
        startRotating = true;
    }
    
}
