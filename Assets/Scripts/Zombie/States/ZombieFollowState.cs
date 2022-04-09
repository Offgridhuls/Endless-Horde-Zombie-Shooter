using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollowState : ZombieStates
{
    GameObject followTarget;
    const float stoppingDistance = 1;
    int movementZhash = Animator.StringToHash("MovementZ");
    public ZombieFollowState(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine zombieStateMachine) : base(zombie, zombieStateMachine)
    {
        followTarget = _followTarget;
        UpdateInterval = 2;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavMeshAgent.SetDestination(followTarget.transform.position);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        ownerZombie.zombieNavMeshAgent.SetDestination(followTarget.transform.position);
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        float moveZ = ownerZombie.zombieNavMeshAgent.velocity.normalized.z != 0f ? 1f : 0f;
        ownerZombie.zombieAnimator.SetFloat(movementZhash, 1);

        //Debug.Log(moveZ);

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if(distanceBetween < stoppingDistance)
        {
            stateMachine.ChangeState(ZombieStateType.Attacking);
        }
    }
}
