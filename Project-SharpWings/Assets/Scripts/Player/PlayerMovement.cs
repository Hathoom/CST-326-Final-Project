using System;
using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Dolly Cart Stats")]
        [SerializeField] private float defaultSpeed;
        [SerializeField] private float breakSpeed;
        [SerializeField] private float boostSpeed;
        private float _cartSpeed;

        [Header("Camera Stats")] 
        [SerializeField] private float defaultFoV = 45;
        [SerializeField] private float breakFoV = 50;
        [SerializeField] private float boostFoV = 40;
        [SerializeField] private float zoomSpeed = 10;
        private float _fov;

        [Header("References")]
        public CinemachineVirtualCamera vCam;

        private InputManager _inputManager;
        private Vector2 _inputDirection;
        private CinemachineDollyCart _cmCart;
        private Camera _mainCamera;

        private void Awake()
        {
            _inputManager = InputManager.CreateInstance();
            _cmCart = transform.parent.GetComponent<CinemachineDollyCart>();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _cartSpeed = defaultSpeed;
            _fov = defaultFoV;
        }

        private void Update()
        {
            if (_inputManager.PlayerBoost())
            {
                _cartSpeed = boostSpeed;
                _fov = boostFoV;
            }else if (_inputManager.PlayerBreak())
            {
                _cartSpeed = breakSpeed;
                _fov = breakFoV;
            }
            else
            {
                _cartSpeed = defaultSpeed;
                _fov = defaultFoV; 
            }
            
            MovePlayer();
        }
        
        private void MovePlayer()
        {
            _inputDirection = _inputManager.GetPlayerMovement();
            transform.localPosition += new Vector3(_inputDirection.x, _inputDirection.y, 0f) * _cartSpeed * Time.deltaTime;
            _cmCart.m_Speed = _cartSpeed;
            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, _fov, zoomSpeed * Time.deltaTime);
            ClampPosition();
        }

        private void ClampPosition()
        {
            var playerPos = _mainCamera.WorldToViewportPoint(transform.position);
            playerPos.x = Mathf.Clamp01(playerPos.x);
            playerPos.y = Mathf.Clamp01(playerPos.y);
            transform.position = _mainCamera.ViewportToWorldPoint(playerPos);
        }
    }
}
