using UnityEngine;

namespace _Imported
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        #region Properties
        public enum AIBehaviour
        {
            Null,
            Patrol,
            Racing,
        }

        [SerializeField] private CircleArea m_Area;
        [SerializeField] private AIBehaviour m_AIBehaviour;

        [Header("Racing")]
        //[SerializeField] private RaceAITargetArea m_FirstRaceTarget;
        [Space(10)]

        [SerializeField] private AIPointPatrol m_PatrolPoint;

        [SerializeField] private Waypoint m_Waypoint;
        [Range(0.0f,  1.0f)]
        [SerializeField] protected float m_NavigationLinear;

        protected float _basicSpeed;

        [Range(0.0f,  1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;

        [SerializeField] private float m_FindNewTargetTime;

        [SerializeField] private float m_ShootDelay;

        [SerializeField] private float m_EvadeRayLenght;

        private const float MAX_ANGLE = 45.0F;

        private SpaceShip m_SpaceShip;

        private Vector3 m_MovePosition;
        private Vector3 m_RaceTargetPoint;

        private Destructible m_SelectTarget;

        private bool isEvade = false;

        private Timer _RandomizeDirectionTimer;
        private Timer _FireTimer;
        private Timer _FindNewTargetTimer;

        private Timer _EvadeTimer;
        [SerializeField] private float m_EvadeDelay;

        private Vector3 LeftRay;
        private Vector3 RightRay;
        #endregion

        #region UnityEvents
        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            InitTimers();
            /*
            if(m_AIBehaviour == AIBehaviour.Racing)
            {
                m_RaceTargetPoint = m_FirstRaceTarget.GetRandonTargetPoint();
            }
            */
            _basicSpeed = m_NavigationLinear;
        }

        private void FixedUpdate()
        {
            UpdateTimers();
        }
        private void Update()
        {
            UpdateAI();
        }
        #endregion

        #region Timers
        private void InitTimers()
        {
            _RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            _FindNewTargetTimer = new Timer(m_FindNewTargetTime);
            _FireTimer = new Timer(m_ShootDelay);
            _EvadeTimer = new Timer(m_EvadeDelay);
        }

        private void UpdateTimers()
        {
            _RandomizeDirectionTimer.RemoveTime(Time.fixedDeltaTime);
            _FireTimer.RemoveTime(Time.fixedDeltaTime);
            _FindNewTargetTimer.RemoveTime(Time.fixedDeltaTime);
            _EvadeTimer.RemoveTime(Time.fixedDeltaTime);
        }

        #endregion
    
        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = point;
        }

        public void SetWaypoint(Waypoint point)
        {
            m_Waypoint = point;
            m_MovePosition = m_Waypoint.transform.position;
        }

        public void SetNextRaceTarget(Vector2 target)
        {
            m_RaceTargetPoint = target;
        }
        private void UpdateAI()
        {
            switch(m_AIBehaviour)
            {
                case AIBehaviour.Patrol:
                    UpdateBehaviourPatrol();
                    break;

                case AIBehaviour.Racing:
                    UpdateBehaviourRacing();
                    break;
            } 
        }

        private void UpdateBehaviourPatrol()
        {
            ActionControlShip();
            ActionFindNewMovePosition();
            //ActionFindNewAttackTarget();
            //ActionFire();
            //ActionEvadeCollision();
        }

        private void UpdateBehaviourRacing()
        {
            ActionControlShip();
            ActionEvadeCollision();
            ActionFindNewMovePosition();
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = m_Area.Radius;

            Destructible potentialTarget = null;

            foreach(var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;
                if (v.TeamID == Destructible.TeamIDNeutral) continue;
                if (v.TeamID == m_SpaceShip.TeamID) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);
                if(dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        #region Actions

        private void ActionEvadeCollision()
        {
            LeftRay =  transform.TransformDirection(Quaternion.AngleAxis(15, transform.forward ) * transform.up) * m_EvadeRayLenght;
            RightRay =  transform.TransformDirection(Quaternion.AngleAxis(-15, transform.forward ) * transform.up) * m_EvadeRayLenght;
            
            if(Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLenght) == true)
            {
                isEvade = true;
                _EvadeTimer.Start(m_EvadeDelay);             
                m_MovePosition = transform.position + transform.right * 1.0f;
            }
            
            else
            {
                if(_EvadeTimer.IsFinished)
                {
                    isEvade = false;
                }
            }
        }

        private void ActionFindNewMovePosition()
        {
            if(isEvade == true) return;

            if(m_AIBehaviour == AIBehaviour.Patrol)
            {
                if(m_SelectTarget != null )
                {
                    m_MovePosition = m_SelectTarget.PositionPrediction();
                }
                else
                {
                    if(m_Waypoint != null)
                    {
                        m_MovePosition = m_Waypoint.transform.position;
                    }

                    else if(m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                        if(isInsidePatrolZone == true)
                        {
                            if(_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;
                                m_MovePosition = newPoint;
                                _RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
            if(m_AIBehaviour == AIBehaviour.Racing)
            {
                m_MovePosition = m_RaceTargetPoint;
            }
        }
        
        private void ActionControlShip()
        {
            m_SpaceShip.TrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAllignTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private void ActionFindNewAttackTarget()
        {
            if(_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectTarget = FindNearestDestructibleTarget();
                _FindNewTargetTimer.Start(m_FindNewTargetTime);
            }
        }
        
        private void ActionFire()
        {
            if(m_SelectTarget != null)
            {
                if(_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);
                    _FireTimer.Start(m_ShootDelay);
                }
            }
        }
        
        #endregion

        private static float ComputeAllignTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;
            return angle;
        }
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            //Ray ray = new Ray();
            //ray.origin = transform.position;
            //Vector3 d = Quaternion.AngleAxis(15, Vector3.forward * m_EvadeRayLenght) * transform.up;
            
            if(isEvade == true)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            //Gizmos.DrawRay(transform.position, transform.up * m_EvadeRayLenght);
            //Gizmos.DrawRay(transform.position, LeftRay );
            //Gizmos.DrawRay(transform.position, RightRay );
            Gizmos.DrawRay(transform.position,transform.up * m_EvadeRayLenght); 
        }
        #endif
    }
}