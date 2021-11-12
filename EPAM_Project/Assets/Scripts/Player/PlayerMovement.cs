using Extensions;
using Services;
using Stats;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent), typeof(PlayerStatLoader))]
    public class PlayerMovement : MonoBehaviour, ITarget
    {
        private NavMeshAgent agent;
        private InputManager inputManager;
        private GameManager gameManager;

        private Vector3 mousePos;
        private Vector2 input;
        private Stat<float> speedStat;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            speedStat = GetComponent<PlayerStatLoader>().Stats.Speed;
            gameManager = ServiceLocator.Instance.Get<GameManager>();
            
            inputManager = ServiceLocator.Instance.Get<InputManager>();
            inputManager.MouseMoved += ChangeMousePos;
            inputManager.WasdInput += ChangePlayerPos;
        }

        private void ChangeMousePos(Vector3 newMousePos)
        {
            mousePos = newMousePos;
        }
        private void ChangePlayerPos(Vector2 newInput)
        {
            input = newInput;
        }
        
        private void Update()
        {
            if (gameManager.State != GameState.Default)
            {
                agent.velocity = Vector3.zero;
                return;
            }
            
            Move();
            Rotate();
        }

        private void Move()
        {
            var dir = input.normalized;
            agent.velocity = speedStat.Value * dir.ToVector3();
        }

        private void Rotate()
        {
            var dirToMouse = mousePos - transform.position;
            agent.transform.rotation = dirToMouse.ToRotation();
        }

        public Vector3 Position => transform.position;
        public GameObject GameObject => gameObject;
    }
}