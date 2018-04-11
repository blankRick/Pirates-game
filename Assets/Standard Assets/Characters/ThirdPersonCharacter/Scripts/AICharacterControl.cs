using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        public Transform[] waypoints;
        private int wayPointInd = 0;
        private Animator anim;
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            anim = GetComponent<Animator>();
	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);
            else
            {
                agent.SetDestination(waypoints[wayPointInd % waypoints.Length].position);
            }
            if (target != null)
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    //anim.SetBool("playerMet", false);
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    //anim.SetBool("playerMet", true);
                    character.Move(Vector3.zero, false, false);
                }
            else
            {
                //anim.SetBool("playerMet", false);
                if (agent.remainingDistance <= 3)
                {
                    character.Move(Vector3.zero, false, false);
                }
                else
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
