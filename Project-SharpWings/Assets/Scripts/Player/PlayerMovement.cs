using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("General")] 
        [SerializeField] private float maxMeter;
        [SerializeField] private float meterDepletion;
        private float _meter;
        
        [Header("Dolly Cart Stats")]
        [SerializeField] private float defaultSpeed = 6;
        [SerializeField] private float breakSpeed = 3;
        [SerializeField] private float boostSpeed = 12;
        [SerializeField] private float lookSpeed = 340;
        [SerializeField] private float leanLimit = 80;
        private float _cartSpeed;

        [Header("Camera Stats")] 
        [SerializeField] private float defaultFoV = 45;
        [SerializeField] private float breakFoV = 50;
        [SerializeField] private float boostFoV = 40;
        [SerializeField] private float zoomSpeed = 10;
        private float _fov;

        [Header("References")] 
        public Transform aimTarget;
        public CinemachineVirtualCamera vCam;
        
        private AudioSource _playerAudio;
        private float _pitch;
        
        private InputManager _inputManager;
        private Vector2 _inputDirection;
        private CinemachineDollyCart _cmCart;
        private Camera _mainCamera;

        private void Awake()
        {
            _inputManager = InputManager.CreateInstance();
            _cmCart = transform.parent.GetComponent<CinemachineDollyCart>();
            _mainCamera = Camera.main;
            _playerAudio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _meter = maxMeter;
            _cartSpeed = defaultSpeed;
            _fov = defaultFoV;
            _pitch = 1;
        }

        private void Update()
        {
            if (_inputManager.PlayerBoost() && _meter > 0)
            {
                _meter -= meterDepletion * Time.deltaTime;
                _cartSpeed = boostSpeed;
                _fov = boostFoV;
                _pitch = 1.1f;
            }
            else if (_inputManager.PlayerBreak() && _meter > 0)
            {
                _meter -= meterDepletion * Time.deltaTime;
                _cartSpeed = breakSpeed;
                _fov = breakFoV;
                _pitch = 0.9f;
            }
            else
            {
                if (_meter < maxMeter) _meter += meterDepletion * Time.deltaTime;
                _cartSpeed = defaultSpeed;
                _fov = defaultFoV;
                _pitch = 1;
            }
            
            _inputDirection = _inputManager.GetPlayerMovement();
            MovePlayer();
            RotationLook(_inputDirection.x, _inputDirection.y);
            HorizontalLean(transform,  _inputDirection.x, 0.1f);
            _playerAudio.pitch = _pitch;
        }
        
        private void MovePlayer()
        {
            transform.localPosition += new Vector3(_inputDirection.x, _inputDirection.y, 0f) * (_cartSpeed * Time.deltaTime);
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

        private void RotationLook(float h, float v)
        {
            aimTarget.parent.position = Vector3.zero;
            aimTarget.localPosition = new Vector3(h, v, 1);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                Quaternion.LookRotation(aimTarget.position), 
                Mathf.Deg2Rad * lookSpeed * Time.deltaTime);
        }
        
        private void HorizontalLean(Transform target, float axis, float lerpTime)
        {
            var targetEulerAngels = target.localEulerAngles;
            target.localEulerAngles = new Vector3(
                targetEulerAngels.x, 
                targetEulerAngels.y, 
                Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime)
                );
        }

        public float GetMeterRatio() => _meter / maxMeter;
    }
}
