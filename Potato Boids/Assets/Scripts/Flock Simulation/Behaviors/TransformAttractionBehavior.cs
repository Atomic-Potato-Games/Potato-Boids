using System.Collections.Generic;
using UnityEngine;

namespace FlockSimulation
{
    [CreateAssetMenu (fileName = "Attraction Behavior", menuName = "Flock/Behavior/Attraction")]
    public class AttractionBehavior : FlockBehavior
    {
        [Tooltip("How far the agents will go before trying to attract again")]
        [SerializeField] float _radius;
        [SerializeField, Range(0, 1)] float _attractionLooseness = .1f;

        Vector3 refPosition;
        public override Vector3 CalculateMoveVector(Agent agent, List<Transform> neighbors, Flock flock)
        {
            Vector3 attractionOffset = flock.Attractor.position - agent.transform.position;
            float distanceFromAttractorWithRadiusPercentage = attractionOffset.magnitude / _radius;
            Vector3 newPosition = Vector3.zero;
            if (distanceFromAttractorWithRadiusPercentage >= _attractionLooseness)
                newPosition = attractionOffset * Mathf.Pow(distanceFromAttractorWithRadiusPercentage, 2);
            // return Vector3.SmoothDamp(agent.transform.up, newPosition, ref refPosition, flock.DirectionChangeSmoothTime);
            return newPosition;
        }
    }
}
