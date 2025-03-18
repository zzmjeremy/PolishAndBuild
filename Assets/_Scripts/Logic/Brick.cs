using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;

    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;

        if (!other.gameObject.CompareTag("Ball")) return;

        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        //If this Brick has a PunchScaleOnHit script attached, call Punch()
        var punchScript = GetComponent<PunchScaleOnHit>();
        if (punchScript != null)
        {
            punchScript.Punch();
            // Wait for a short duration so the player can see the punch animation
            yield return new WaitForSeconds(0.3f);
        }

        GameManager.Instance.OnBrickDestroyed(transform.position);

        Destroy(gameObject);
    }
}

