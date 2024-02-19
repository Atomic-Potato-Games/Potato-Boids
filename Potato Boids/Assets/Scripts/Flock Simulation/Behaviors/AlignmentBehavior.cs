using System.Collections.Generic;
using UnityEngine;

namespace FlockSimulation
{
    [CreateAssetMenu (fileName = "Alignment Behavior", menuName = "Flock/Behavior/Alignment")]
    public class AlignmentBehavior : FlockBehavior
    {
        public override Vector3 CalculateMoveVector(Agent agent, List<Transform> neighbors, Flock flock)
        {
            bool isNeighborsExist = neighbors.Count != 0;
            if (!isNeighborsExist)
                return Vector3.zero;

            return GetNeighborsAvgHeading();

            Vector3 GetNeighborsAvgHeading()
            {
                Vector3 sum = Vector3.zero;
                foreach (Transform neighbor in neighbors)
                    sum += neighbor.up;
                return sum / neighbors.Count;
            }
        }
    }
}
