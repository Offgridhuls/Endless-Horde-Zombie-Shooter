using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
    GameObject followTarget;
    float attackRange = 2;

    private IDamagable damagableObject;

    int movementZhash = Animator.StringToHash("MoveZ");
    int isAttackingHash = Animator.StringToHash("isAttacking");
    // Start is called before the first frame update

    public ZombieAttackState(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        followTarget = _followTarget;
        UpdateInterval = 2;
        damagableObject = followTarget.GetComponent<IDamagable>();
    }
    public override void Start()
    {
        //base.Start();
        ownerZombie.zombieNavMeshAgent.isStopped = true;
        ownerZombie.zombieNavMeshAgent.ResetPath();
        ownerZombie.zombieAnimator.SetFloat(movementZhash, 0);
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, true);
    }
    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        damagableObject?.TakeDamage(ownerZombie.zombieDamage);
    }
    // Update is called once per frame
    public override void Update()
    {
        ownerZombie.transform.LookAt(followTarget.transform.position, Vector3.up);

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBetween > attackRange)
        {
            stateMachine.ChangeState(ZombieStateType.Following);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavMeshAgent.isStopped = false;
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, false);
    }
}
