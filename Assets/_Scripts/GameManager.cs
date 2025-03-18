using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;

    // New fields for score tracking
    [SerializeField] private ScoreCounterUI scoreCounterUI; // Reference to ScoreCounterUI component
    private int currentScore = 0;

    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        AudioManager.Instance.PlaySFX("break");
        // implement particle effect here
        // add camera shake here
        currentBrickCount--;

        // Increase the score by 1 and update UI
        currentScore++;
        Debug.Log("Current Score: " + currentScore);  // Debug output to check score increment

        if (scoreCounterUI != null)
        {
            scoreCounterUI.UpdateScore(currentScore);
        }
        else
        {
            Debug.LogWarning("ScoreCounterUI reference is null!");
        }

        CameraShake.Shake();
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if (currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();
    }
}
