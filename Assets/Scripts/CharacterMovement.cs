using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 1f;

    private CharacterController characterController;
    private Animator animator;
    private Coroutine currentMovementCoroutine;

    private Vector3 currentTarget;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        StartCoroutine(FindIDamagable());
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget);
        float rotation = Vector3.Angle(transform.position, currentTarget);

        if (distanceToTarget > 2f)
        {   
            Vector3 moveDirection = (currentTarget - transform.position).normalized;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            animator.SetTrigger("run");
        }
        else
        {
            animator.SetTrigger("idle");
        }
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
            }
            yield return new WaitForSeconds(1);
        }
    }
}
