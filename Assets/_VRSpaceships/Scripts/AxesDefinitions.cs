public class AxesDefinitions
{
    public static string RightThrottleAxis;
    public static string LeftThrottleAxis;
    public static string ThirdHandleAxis;
    public static string YokeTurn;
    public static string YokePull;
    public static string CameraVerical;
    public static string CameraHorizontal;
    public static string Fire;

    public static void ChangeController(PlayerInput.ControlMode controlMode)
    {
        switch (controlMode)
        {
            case PlayerInput.ControlMode.GAMEPAD:
                AxesDefinitions.RightThrottleAxis = "RightThrottle";
                AxesDefinitions.LeftThrottleAxis = "LeftThrottle";
                AxesDefinitions.YokeTurn = "LeftAnalogHorizontal";
                AxesDefinitions.YokePull = "LeftAnalogVertical";
                AxesDefinitions.CameraHorizontal = "RightAnalogHorizontal";
                AxesDefinitions.CameraVerical = "RightAnalogVertical";
                AxesDefinitions.Fire = "Fire1";
                AxesDefinitions.ThirdHandleAxis = "";
                break;
            case PlayerInput.ControlMode.WOLANT:
                AxesDefinitions.RightThrottleAxis = "RightThrottleYoke";
                AxesDefinitions.LeftThrottleAxis = "LeftThrottleYoke";
                AxesDefinitions.YokeTurn = "YokeTurn";
                AxesDefinitions.YokePull = "YokePull";
                AxesDefinitions.CameraHorizontal = "";
                AxesDefinitions.CameraVerical = "";
                AxesDefinitions.Fire = "FireYoke";
                AxesDefinitions.ThirdHandleAxis = "ThirdLever";
                break;
            default:
                break;
        }
    }
}