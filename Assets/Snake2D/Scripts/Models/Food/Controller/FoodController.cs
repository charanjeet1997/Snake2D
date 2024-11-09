using System;
using UnityEngine;
using Games.Snake2D.Core;
using ServiceLocatorFramework;
using Random = UnityEngine.Random;

namespace Games.Snake2D.Food
{
    public class FoodController : MonoBehaviour, ICollidable
    {
        private Camera mainCamera;
        
        private void Start()
        {
            mainCamera = Camera.main; // Get the main camera
            RandomizePosition();
        }
        

        public void OnCollision(GameObject other)
        {
            if (other.CompareTag("Snake"))
            {
                var snake = other.GetComponent<Snake.SnakeController>();
                snake.Grow();
                RandomizePosition();
            }
        }

        public void RandomizePosition()
        {
            // Get the camera's orthographic size and aspect ratio
            float verticalBoundary = mainCamera.orthographicSize;
            float horizontalBoundary = verticalBoundary * mainCamera.aspect;

            // Generate random positions within the camera's visible area
            float x = Mathf.Floor(Random.Range(-horizontalBoundary, horizontalBoundary));
            float y = Mathf.Floor(Random.Range(-verticalBoundary, verticalBoundary));

            // Set the food's position
            transform.position = new Vector2(x, y);
        }
    }
}