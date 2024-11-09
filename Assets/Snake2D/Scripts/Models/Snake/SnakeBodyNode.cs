using UnityEngine;

namespace Games.Snake2D.Snake
{
    public class SnakeBodyNode : MonoBehaviour
    {
        public void MoveToPosition(Vector3 newPosition)
        {
            // Move this body part directly to maintain precise chain formation
            transform.position = newPosition;
        }
    }
}