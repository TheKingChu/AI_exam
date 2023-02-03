/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */


public abstract class BaseState
{
    protected BaseState context;
    BaseState(newFSM currentContext)
    {
        //context = currentContext;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();

    protected void UpdateStates() { }
    protected void SwitchState(BaseState newState) 
    {
        //from current state it exits that state
        ExitState();

        //we then enter a new state, calling the new state
        newState.EnterState();

        //context.CurrentState = newState;
    }
}
