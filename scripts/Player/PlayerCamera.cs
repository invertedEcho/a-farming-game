using Godot;

public partial class PlayerCamera : Camera3D
{
    private float pitch = 0f;
    private const float Sens = 0.002f;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion m && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            // yaw
            RotateY(-m.Relative.X * Sens);

            // pitch
            pitch -= m.Relative.Y * Sens;
            pitch = Mathf.Clamp(pitch, Mathf.DegToRad(-89), Mathf.DegToRad(89));

            Rotation = new Vector3(pitch, Rotation.Y, 0);
        }
    }
}
