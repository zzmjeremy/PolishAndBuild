using UnityEngine;
using DG.Tweening;

public class CameraShake : SingletonMonoBehavior<CameraShake>
{
    // 在Inspector中调整默认震动参数
    [SerializeField] private float defaultDuration = 0.2f;
    [SerializeField] private float defaultStrength = 0.2f;

    // 静态方法可以传入参数进行震动
    public static void Shake(float duration, float strength)
    {
        if (Instance != null)
            Instance.OnShake(duration, strength);
    }

    // 重载方法：如果不传入参数，则使用默认值
    public static void Shake()
    {
        if (Instance != null)
            Instance.OnShake(Instance.defaultDuration, Instance.defaultStrength);
    }

    private void OnShake(float duration, float strength)
    {
        // 利用DoTween实现位置和旋转的震动效果
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }
}
