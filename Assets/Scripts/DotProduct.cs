using System;
using UnityEngine;

public class DotProduct : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float fieldOfView = 45f;

    private void Update()
    {
        CheckIfPlayerIsInFront();
        CheckFieldOfView();
    }

    private void CheckIfPlayerIsInFront()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, toPlayer);

        if (dot > 0)
        {
            Debug.Log("Player is in front");
        }
        else
        {
            Debug.Log("Player is behind");
        }
        Debug.Log(HitFromFront(toPlayer));
    }

    private bool HitFromFront(Vector3 hitDirection)
    {
        hitDirection.Normalize();
        float dot = Vector3.Dot(transform.forward, hitDirection);
        return dot > 0;
    }

    private void CheckFieldOfView()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, toPlayer);

        float threshold = Mathf.Cos(fieldOfView * Mathf.Deg2Rad);

        if (dot > threshold)
        {
            Debug.Log("Player is within field of view");
        }
        else
        {
            Debug.Log("Player is outside field of view");
        }
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);

            Gizmos.color = Color.yellow;
            Quaternion leftRot = Quaternion.Euler(0, -fieldOfView, 0);
            Quaternion rightRot = Quaternion.Euler(0, fieldOfView, 0);

            Gizmos.DrawLine(transform.position, transform.position + leftRot * transform.forward * 3);
            Gizmos.DrawLine(transform.position, transform.position + rightRot * transform.forward * 3);
        }
    }
}