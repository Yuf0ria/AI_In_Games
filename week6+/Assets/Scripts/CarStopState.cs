//0Speed
//StayStopped if condition is red
//Switch to slowdown or go
public class CarStopState : ICarState
{
    private readonly CarAI ai; 
    private readonly StateMachine sm;
    
    public CarStopState(CarAI ai, StateMachine sm){
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
        ai.SetTargetSpeed(0);
        //ligth is only consideref in traffic light is not null
        bool red = ai.ActiveTrafficLight != null && ai.ActiveTrafficLight.isRed;
        //if red light or front is stopped, keep stopping
        if(red || ai.CarAheadStoppedClose) return;
        //if orange light while near intersection, slow down
        bool orange = ai.ActiveTrafficLight != null && ai.ActiveTrafficLight.isOrange;
        //if orange or there is car ahead, this slows down
        if (orange || ai.CarAheadDetected)
        {
            sm.Change(ai.CarSlowState);
        }
        else//safe to go
        {
            sm.Change(ai.CarGoState);
        }
    }
}
