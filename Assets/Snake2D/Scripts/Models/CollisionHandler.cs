using UnityEngine;
using Games.Snake2D.Core;

namespace Games.Snake2D
{
    public class CollisionHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Collision detected!");
            var collidable = collision.GetComponent<ICollidable>();
            collidable?.OnCollision(gameObject);
        }
    }
}