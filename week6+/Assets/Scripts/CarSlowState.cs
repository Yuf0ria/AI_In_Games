//0Speed
//StayStopped if condition is red
//Switch to slowdown or go
public class CarSlowState : ICarState
{
    private readonly CarAI ai; 
    private readonly StateMachine sm;
    
    public CarSlowState(CarAI ai, StateMachine sm){
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
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        bool red = ai.ActiveTrafficLight != null && ai.ActiveTrafficLight.isRed;
        if (red || ai.CarAheadStoppedClose)
        {
            sm.Change(ai.CarStopState); return;
        }

        ai.SetTargetSpeed(ai.goSpeed);
        
        bool green = ai.ActiveTrafficLight != null && ai.ActiveTrafficLight.isGreen;
        if (green || ai.CarAheadDetected)
        {
            sm.Change(ai.CarGoState); return;
        }
    }
}