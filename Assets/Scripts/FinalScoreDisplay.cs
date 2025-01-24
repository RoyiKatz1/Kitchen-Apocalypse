using TMPro;
using UnityEngine;

public class FinalScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int finalScore;

    void Start()
    {
        finalScore = ScoreManager.Instance.GetScore();
    }
}
