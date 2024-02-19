using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlockSimulation
{
    public class Flock : MonoBehaviour
    {
        #region Global Variables
        [SerializeField] Agent _agentPrefab;
        List<Agent> _agents = new List<Agent>();
        [SerializeField] FlockBehavior _flockBehavior;

        [Space, Header("Flock Properites")]
        [SerializeField, Min(0)] int _agentsCount = 100;
        [SerializeField, Min(0)] float _agentsSpawnDensity = .08f; 

        [Space, Header("Agent Properites")]
        [SerializeField, Min(0)] float _speedMultiplier = 3f;
        [SerializeField, Min(0)] float _maxSpeed = 10f;
        [SerializeField, Min(0)] float _neighborDetectionRadius = 1.5f;
        [SerializeField, Min(0)] float _neighborAvoidanceRadiusMultiplier = .5f;


        float _maxSpeedSquared;
        float _neighborDetectionRadiusSquared;
        float _neighborAvoidanceRadiusSquared;
        #endregion 

        #region Execution
        void Awake()
        {
            _maxSpeedSquared = Mathf.Pow(_maxSpeed, 2);
            _neighborDetectionRadiusSquared = Mathf.Pow(_neighborDetectionRadius, 2);
            _neighborAvoidanceRadiusSquared = _neighborDetectionRadius * Mathf.Pow(_neighborAvoidanceRadiusMultiplier, 2);
            SpawnAgents();
        }

        void SpawnAgents()
        {
            for (int i=0; i < _agentsCount; i++)
            {
                Agent agent = Instantiate(
                    _agentPrefab,
                    Random.insideUnitSphere * _agentsCount * _agentsSpawnDensity,
                    Quaternion.identity,
                    transform
                );
                agent.name = "Agent " + i.ToString();
                agent.Initialize(this);
                _agents.Add(agent);
            }
        }
        #endregion
    }
}
