using System.Collections.Generic;
using UnityEngine;

namespace FlockSimulation
{
    [CreateAssetMenu (fileName = "Cohesion Behavior", menuName = "Flock/Behavior/Cohesion")]
    public class CohesionBehavior : FlockBehavior
    {
        Vector3 refPosition;

        public override Vector3 CalculateMoveVector(Agent agent, List<Transform> neighbors, Flock flock)
        {
            bool isNeighborsExist = neighbors.Count != 0;
            if (!isNeighborsExist)
                return Vector3.zero;

            Vector3 avgPosition = GetNeighborsAvgPosition();

            // return the avg position offset by the agent position
            avgPosition -= agent.transform.position;
            avgPosition = GetSmoothedAvgPosition();
            
            return avgPosition;

            Vector3 GetNeighborsAvgPosition()
            {
                Vector3 sum = Vector3.zero;
                foreach (Transform neighbor in neighbors)
                    sum += neighbor.position;
                return sum / neighbors.Count;
            }
            Vector3 GetSmoothedAvgPosition()
            {
                return Vector3.SmoothDamp(agent.transform.up, avgPosition, ref refPosition, flock.DirectionChangeSmoothTime);
            }
        }
    }
}
