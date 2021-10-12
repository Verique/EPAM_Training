using System;
using Cam;
using Services;
using UnityEngine;

public class UserInputController : IService
{
    public static Vector3? GetPointerPositionInWorld =>
        Physics.Raycast(ServiceLocator.Instance.Get<CameraController>().Cam.ScreenPointToRay(Input.mousePosition), out var hit)
            ? hit.point
            : (Vector3?) null;
       
    public event Action<Vector3?> MouseMoved;
    public event Action<Vector2> WasdInput;

    public void ProcessInput()
    {
        MouseMoved?.Invoke(GetPointerPositionInWorld);

        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
        if (input != Vector2.zero)
            WasdInput?.Invoke(input);
    }
}