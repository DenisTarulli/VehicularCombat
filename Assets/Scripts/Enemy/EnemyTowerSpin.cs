using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    [SerializeField] private float spinThreshold;
    [SerializeField] private float aimingDistance;

    private Transform player;
    private EnemyMovement enemy;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyMovement>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= aimingDistance)
            SpinTower(SetSpinDirection());
    }

    private void SpinTower(float direction)
    {
        if (direction == 1f)
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

        if (direction == -1f)
            transform.Rotate(Vector3.up, -spinSpeed * Time.deltaTime);
    }

    private float SetSpinDirection()
    {
        float direction = 0f;

        Vector3 playerDirection = player.position - transform.position;
        playerDirection.Normalize();

        int forwardQuadrant = enemy.FindQuadrant(transform.forward);
        int toPlayerQuadrant = enemy.FindQuadrant(playerDirection);

        if (forwardQuadrant == toPlayerQuadrant)
            direction = SameQuadrantRotation(transform.forward, playerDirection, toPlayerQuadrant);
        else
        {
            if (forwardQuadrant == 1)
            {
                switch (toPlayerQuadrant)
                {
                    case 2:
                        direction = -1f;
                        break;
                    case 3:
                        direction = 1f;
                        break;
                    case 4:
                        direction = 1f;
                        break;
                }
            }
            else if (forwardQuadrant == 2)
            {
                switch (toPlayerQuadrant)
                {
                    case 1:
                        direction = 1f;
                        break;
                    case 3:
                        direction = -1f;
                        break;
                    case 4:
                        direction = 1f;
                        break;
                }
            }
            else if (forwardQuadrant == 3)
            {
                switch (toPlayerQuadrant)
                {
                    case 1:
                        direction = 1f;
                        break;
                    case 2:
                        direction = 1f;
                        break;
                    case 4:
                        direction = -1f;
                        break;
                }
            }
            else
            {
                switch (toPlayerQuadrant)
                {
                    case 1:
                        direction = -1f;
                        break;
                    case 2:
                        direction= 1f;
                        break;
                    case 3:
                        direction = 1f;
                        break;
                }
            }
        }

        return direction;
    }

    public float SameQuadrantRotation(Vector3 forward, Vector3 playerDir, int currentQuad)
    {
        float rotation = 0f;

        float xFwdQuad = forward.x;
        float xPlayerQuad = playerDir.x;

        float xDiff = Mathf.Abs(xFwdQuad - xPlayerQuad);

        if (currentQuad == 1 || currentQuad == 2)
        {
            if (xDiff >= spinThreshold)
            {
                if (xFwdQuad < xPlayerQuad)
                    rotation = 1f;
                else
                    rotation = -1f;
            }

        }
        else
        {
            if (xDiff >= spinThreshold)
            {
                if (xFwdQuad > xPlayerQuad)
                    rotation = 1f;
                else
                    rotation = -1f;
            }
        }

        return rotation;
    }
}
