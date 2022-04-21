using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Vector2 limits = new Vector2(5, 3);
    private Vector2 movement;
    private InputManager _inputManager;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = InputManager.createInstance();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }
    
    void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;

        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
    }

    private void movePlayer()
    {
        movement = _inputManager.getPlayerMovement();
        transform.localPosition += new Vector3(movement.x, movement.y, 0f) * speed * Time.deltaTime;
        clampPosition();
    }

    private void clampPosition()
    {
        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);
        playerPos.x = Mathf.Clamp01(playerPos.x);
        playerPos.y = Mathf.Clamp01(playerPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(playerPos);
    }
}
