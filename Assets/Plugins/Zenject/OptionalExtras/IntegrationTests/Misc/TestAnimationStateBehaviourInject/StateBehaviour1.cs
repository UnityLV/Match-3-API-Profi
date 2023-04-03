using UnityEngine;


public class StateBehaviour : StateMachineBehaviour
{
    public static int OnStateEnterCalls;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnStateEnterCalls++;
    }
}

