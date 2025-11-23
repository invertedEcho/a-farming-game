using Godot;

public partial class World : Node3D
{
    Node3D dirtPatch;

    public override void _Ready()
    {
        dirtPatch = GetNode<Node3D>("/root/World/DirtPatch");
        dirtPatch.Connect("PlantFullyGrown", new Callable(this, MethodName.HandlePlantFullyGrownSignal));
    }
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("escape"))
        {
            if (Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
            }
            else
            {
                Input.MouseMode = Input.MouseModeEnum.Captured;
            }
        }
    }

    private void HandlePlantFullyGrownSignal()
    {
        PackedScene npcScene = GD.Load<PackedScene>("res://assets/scenes/npc.tscn");
        Node3D instantiatedScene = npcScene.Instantiate<Node3D>();
        // instantiatedScene.Position = new Vector3();
        AddChild(instantiatedScene);
    }
}
