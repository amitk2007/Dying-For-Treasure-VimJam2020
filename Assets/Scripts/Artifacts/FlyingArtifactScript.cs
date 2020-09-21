using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingArtifactScript : MonoBehaviour
{
    [SerializeField] private float AttackingIntervals = 4.5f;
    [SerializeField] private float MaxNormalStepSpeed = 10f;// This value is 10f per second, we will use it in practice multiplied by time.DeltaTime
    [SerializeField] private float MaxAttackStepSpeed = 30f;
    [SerializeField] private float OrbitRadius = 5f;
    [SerializeField] private float OrbitSafezone = 0.5f; //This is the range that even if the artifact is not exactly on orbit radius, it can prepare for attack
    [SerializeField] private float OrbitFrequency = 1 / 6f; //This is the frequency in which a single circle is completed around the target
    [SerializeField] private int Damage = 2;
    [SerializeField] private float PushForce = 200f;
    [SerializeField] private SpriteRenderer targetLineSpritePrefab;
    private float orbitAngle = 0f; //This angle grows as enemy rotates
    private Transform playerTarget;
    private float attackingTimer = 0f;
    private bool attacking = false;

    private void Start()
    {
        attackingTimer = AttackingIntervals;
    }

    public void SetTarget(Transform inputTransform)
    {
        playerTarget = inputTransform;
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
                attackingTimer = 1f;
                CreateAttackLine();
            }
            else
            {
                //This is the standard condition - just following the player
                Vector2 targetPos = GetCirclePoint();
                transform.position = Vector2.MoveTowards(transform.position, targetPos, MaxNormalStepSpeed * Time.deltaTime);

                //Check if we can lower the attacking timer
                if ((transform.position - playerTarget.position).magnitude <= OrbitRadius + OrbitSafezone)
                {
                    attackingTimer = Mathf.Clamp(attackingTimer - Time.deltaTime, 0f, AttackingIntervals);
                }
            }
        }
        else
        {
            //Attacking logic
            if (attackingTimer == 0f)
            {
                if (attackTarget == Vector2.zero)
                {
                    attackTarget = GetOppositeCirclePoint();
                    DestroyAttackLine();
                }
                transform.position = Vector2.MoveTowards(transform.position, attackTarget, MaxAttackStepSpeed * Time.deltaTime);
                if (transform.position.x == attackTarget.x && transform.position.y == attackTarget.y)
                {
                    //At the end, reset attacking status
                    attacking = false;
                    attackTarget = Vector2.zero;
                    attackingTimer = AttackingIntervals;
                }
            }
            else
                attackingTimer = Mathf.Clamp(attackingTimer - Time.deltaTime, 0f, AttackingIntervals);
        }
    }

    private Vector2 attackTarget;
    private GameObject attackLine;

    private Vector2 GetCirclePoint()
    {
        orbitAngle = (orbitAngle + Time.deltaTime * OrbitFrequency) % 6;
        //Debug.Log("Orbit angle: " + orbitAngle.ToString("0.0"));
        return GetStationaryCirclePoint();
    }

    private Vector2 GetOppositeCirclePoint()
    {
        orbitAngle = (orbitAngle + 3f) % 6f;
        return GetStationaryCirclePoint();
    }

    private Vector2 GetStationaryCirclePoint()
    {
        float x = Mathf.Cos(orbitAngle) * OrbitRadius + playerTarget.position.x;
        float y = Mathf.Sin(orbitAngle) * OrbitRadius + playerTarget.position.y;
        //float z = playerTarget.position.z;
        return new Vector2(x, y);
    }

    private void CreateAttackLine()
    {
        attackLine = (Instantiate(targetLineSpritePrefab.gameObject, playerTarget.position, Quaternion.LookRotation(Vector3.forward, ((Vector2)playerTarget.position - GetStationaryCirclePoint())))) as GameObject;
    }

    private void DestroyAttackLine()
    {
        Destroy(attackLine);
        attackLine = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attacking)
        {
            if (collision.tag == "Player")
            {
                //When colliding with a player:
                // 1. Damage player
                // 2. Knock him back
                collision.GetComponent<PlayerHealth>().TakeDamage(Damage);
                collision.GetComponent<Rigidbody2D>().AddForce((collision.gameObject.transform.position - transform.position) * PushForce);
            }
        }
    }
}
