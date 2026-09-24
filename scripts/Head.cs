using Godot;
using System;

public partial class Head : Node3D
{
    [Export] public float MouseSensitivity = 0.002f;
    
    private Camera3D _camera;
    private float _pitch = 0.0f; // Explicitly track the up/down angle

    public override void _Ready()
    {
        _camera = GetNode<Camera3D>("Camera3D");
        Input.MouseMode = Input.MouseModeEnum.Captured;

        _pitch = _camera.Rotation.X; // Initialize pitch to the camera's current rotation
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            // Rotate the neck left and right normally
            RotateY(-mouseMotion.Relative.X * MouseSensitivity);

            // Accumulate the up/down movement into our pitch variable
            _pitch -= mouseMotion.Relative.Y * MouseSensitivity;
            
            // Clamp the pitch so the player cannot snap their neck backward
            _pitch = Mathf.Clamp(_pitch, Mathf.DegToRad(-70f), Mathf.DegToRad(70f));

            // Apply the pitch directly to the X axis. 
            // Setting Y and Z strictly to 0 prevents all tilting/rolling!
            _camera.Rotation = new Vector3(_pitch, 0, 0);
        }
    }
}