using UnityEngine;

namespace FlockSimulation
{
    [RequireComponent(typeof(Collider))]
    public class Agent : MonoBehaviour
    {
        Flock _flock;
        public Flock Flock => _flock;
        Collider _collider;
        public Collider Collider => _collider;
        Vector3 refVelocity;
        void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Initialize(Flock flock)
        {
            _flock = flock;
        }

        public void Move(Vector3 velocity)
        {
            // transform.up = velocity;
            transform.up = Vector3.SmoothDamp(transform.up, velocity, ref refVelocity, 0.25f);
            
            transform.position += velocity * Time.deltaTime;
        }
    }
}
