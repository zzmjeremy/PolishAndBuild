using UnityEngine;
using DG.Tweening;

public class PunchScaleOnHit : MonoBehaviour
{
    // Adjustable punch scale, duration, vibrato, and elasticity
    [SerializeField] private Vector3 punch = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private float elasticity = 1f;

    //Trigger the punch scale effect
    public void Punch()
    {
        // Kill any existing tweens on this transform to avoid overlapping effects
        transform.DOKill();
        // DOPunchScale animates the scale of the transform with a punch effect
        transform.DOPunchScale(punch, duration, vibrato, elasticity);
    }
}
