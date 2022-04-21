using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject target;
    public GameObject groupParent;

    [SerializeField] private GameObject gunPosition;
    [SerializeField] private float fireRate;
    [SerializeField] private float minTargetDistance, maxTargetDistance;

    private float _distanceToTarget;

    private void Update()
    {
        var targetTransform = target.transform;
        _distanceToTarget = (targetTransform.position - transform.position).magnitude;
        
        // if you're not within targeting distance, look towards the path and exit
        if (!(_distanceToTarget > minTargetDistance || _distanceToTarget < maxTargetDistance))
        { 
            transform.LookAt(groupParent.transform.position + groupParent.transform.forward);
            return;
        }

        // look at player
        transform.LookAt(targetTransform.position);
        
        // fire
    }
}
