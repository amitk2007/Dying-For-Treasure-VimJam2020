using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerCanvasStateAtEnd : StateMachineBehaviour
{
    public PlayerCanvasAnimation endAnimation;
    public PlayerAnimationState endAnimation2;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (DoesAnimatorHaveParameter("PlayerCanvasState", animator))
            animator.SetInteger("PlayerCanvasState", PlayerCanvasAnimationManager.GetAnimationIndex(endAnimation));
        else if (DoesAnimatorHaveParameter("State", animator))
            animator.SetInteger("State", PlayerAnimationManager.GetAnimationIndex(endAnimation2));
    }

    public static bool DoesAnimatorHaveParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }
}
