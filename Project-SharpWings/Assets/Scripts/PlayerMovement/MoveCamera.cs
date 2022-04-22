using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{


    public Transform targetToFollow;
    public Vector3 offset = Vector3.zero;

    public Vector2 limits = new(5, 3);

    [Range(0,1)]
    public float followTime;
    
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            transform.localPosition = offset;
        }
        moveCamera();
    }

    private void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), 
            Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
    }

    private void moveCamera()
    {
        /*Debug.Log(targetToFollow.localPosition);*/
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = targetToFollow.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, 
            new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z), 
            ref velocity, followTime);
    }
}
