using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public ICarState CurrentCarState { get; private set; }

    public void Chamge(ICarState next)
    {
        if (next == null || next == CurrentCarState)
        CurrentCarState?.Exit();
        CurrentCarState = next;
        CurrentCarState.Enter();
    }

    public void Tick()
    {
        CurrentCarState?.Tick();
    }
}
