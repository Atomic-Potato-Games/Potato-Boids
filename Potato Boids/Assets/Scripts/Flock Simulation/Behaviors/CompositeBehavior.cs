using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockSimulation
{
    [CreateAssetMenu (fileName = "Composite Behavior", menuName = "Flock/Behavior/Composit")]
    public class CompositeBehavior : FlockBehavior
    {
        [SerializeField] FlockBehavior[] _behaviors;
        [SerializeField, Min(0)] float[] _weights;

        public override Vector3 CalculateMoveVector(Agent agent, List<Transform> neighbors, Flock flock)
        {
            if (_behaviors.Length != _weights.Length)
                throw new System.Exception("Inequal weights count to behaviors!");
            
            Vector3 moveLocation = Vector3.zero;
            for (int i=0; i < _behaviors.Length; i++)
            {
                Vector3 behaviorLocation = _behaviors[i].CalculateMoveVector(agent, neighbors, flock);
                if (behaviorLocation != Vector3.zero)
                {
                    if (behaviorLocation.sqrMagnitude > Mathf.Pow(_weights[i], 2))
                    {
                        behaviorLocation.Normalize();
                        behaviorLocation *= _weights[i];
                    }
                }
                moveLocation += behaviorLocation;
            }
            return moveLocation;
        }
    }
}
