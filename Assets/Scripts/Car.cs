using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider backLeftCollider;
    [SerializeField] private WheelCollider backRightCollider;

    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform backLeftTransform;
    [SerializeField] private Transform backRightTransform;
    
    [SerializeField] private GameObject backLight;

    private float motorForce = 1000f;
    private float breakForce = 1500f;
    private float maxSteerAngle = 30f;
    
    private float currentSteerAngle;
    private float horizontalInput;
    private float verticalInput;
    private float currentBreakForce;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private bool isBreaking;
    private bool isCanvasButtonPressed;
    private bool soundHasBeenPlayed;

    public bool SoundHasBeenPlayed => soundHasBeenPlayed;

    private void Start()
    {
        soundHasBeenPlayed = false;
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(backLeftCollider, backLeftTransform);
        UpdateSingleWheel(backRightCollider, backRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftCollider.steerAngle = currentSteerAngle;
        frontRightCollider.steerAngle = currentSteerAngle;
    }

    private void HandleMotor()
    {
        frontLeftCollider.motorTorque = verticalInput * motorForce;
        frontRightCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0;

        ApplyBreaking();
        
        if (verticalInput < 0) // Если движемся назад
        {
            backLight.SetActive(true);
            soundHasBeenPlayed = false;

        }
        else if (verticalInput > 0) // Если движемся вперед
        {
            backLight.SetActive(false);
            soundHasBeenPlayed = true;
        }
        else
        {
            soundHasBeenPlayed = false;
        }
    }

    private void ApplyBreaking()
    {
        frontLeftCollider.brakeTorque = currentBreakForce;
        frontRightCollider.brakeTorque = currentBreakForce;
        backRightCollider.brakeTorque = currentBreakForce;
        backLeftCollider.brakeTorque = currentBreakForce;
    }

    private void GetInput()
    {
        horizontalInput = SimpleInput.GetAxis(HORIZONTAL);
        verticalInput = SimpleInput.GetAxis(VERTICAL);
        isBreaking = isCanvasButtonPressed;
    }
    
    public void OnCanvasButtonPressed()
    {
        isCanvasButtonPressed = true;
        backLight.SetActive(true);
        soundHasBeenPlayed = false;
    }
    public void OnCanvasButtonReleased()
    {
        isCanvasButtonPressed = false;
        backLight.SetActive(false);
    }
} 