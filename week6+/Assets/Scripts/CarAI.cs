using UnityEngine;

public class CarAI : MonoBehaviour
{
    #region Speeds
        public float goSpeed,
            slowSpeed,
            accelerationSpeed,
            brake;
    #endregion
    #region Sensor
        public float frontCheckingDistance,
            stopDistance;
        public LayerMask carLayer;
    #endregion
    public TrafficLight ActiveTrafficLight { get; private set; }
    public float currentSpeed{get; private set;}
    public bool CarAheadDetected { get; private set; }//raycast bool
    public bool CarAheadStoppedClose { get; private set; }

    #region Instances
    public CarGoState CarGoState { get; private set; }
    public CarStopState CarStopState { get; private set; }
    public CarSlowState CarSlowState { get; private set; }
    private StateMachine  sm;

    #endregion

    public void Awake()
    {
        sm = new StateMachine();
        CarSlowState = new CarSlowState(this, sm);
        CarStopState = new CarStopState(this, sm);
        CarGoState = new CarGoState(this, sm);
    }

    private void Start()
    {
        sm.Change(CarStopState);
    }

    private void Update()
    {
        UpdateSensor();
        sm.Tick();
        MoveForward();
    }

    void MoveForward()
    {
        transform.position += transform.forward * (currentSpeed * Time.deltaTime);
    }

    private void UpdateSensor()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        
        CarAheadDetected = false;
        CarAheadStoppedClose = false;
        Vector3 direction = Vector3.forward;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, frontCheckingDistance, carLayer))
        {
            //hits the carlayer
            CarAheadDetected = true;
            CarAI other = hit.collider.GetComponent<CarAI>();
            //if other exists, read.speed. else treat it as 0
            float otherSpeed = other!=null ? other.currentSpeed : 0;
            //Consider other car is stopped if speed is almost zero
            bool otherStopped = otherSpeed <= 0.1f;
            //Consider "veryclose"
            bool veryClose = hit.distance > stopDistance;
            //if the other car is stopped and close, we must stop car too
            CarAheadStoppedClose  = otherStopped && veryClose;
        }
    }

    public void SetTargetSpeed(float speed)
    {
        //target is higher than current -> accel
        //if target is slower then break
        float rate =(speed > currentSpeed) ? accelerationSpeed : brake;
        //MoveTowards make smoothe changes, without overshoots
        currentSpeed = Mathf.MoveTowards(currentSpeed, speed, rate *  Time.deltaTime);
        
    }

    public void SetActiveTrafficLight(TrafficLight light)
    {
        ActiveTrafficLight = light;
    }
    
    //called when exits intersection
    public void ClearActiveTrafficLight(TrafficLight light)
    {
        if(ActiveTrafficLight == GetComponent<Light>())
            ActiveTrafficLight = null;
    }
}
