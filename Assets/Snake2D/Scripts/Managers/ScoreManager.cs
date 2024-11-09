using UnityEngine;

namespace Games.Snake2D.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        private int score;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void AddScore(int value)
        {
            score += value;
            Debug.Log($"Score: {score}");
        }

        public int GetScore()
        {
            return score;
        }
    }
}