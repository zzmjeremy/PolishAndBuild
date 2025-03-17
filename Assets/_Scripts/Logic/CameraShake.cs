using UnityEngine;
using DG.Tweening;

public class CameraShake : SingletonMonoBehavior<CameraShake>
{
    [SerializeField] private float defaultDuration = 0.2f;
    [SerializeField] private float defaultStrength = 0.2f;

    public static void Shake(float duration, float strength)
    {
        if (Instance != null)
            Instance.OnShake(duration, strength);
    }

    public static void Shake()
    {
        if (Instance != null)
            Instance.OnShake(Instance.defaultDuration, Instance.defaultStrength);
    }

    private void OnShake(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }
}
