using Godot;

public partial class Porsche : VehicleBody3D
{
	// Adjust these values in the Godot Inspector to change how the car feels
	[Export] public float HorsePower = 500.0f;
	[Export] public float MaxSteer = 0.4f;
	[Export] public float SteeringSpeed = 2.5f;

	public override void _PhysicsProcess(double delta)
	{
		// Handle Steering (Left/Right)
		// Ensure you have "ui_left" and "ui_right" mapped in your Input Map
		float steerInput = Input.GetAxis("ui_right", "ui_left");
		
		// MoveToward smoothly interpolates the steering so it doesn't snap instantly
		Steering = Mathf.MoveToward(Steering, steerInput * MaxSteer, (float)delta * SteeringSpeed);

		// Handle Acceleration (Forward/Backward)
		// Ensure you have "ui_up" (accelerate) and "ui_down" (brake/reverse) mapped
		float driveInput = Input.GetAxis("ui_down", "ui_up");
		EngineForce = driveInput * HorsePower;
	}
}
