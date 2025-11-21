using Godot;

public partial class Player : CharacterBody3D
{
    Camera3D playerCamera;
    private int _speed = 15;

    public override void _Ready()
    {
        playerCamera = (Camera3D)GetNode("PlayerCamera");
    }
    public void GetInput()
    {
        Vector3 inputVector = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.IsActionPressed("move_left"))
        {
            inputVector.X -= 1.0f;
        }
        if (Input.IsActionPressed("move_right"))
        {
            inputVector.X += 1.0f;
        }
        if (Input.IsActionPressed("move_forward"))
        {
            inputVector.Z -= 1.0f;
        }
        if (Input.IsActionPressed("move_backwards"))
        {
            inputVector.Z += 1.0f;
        }
        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            inputVector.Y += 1.0f;
        }

        inputVector = inputVector.Normalized();
        Godot.Basis basis = playerCamera.GlobalBasis;
        Vector3 movement = basis.X * inputVector.X + basis.Z * inputVector.Z;

        Velocity = new Vector3(movement.X * _speed, Velocity.Y, movement.Z * _speed);
    }
    public override void _PhysicsProcess(double delta)
    {
        ApplyGravity(delta);
        GetInput();
        MoveAndSlide();
    }

    private void ApplyGravity(double delta)
    {
        if (!IsOnFloor())
        {
            Position += new Vector3(0.0f, -5.0f * (float)delta, 0.0f);
        }
    }

}
