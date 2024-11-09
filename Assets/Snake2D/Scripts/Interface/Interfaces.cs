using UnityEngine;

namespace Games.Snake2D.Core
{
    public interface IUpdatable
    {
        void OnUpdate();
    }
    
    public interface ICollidable
    {
        void OnCollision(GameObject other);
    }
}
