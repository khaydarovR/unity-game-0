using System.Collections;

public abstract class FSMState
{
    protected FSMState Fsm;

    public FSMState(FSMState fsm)
    {
        Fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
