using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;


public enum EnemyState
{
    Idle,
    Walk,
    Attack,
}

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 1f;
    public int damage = 5;
    public float calldownSec = 1;

    private CharacterController characterController;
    private Animator animator;
    private List<UnityEngine.AnimatorControllerParameter> animParams;

    private Vector3 currentTarget;
    private IDamagable currentTargetObj = null;
    private EnemyState state = EnemyState.Idle;
    private EnemyState oldState = EnemyState.Idle;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        StartCoroutine(FindIDamagable());
        animParams = animator.parameters.ToList();
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                SetAnim("idle");
                break;
            case EnemyState.Walk:
                SetAnim("walk");
                
                if (currentTargetObj != null)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, currentTarget);

                    if (distanceToTarget > 2f)
                    {
                        MoveToTarget();
                        SetState(EnemyState.Walk);
                        StopAttack();
                    }
                    else
                    {
                        SetState(EnemyState.Attack);
                    }
                }
                else
                {
                    SetState(EnemyState.Idle);
                }
                break;
            case EnemyState.Attack:
                SetAnim("attack");

                if (currentTargetObj != null)
                {
                    if (attackState == null)
                    {
                        attackState = StartCoroutine(Attack());
                    }
                }
                else
                {
                    StopAttack();
                    SetState(EnemyState.Idle);
                }
                break;
        }
    }

    private void MoveToTarget()
    {
        Vector3 moveDirection = (currentTarget - transform.position).normalized;
        moveDirection.y = 0;

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    IEnumerator FindIDamagable()
    {
        while (true)
        {
            var targets = FindObjectsOfType<MonoBehaviour>().OfType<IDamagable>().ToList();

            IDamagable nearestDamagable = null;
            float nearestDistance = Mathf.Infinity;
            foreach (IDamagable damagableObject in targets)
            {
                float distance = Vector3.Distance(transform.position, ((MonoBehaviour)damagableObject).transform.position);
                if (distance < nearestDistance)
                {
                    nearestDamagable = damagableObject;
                    nearestDistance = distance;
                }
            }

            if (nearestDamagable != null)
            {
                currentTarget = ((MonoBehaviour)nearestDamagable).transform.position;
                currentTargetObj = nearestDamagable;
                SetState(EnemyState.Walk);
            }
            else
            {
                currentTargetObj = null;
            }
            yield return new WaitForSeconds(1);
        }
    }

    Coroutine attackState = null;

    IEnumerator Attack()
    {
        while (true)
        {
            if (currentTargetObj != null)
            {
                currentTargetObj.TakeDamage(damage);
                yield return new WaitForSeconds(calldownSec);
            }
        }
    }

    void StopAttack()
    {
        if (attackState != null)
        {
            StopCoroutine(attackState);
            attackState = null;
        }
    }

    string lastAnimName = string.Empty;
    void SetAnim(string name)
    {
        if (lastAnimName != name)
        {
            foreach (var param in animParams)
            {
                if (param.type == UnityEngine.AnimatorControllerParameterType.Bool && param.name != name)
                {
                    animator.SetBool(param.name, false);
                }
            }
            animator.SetBool(name, true);
            lastAnimName = name;
        }
    }

    void SetState(EnemyState newState)
    {
        oldState = state;
        state = newState;
    }

}
