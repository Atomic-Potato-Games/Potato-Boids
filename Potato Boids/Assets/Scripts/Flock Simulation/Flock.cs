﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

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
        [Tooltip("If an attraction behavior is added, then this field is required.")]
        [SerializeField] Transform _attractor;
        public Transform Attractor => _attractor;

        [Space, Header("Agent Properites")]
        [SerializeField, Min(0)] float _speedMultiplier = 3f;
        [SerializeField, Min(0)] float _maxSpeed = 10f;
        [SerializeField, Min(0)] float _neighborDetectionRadius = 1.5f;
        [SerializeField, Min(0)] float _neighborAvoidanceRadius = 1f;
        public float NeighborAvoidanceRadius => _neighborAvoidanceRadius;
        [SerializeField, Min(0)] float _directionChangeSmoothTime = .5f;
        public float DirectionChangeSmoothTime => _directionChangeSmoothTime;
        [SerializeField] LayerMask _agentLayer;

        [Space, Header("Debugging")]
        [SerializeField] bool _isDrawNeighborsLines;        

        float _maxSpeedSquared;
        #endregion 

        #region Execution
        void Awake()
        {
            _maxSpeedSquared = Mathf.Pow(_maxSpeed, 2);
            SpawnAgents();
        }

        void SpawnAgents()
        {
            for (int i=0; i < _agentsCount; i++)
            {
                Agent agent = Instantiate(
                    _agentPrefab,
                    Random.insideUnitSphere * _agentsCount * _agentsSpawnDensity,
                    Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))),
                    transform
                );
                agent.name = "Agent " + i.ToString();
                agent.Initialize(this);
                _agents.Add(agent);
            }
        }

        void Update()
        {
            MoveAgents();

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion

        void MoveAgents()
        {
            foreach (Agent agent in _agents)
            {
                List<Transform> neighbors = GetNeighbors(agent);

                if (_isDrawNeighborsLines)
                    foreach (Transform neighbor in neighbors)
                        Debug.DrawLine(agent.transform.position, neighbor.position);

                Vector3 moveLocation = _flockBehavior.CalculateMoveVector(agent, neighbors, this);
                moveLocation *= _speedMultiplier;
                bool isExceededMaxSpeed = moveLocation.sqrMagnitude > _maxSpeedSquared; 
                if (isExceededMaxSpeed)
                    moveLocation = moveLocation.normalized * _maxSpeed;

                if (moveLocation == Vector3.zero)
                    moveLocation += transform.up;
                agent.Move(moveLocation);
            }
        }

        public List<Transform> GetNeighbors(Agent agent)
        {
            List<Transform> neighbors = new List<Transform>();
            Collider[] hits = Physics.OverlapSphere(agent.transform.position, _neighborDetectionRadius, _agentLayer);
            foreach (Collider collider in hits)
            {
                if (collider != agent.Collider)
                    neighbors.Add(collider.gameObject.transform);
            }
            return neighbors;
        }
    }
}
