using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingArtifactScript : MonoBehaviour
{
    [SerializeField] private float AttackingIntervals = 4.5f;
    [SerializeField] private float MaxNormalStepSpeed = 10f;// This value is 10f per second, we will use it in practice multiplied by time.DeltaTime
    private Transform playerTarget;
    private float attackingTimer = 0f;
    private bool attacking = false;

    private void Start()
    {
        attackingTimer = AttackingIntervals;
    }

    public void SetTarget(Transform transform)
    {
        playerTarget = transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (attackingTimer == 0f)
            {
                //Setup for attacking
                attacking = true;
            }
            else
            {
                //This is the standard condition - just following the player
                Vector2 targetPos = GetCirclePoint();
                transform.position = Vector2.MoveTowards(transform.position, targetPos, MaxNormalStepSpeed * Time.deltaTime);
            }
        }
        else
        {
            //Attacking logic

            //At the end, reset attacking status
            attacking = false;
            attackingTimer = AttackingIntervals;
        }
    }

    private Vector2 GetCirclePoint()
    {

    }
}
