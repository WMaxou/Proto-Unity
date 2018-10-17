using Valve.VR;

public interface IGrabeable
{
    void Grab(SteamVR_Input_Sources source, ControllerVR controller);
    void Release(SteamVR_Input_Sources source, ControllerVR controller);
}
