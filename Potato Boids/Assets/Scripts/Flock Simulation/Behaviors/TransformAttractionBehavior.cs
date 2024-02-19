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

        public override Vector3 CalculateMoveVector(Agent agent, List<Transform> neighbors, Flock flock)
        {
            Vector3 attractionOffset = flock.Attractor.position - agent.transform.position;
            float distanceFromAttractorWithRadiusPercentage = attractionOffset.magnitude / _radius;
            if (distanceFromAttractorWithRadiusPercentage > _attractionLooseness)
                return attractionOffset * Mathf.Pow(distanceFromAttractorWithRadiusPercentage, 2);
            else
                return Vector3.zero;

        }
    }
}
