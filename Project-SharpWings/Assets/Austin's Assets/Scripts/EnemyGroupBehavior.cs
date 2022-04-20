using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EnemyGroupBehavior : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float enemySpeed;

    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private CinemachineSmoothPath trackPath;

    private GameObject _trackedObject;

    private void Awake()
    {
        dollyCart = GetComponent<CinemachineDollyCart>();
    }

    private void Start()
    {
        foreach (var enemy in enemies) enemy.SetActive(false);
    }

    private void Update()
    {
        if (dollyCart.m_Position !< trackPath.PathLength) return;
        foreach (var enemy in enemies) enemy.SetActive(false);
    }

    public void BeginPathing(GameObject obj, bool autoGeneratePath)
    {
        _trackedObject = obj;
        
        if (autoGeneratePath)
        {
            trackPath.m_Waypoints = new CinemachineSmoothPath.Waypoint[3];
            trackPath.m_Waypoints[0].position = new Vector3(0f, 0f, 0f);
            trackPath.m_Waypoints[1].position = _trackedObject.transform.position;
            trackPath.m_Waypoints[2].position = new Vector3(50f, 50f, 50f);
        }
        
        foreach (var enemy in enemies)
        {
            enemy.SetActive(true);
            // pass target to enemy
        }
        
        dollyCart.m_Speed = enemySpeed;
    }
}
