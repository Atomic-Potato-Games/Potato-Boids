using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlockSimulation
{
    [Serializable]
    public abstract class FlockBehavior : ScriptableObject
    {
        public abstract Vector3 CalculateMoveVector(Agent agent, List<Transform> neighbors, Flock flock);
    }
}
