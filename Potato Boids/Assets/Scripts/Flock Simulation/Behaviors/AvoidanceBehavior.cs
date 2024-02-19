using System.Collections.Generic;
using UnityEngine;

namespace FlockSimulation
{
    [CreateAssetMenu (fileName = "Avoidance Behavior", menuName = "Flock/Behavior/Avoidance")]
    public class AvoidanceBehavior : FlockBehavior
    {
        public override Vector3 CalculateMoveVector(Agent agent, List<Transform> neighbors, Flock flock)
        {
            bool isNeighborsExist = neighbors.Count != 0;
            if (!isNeighborsExist)
                return Vector3.zero;

            return GetNeighborsAvgAvoidancePosition();

            Vector3 GetNeighborsAvgAvoidancePosition()
            {
                Vector3 sum = Vector3.zero;
                int neighborsToAvoidCount = 0;
                foreach (Transform neighbor in neighbors)
                {
                    bool isNeighborWithinAvoidanceDistance =
                        // (neighbor.position - agent.transform.position).sqrMagnitude < flock.NeighborAvoidanceRadiusSquared;
                        Vector3.Distance(neighbor.position, agent.transform.position) < flock.NeighborAvoidanceRadius;
                    if (isNeighborWithinAvoidanceDistance)
                    {
                        sum += agent.transform.position - neighbor.position;
                        neighborsToAvoidCount++;
                    }
                }

                return neighborsToAvoidCount > 0 ? sum / neighborsToAvoidCount : sum;
            }
        }
    }
}
