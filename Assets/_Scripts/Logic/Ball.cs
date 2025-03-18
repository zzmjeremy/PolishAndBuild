using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Movement")]
    [SerializeField] private float ballLaunchSpeed;
    [SerializeField] private float minBallBounceBackSpeed;
    [SerializeField] private float maxBallBounceBackSpeed;
    [Header("References")]
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private Rigidbody rb;

    // New Fields for TrailRenderer
    [Header("Trail Settings")]
    [Tooltip("Drag your TrailRenderer component here.")]
    [SerializeField] private TrailRenderer trailRenderer;

    [Tooltip("Duration of the trail in seconds.")]
    [SerializeField] private float trailTime = 3f;

    [Tooltip("AnimationCurve to control the trail width from start (near the ball) to end.")]
    [SerializeField]
    private AnimationCurve trailWidthCurve = new AnimationCurve(
        new Keyframe(0f, 0.2f),   // width at the ball
        new Keyframe(1f, 0f)     // width at the tail end
    );

    private bool isBallActive;
    private void Start()
    {
        // If TrailRenderer is assigned, set its parameters
        if (trailRenderer != null)
        {
            trailRenderer.time = trailTime;
            trailRenderer.widthCurve = trailWidthCurve;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Paddle"))
        {
            Vector3 directionToFire = (transform.position - other.transform.position).normalized;
            float angleOfContact = Vector3.Angle(transform.forward, directionToFire);
            float returnSpeed = Mathf.Lerp(minBallBounceBackSpeed, maxBallBounceBackSpeed, angleOfContact / 90f);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(directionToFire * returnSpeed, ForceMode.Impulse);
        }
    }

    public void ResetBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isBallActive = false;
        // Clear the trail so it doesn't draw a line from the kill zone back to the anchor
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }
    }

    public void FireBall()
    {
        if (isBallActive) return;
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * ballLaunchSpeed, ForceMode.Impulse);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        isBallActive = true;
    }
}
