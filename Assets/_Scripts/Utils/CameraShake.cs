using UnityEngine;
using DG.Tweening;

public class CameraShake : SingletonMonoBehavior<CameraShake>
{
    [SerializeField] private float defaultDuration = 0.2f;
    [SerializeField] private float defaultStrength = 0.2f;

    //Shake the camera with custom duration and strength.
    public static void Shake(float duration, float strength)
    {
        if (Instance != null)
            Instance.OnShake(duration, strength);
    }

    // Overloaded static method using default parameters.
    public static void Shake()
    {
        if (Instance != null)
            Instance.OnShake(Instance.defaultDuration, Instance.defaultStrength);
    }

    // Instance method that performs the actual shake effect.
    private void OnShake(float duration, float strength)
    {
        // Use DoTween to shake the camera's position and rotation
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }
}
