using UnityEngine;
using System.Linq;
using Games.Snake2D.Core;

namespace Games.Snake2D.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private bool isGameOver = false;
        private IUpdatable[] updatables;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            updatables = FindObjectsOfType<MonoBehaviour>().OfType<IUpdatable>().ToArray();
        }

        private void Update()
        {
            if (isGameOver) return;

            foreach (var updatable in updatables)
            {
                updatable.OnUpdate();
            }
        }

        public void GameOver()
        {
            isGameOver = true;
            Debug.Log("Game Over!");
        }
    }
}