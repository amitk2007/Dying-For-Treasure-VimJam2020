using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderColliderScript : MonoBehaviour
{
    //public bool IsCollidingWithLadder = false;

    ////Using fixedUpdate because it's called before onTriggerEnter2D, unlike Update which is called after
    //private void FixedUpdate()
    //{
    //    IsCollidingWithLadder = false;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!IsCollidingWithLadder)
    //        IsCollidingWithLadder = (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"));
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (!IsCollidingWithLadder)
    //        IsCollidingWithLadder = (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"));
    //}
}
