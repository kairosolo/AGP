using UnityEngine;

public class TrebuchetRangeCalculator : MonoBehaviour
{
    [Header("Referemces")]
    [SerializeField] private Rigidbody projectile;

    [Header("Debug")]
    private bool calculateOnKeyPress = true;
    private KeyCode calculateKey = KeyCode.Space;

    private void Update()
    {
        if (calculateOnKeyPress && Input.GetKeyDown(calculateKey))
        {
            float predictedRange = CalculateExpectedRange(projectile.linearVelocity, projectile.transform.position.y);

            Debug.Log("Predicted Range: " + predictedRange.ToString("F2") + " meters");
        }
    }

    private float CalculateExpectedRange(Vector3 launchVelocity, float launchHeight)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);
        float vx = launchVelocity.x;
        float vz = launchVelocity.z;

        float horizontalSpeed = new Vector2(vx, vz).magnitude;

        float vy = launchVelocity.y;

        float discriminant = (vy * vy) + (2 * gravity * launchHeight);

        if (discriminant < 0)
            return 0f;

        float time = (vy + Mathf.Sqrt(discriminant)) / gravity;

        float range = horizontalSpeed * time;

        return range;
    }
}