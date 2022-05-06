using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Stats")]
    public float startFOV;
    public float endFOV;
    public float playerSpeed;

    [Header("References")]
    private InputManager _inputManager;
    private CinemachineDollyCart cmCart;
    private CinemachineVirtualCamera vCam;
    
    // variable to store input direction
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = InputManager.createInstance();
        cmCart = GameObject.Find("GameTrack").GetComponent<CinemachineDollyCart>();
        vCam = GameObject.Find("vCam_1").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

        if (_inputManager.playerBoost())
        {
            cmCart.m_Speed = 12;
            playerSpeed = 12;
            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, endFOV, 10 * Time.deltaTime);
        }else if (_inputManager.playerBreak())
        {
            cmCart.m_Speed = 3;
            playerSpeed = 3;
            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, startFOV*0.8f, 10 * Time.deltaTime);
        }
        else
        {
            cmCart.m_Speed = 6;
            playerSpeed = 6;
            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, startFOV, 10 * Time.deltaTime);
        }
    }
    private void movePlayer()
    {
        movement = _inputManager.getPlayerMovement();
        transform.localPosition += new Vector3(movement.x, movement.y, 0f) * playerSpeed * Time.deltaTime;
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
