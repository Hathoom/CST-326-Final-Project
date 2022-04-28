using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    [HideInInspector]
    public GameObject groupParent;

    [HideInInspector] public GameObject bulletPrefab;
    [HideInInspector] public float bulletSpeed;
    
    [HideInInspector]
    public float fireRate, fireRateOffset;
    private float _fireTimer;
    [HideInInspector]
    public float minTargetDistance, maxTargetDistance;
    
    [HideInInspector]
    public string currentState;
    
    // swarming stuff
    [HideInInspector] 
    public float swarmRadius, rotationSpeed;
    [HideInInspector]
    public Vector3 rotationAxis;

    private void Start()
    {
        fireRate += fireRateOffset;
        fireRate = Time.time;
        var center = groupParent.transform.position;
        var transformPosition = transform.position;
        transformPosition = (Random.insideUnitSphere - center).normalized * swarmRadius + center;
        transform.position = transformPosition;
    }

    private void Update()
    {
        var thisPosition = transform.position;
        
        // swarm around group parent
        var center = groupParent.transform.position;
        transform.RotateAround(center, rotationAxis, rotationSpeed * Time.deltaTime);
        var desiredPosition = (thisPosition - center).normalized * swarmRadius + center;
        thisPosition = desiredPosition;

        // check for range when pathing
        var targetPosition = target.transform.position;
        var toTarget = targetPosition - thisPosition;
        var distanceToTarget = toTarget.magnitude;
        var dot = Vector3.Dot(targetPosition, toTarget);
        
        if (currentState == "Pathing" && (distanceToTarget < minTargetDistance || distanceToTarget > maxTargetDistance || dot < 0))
        { 
            transform.LookAt(groupParent.transform.position + groupParent.transform.forward);
            _fireTimer = Time.time;
            return;
        }
        
        // look at player
        transform.LookAt(targetPosition);
        
        // fire
        if (Time.time - _fireTimer > fireRate)
        {
            // Debug.Log("fire" + gameObject.name);
            _fireTimer = Time.time;
            var localTransform = transform;
            Debug.Log(localTransform.position + groupParent.transform.position);
            var bullet = Instantiate(bulletPrefab,
                localTransform.TransformPoint(localTransform.forward),
                localTransform.rotation).GetComponent<RudimentaryBullet>();
            bullet.speed = bulletSpeed;
        }
    }
}
