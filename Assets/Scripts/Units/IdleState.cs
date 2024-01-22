using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSMState
{
    public IdleState(FSMState fsm) : base(fsm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

    }

}
