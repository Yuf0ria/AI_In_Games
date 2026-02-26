//0Speed
//StayStopped if condition is red
//Switch to slowdown or go
public class CarGoState : ICarState
{
    private readonly CarAI ai; 
    private readonly StateMachine sm;
    
    public CarGoState(CarAI ai, StateMachine sm){
        this.ai = ai;
        this.sm = sm;
    }
    public void Enter(ICarState next)
    {
        
    }

    public void Exit()
    {
        
    }

    public void Enter()
    {
        
    }

    public void Tick()
    {
        bool red = ai.ActiveTrafficLight != null && ai.ActiveTrafficLight.isRed;
        if (red || ai.CarAheadStoppedClose)
        {
            sm.Change(ai.CarStopState); return;
        }
        bool orange = ai.ActiveTrafficLight != null && ai.ActiveTrafficLight.isOrange;
        if (orange || ai.CarAheadDetected)
        {
            sm.Change(ai.CarSlowState); return;
        }
        ai.SetTargetSpeed(ai.goSpeed);
    }
}