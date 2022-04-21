using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EnemyGroupBehavior : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CinemachineSmoothPath trackPath;

    [Header("Enemy Information")]
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float enemySpeed;

    [Header("Automatic Path Generation")]
    [SerializeField] private bool autoGeneratePath = true;
    [SerializeField] private float randomCoordOffset;
    [SerializeField] private float minDistanceOffset, maxDistanceOffset;
    [SerializeField] private float targetTrackOffset;
    [SerializeField] private float swoopDistance;

    private CinemachineDollyCart _dollyCart;
    private GameObject _trackedObject;
    private bool _begunSwoop;
    
    private void Awake()
    {
        _dollyCart = GetComponent<CinemachineDollyCart>();
    }

    private void Start()
    {
        foreach (var enemy in enemies) enemy.SetActive(false);
        _begunSwoop = false;
    }

    private void Update()
    {
        // if you've reached the end, stop
        if (_dollyCart.m_Position >= trackPath.PathLength) return;

        // if the enemy group reaches the swoopDistance, it stops tracking the player and begins it's swoop
        if ((_trackedObject.transform.position - transform.position).magnitude < swoopDistance && !_begunSwoop)
        {
            var newTrackWaypoints = new CinemachineSmoothPath.Waypoint[3];
            newTrackWaypoints[0] = trackPath.m_Waypoints[0];
            newTrackWaypoints[1] = trackPath.m_Waypoints[1];
            
            _begunSwoop = true;
            
            // set the last waypoint in front of the tracked object
            var forward = _trackedObject.transform.forward;
            var positionForwardTracked = forward * Random.Range(minDistanceOffset, maxDistanceOffset);
            positionForwardTracked.x += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionForwardTracked.y += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionForwardTracked.z += Random.Range(-randomCoordOffset, randomCoordOffset);

            newTrackWaypoints[2].position = positionForwardTracked + _trackedObject.transform.position +
                                            (forward * targetTrackOffset);

            trackPath.m_Waypoints = newTrackWaypoints;
        }
        else
        {
            // set the path's next waypoint to the target
            trackPath.m_Waypoints[1].position = _trackedObject.transform.position 
                                                + (_trackedObject.transform.forward * targetTrackOffset);
        }
    }

    public void BeginPathing(GameObject obj)
    {
        _trackedObject = obj;
        
        if (autoGeneratePath)
        {
            // put the first waypoint behind the tracked object
            var forward = _trackedObject.transform.forward;
            var positionBehindTracked = -forward * Random.Range(minDistanceOffset, maxDistanceOffset);
            positionBehindTracked.x += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionBehindTracked.y += Random.Range(-randomCoordOffset, randomCoordOffset);
            positionBehindTracked.z += Random.Range(-randomCoordOffset, randomCoordOffset);


            trackPath.m_Waypoints = new CinemachineSmoothPath.Waypoint[2];
            var position = _trackedObject.transform.position;
            trackPath.m_Waypoints[0].position = positionBehindTracked + position;
            trackPath.m_Waypoints[1].position = position
                                                + (forward * targetTrackOffset);
        }
        
        // activate the enemies and give them the tracked object as a target
        foreach (var enemy in enemies)
        {
            enemy.SetActive(true);
            enemy.GetComponent<EnemyBehavior>().target = _trackedObject;
        }
        
        _dollyCart.m_Speed = enemySpeed;
    }
}
