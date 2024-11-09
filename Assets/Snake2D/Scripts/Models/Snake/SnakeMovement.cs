using System;
using System.Collections.Generic;
using UnityEngine;
using Games.Snake2D.Core;

namespace Games.Snake2D.Snake
{
    [Serializable]
    public class SnakeMovement
    {
        private Transform snakeHead;
        private Vector2 direction = Vector2.right;
        private Vector2 targetDirection = Vector2.right;
        private float moveSpeed = 5f;
        [SerializeField] private float bodySegmentSpacing = 0.5f;
        private List<Vector3> bodyPositions = new List<Vector3>();

        public Vector2 CurrentDirection => direction;
        public float BodySegmentSpacing => bodySegmentSpacing;

        public void Init(Transform head)
        {
            snakeHead = head;
            bodyPositions.Add(head.position);
        }

        public void OnUpdate(LinkedList<SnakeBodyNode> snakeBody)
        {
            HandleInput();
            MoveSnakeHead();
            UpdateBodyMovement(snakeBody);
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
                targetDirection = Vector2.up;
            if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
                targetDirection = Vector2.down;
            if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
                targetDirection = Vector2.left;
            if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
                targetDirection = Vector2.right;

            direction = targetDirection;
        }

        private void MoveSnakeHead()
        {
            Vector3 newPosition = snakeHead.position + (Vector3)(direction.normalized * moveSpeed * Time.deltaTime);
            snakeHead.position = WrapPosition(newPosition);
            
            if (bodyPositions.Count > 0)
            {
                bodyPositions[0] = snakeHead.position;
            }
        }

        private void UpdateBodyMovement(LinkedList<SnakeBodyNode> snakeBody)
        {
            while (bodyPositions.Count < snakeBody.Count + 1)
            {
                bodyPositions.Add(snakeHead.position);
            }

            // Update all body positions
            for (int i = bodyPositions.Count - 1; i > 0; i--)
            {
                Vector3 previousPos = bodyPositions[i - 1];
                Vector3 currentPos = bodyPositions[i];
                
                // Handle wrapping when calculating direction
                Vector3 directionToTarget = GetShortestDirection(currentPos, previousPos);
                
                // Calculate new position
                Vector3 newPosition = previousPos - directionToTarget.normalized * bodySegmentSpacing;
                // Wrap the new position if needed
                bodyPositions[i] = WrapPosition(newPosition);
            }

            // Move body segments to their new positions
            int index = 1;
            foreach (SnakeBodyNode bodyPart in snakeBody)
            {
                if (index < bodyPositions.Count)
                {
                    bodyPart.MoveToPosition(bodyPositions[index]);
                }
                index++;
            }
        }

        private Vector3 GetShortestDirection(Vector3 fromPosition, Vector3 toPosition)
        {
            float verticalBoundary = Camera.main.orthographicSize;
            float horizontalBoundary = verticalBoundary * Camera.main.aspect;
            
            Vector3 direction = toPosition - fromPosition;
            
            // Check X-axis wrapping
            float dx = Mathf.Abs(direction.x);
            if (dx > horizontalBoundary)
            {
                float wrappedX = (2 * horizontalBoundary) - dx;
                if (wrappedX < dx)
                {
                    direction.x = wrappedX * (direction.x > 0 ? -1 : 1);
                }
            }
            
            // Check Y-axis wrapping
            float dy = Mathf.Abs(direction.y);
            if (dy > verticalBoundary)
            {
                float wrappedY = (2 * verticalBoundary) - dy;
                if (wrappedY < dy)
                {
                    direction.y = wrappedY * (direction.y > 0 ? -1 : 1);
                }
            }
            
            return direction;
        }

        private Vector3 WrapPosition(Vector3 position)
        {
            float verticalBoundary = Camera.main.orthographicSize;
            float horizontalBoundary = verticalBoundary * Camera.main.aspect;
            Vector3 wrappedPosition = position;

            // Wrap horizontally
            if (wrappedPosition.x > horizontalBoundary)
                wrappedPosition.x = -horizontalBoundary;
            else if (wrappedPosition.x < -horizontalBoundary)
                wrappedPosition.x = horizontalBoundary;

            // Wrap vertically
            if (wrappedPosition.y > verticalBoundary)
                wrappedPosition.y = -verticalBoundary;
            else if (wrappedPosition.y < -verticalBoundary)
                wrappedPosition.y = verticalBoundary;

            return wrappedPosition;
        }

        public void Reset()
        {
            direction = Vector2.right;
            targetDirection = Vector2.right;
            bodyPositions.Clear();
            bodyPositions.Add(snakeHead.position);
        }
    }
}