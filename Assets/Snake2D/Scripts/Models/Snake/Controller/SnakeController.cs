using System;
using UnityEngine;
using System.Collections.Generic;
using Games.Snake2D.Core;
using Games.Snake2D.Game;

namespace Games.Snake2D.Snake
{
    public class SnakeController : MonoBehaviour, IUpdatable
    {
        public Transform snakeHead;
        public GameObject bodyPrefab;
        private LinkedList<SnakeBodyNode> snakeBody = new LinkedList<SnakeBodyNode>();
        [SerializeField] private SnakeMovement movement;

        private void Start()
        {
            movement.Init(snakeHead);
        }

        public void OnUpdate()
        {
            movement.OnUpdate(snakeBody);
        }

        public void Grow()
        {
            Vector3 newPosition;
            if (snakeBody.Count == 0)
            {
                // First body segment - place it behind the head
                Vector3 direction = -movement.CurrentDirection;
                newPosition = snakeHead.position + (Vector3)(direction * movement.BodySegmentSpacing);
            }
            else
            {
                // Place after the last segment
                newPosition = snakeBody.Last.Value.transform.position;
            }

            SnakeBodyNode newBodyPart = Instantiate(bodyPrefab).GetComponent<SnakeBodyNode>();
            newBodyPart.transform.position = newPosition;
            snakeBody.AddLast(newBodyPart);
        }
    }
}
