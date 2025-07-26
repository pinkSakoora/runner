using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI DeathScoreText;
    public Collider2D PlayerCollider;
    public int CurrentScore;

    public void IncrementScore()
    {
        CurrentScore++;
        ScoreText.text = CurrentScore.ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            IncrementScore();
        }
    }
    public void OnDeath()
    {
        DeathScoreText.text = "Score: " + CurrentScore.ToString();
    }
}
