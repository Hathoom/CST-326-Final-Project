using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class EnemyGroupBehavior : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CinemachineSmoothPath trackPath;

    [Header("Enemy Information")]
    [SerializeField] private List<EnemyBehavior> enemies;
    [SerializeField] private float enemyGroupSpeed;
    [SerializeField] private float enemyFireRate, randFireOffset;
    [SerializeField] private float minTargetDistance, maxTargetDistance;

    [Header("Automatic Path Generation")]
    [SerializeField] private bool autoGeneratePath = true;
    [SerializeField] private float randomCoordOffset;
    [SerializeField] private float minDistanceOffset, maxDistanceOffset;
    [SerializeField] private float targetTrackOffset;

    [Header("Swarming Settings")] 
    [SerializeField] private float swarmRadius;
    [SerializeField] private float rotationSpeed;


    private CinemachineDollyCart _dollyCart;
    private GameObject _trackedObject;
    private bool _begunPathing;

    private string _currentState;
    
    private void Awake()
    {
        _dollyCart = GetComponent<CinemachineDollyCart>();
    }

    private void Start()
    {
        foreach (var enemy in enemies) enemy.gameObject.SetActive(false);
        _currentState = "none";
        _dollyCart.enabled = false;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case "Pathing":
                // if you've reached the end, stop and disable enemies
                if (_dollyCart.m_Position >= trackPath.PathLength)
                { 
                    gameObject.SetActive(false);
                    foreach (var enemyObj in enemies) enemyObj.gameObject.SetActive(false);
                    _currentState = "none";
                }
                break;
            
            case "Swarming":
                // if there are no longer any enemies, destroy self
                if (!enemies.Any()) Destroy(gameObject);
                
                transform.position = _trackedObject.transform.position;
                
                break;
        }
        
    }

    // This methods takes in an object to track and fires or swarms 
    public void BeginPathing(GameObject obj)
    {
        _trackedObject = obj;
        _currentState = "Pathing";
        
        if (autoGeneratePath)
        {
            // put the first waypoint behind the tracked object
            var forward = _trackedObject.transform.forward;
            var position = _trackedObject.transform.position;
            
            var positionBehindTracked = -forward * Random.Range(minDistanceOffset, maxDistanceOffset);
            positionBehindTracked.x += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionBehindTracked.y += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionBehindTracked.z += Random.Range(-randomCoordOffset, randomCoordOffset);

            // set the last waypoint in front of the tracked object
            var positionForwardTracked = forward * Random.Range(minDistanceOffset, maxDistanceOffset);
            positionForwardTracked.x += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionForwardTracked.y += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionForwardTracked.z += Random.Range(-randomCoordOffset, randomCoordOffset);
            
            trackPath.m_Waypoints = new CinemachineSmoothPath.Waypoint[3];
            trackPath.m_Waypoints[0].position = positionBehindTracked + position + (forward * targetTrackOffset);
            trackPath.m_Waypoints[1].position = position + (forward * targetTrackOffset);
            trackPath.m_Waypoints[2].position = positionForwardTracked + position + (forward * targetTrackOffset);
        }
        
        // activate the enemies and give them the data they need
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.target = _trackedObject;
            enemy.minTargetDistance = minTargetDistance;
            enemy.maxTargetDistance = maxTargetDistance;
            enemy.fireRate = enemyFireRate;
            enemy.fireRateOffset = Random.Range(0, randFireOffset);
            enemy.groupParent = gameObject;
            enemy.currentState = _currentState;

            enemy.swarmRadius = 1;
            enemy.rotationAxis = Random.insideUnitSphere;
            enemy.rotationSpeed = 60;
        }

        _dollyCart.m_Path = trackPath;
        _dollyCart.m_Speed = enemyGroupSpeed;
        _dollyCart.enabled = true;
    }

    public void BeginSwarming(GameObject obj)
    {
        _trackedObject = obj;
        _currentState = "Swarming";
        
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.target = _trackedObject;
            enemy.minTargetDistance = minTargetDistance;
            enemy.maxTargetDistance = maxTargetDistance;
            enemy.fireRate = enemyFireRate;
            enemy.fireRateOffset = Random.Range(0, randFireOffset);
            enemy.groupParent = gameObject;
            enemy.currentState = _currentState;

            enemy.swarmRadius = swarmRadius;
            enemy.rotationAxis = Random.insideUnitSphere; // todo: 
            enemy.rotationSpeed = rotationSpeed;
        }
        
    }
}
