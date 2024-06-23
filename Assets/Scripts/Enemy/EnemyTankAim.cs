using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankAim : MonoBehaviour
{
    [SerializeField] private float tiltSpeed;
    [SerializeField] private float adjustmentsThresholdDistance;
    [SerializeField] private float maxTilt;
    [SerializeField] private float minTilt;
    [SerializeField] private Transform hitMarker;

    private float distanceToPlayer;
    private float distanceToHit;
    private float hitToPlayerDistance;

    private Transform player;
    private EnemyProjectileThrow enemyThrow;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyThrow = GetComponentInParent<EnemyProjectileThrow>();
    }

    private void Update()
    {
        TiltCanon();
        CanonTiltClamp();
        SetDistances();
        ChangeForce();
    }

    private float SetTiltDirection()
    {
        float direction = 0;        

        if (Mathf.Abs(hitToPlayerDistance) > adjustmentsThresholdDistance)
        {
            if (distanceToHit > distanceToPlayer)
                direction = 1f;
            else
                direction = -1f;
        }

        return direction;
    }

    private void SetDistances()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        distanceToHit = Vector3.Distance(transform.position, hitMarker.position);
        hitToPlayerDistance = distanceToPlayer - distanceToHit;
    }

    private void ChangeForce()
    {
        if (!(Mathf.Abs(hitToPlayerDistance) > adjustmentsThresholdDistance)) return;

        if (distanceToHit > distanceToPlayer)        
            enemyThrow.AdjustForce(-1f);        
        else if (distanceToHit < distanceToPlayer)        
            enemyThrow.AdjustForce(1f);        
    }

    private void TiltCanon()
    {        
        transform.Rotate(Vector3.right, SetTiltDirection() * tiltSpeed * Time.deltaTime);
    }

    private void CanonTiltClamp()
    {
        if (transform.localEulerAngles.x >= 0f && transform.localEulerAngles.x <= 15f || transform.localEulerAngles.x > minTilt)
            transform.localEulerAngles = new(minTilt, 0f, 0f);
        else if (transform.localEulerAngles.x < maxTilt && transform.localEulerAngles.x >= 270f)
            transform.localEulerAngles = new(maxTilt, 0f, 0f);
    }
}
