using Godot;

public partial class Npc : MeshInstance3D
{
    CharacterBody3D player;
    bool playerInRange;

    public override void _Ready()
    {
        player = (CharacterBody3D)GetNode("/root/World/Player");

        UiManager.Instance.InteractLabel.Text = $"Press (F) to sell the cactus for {GameConstants.CactusPrize} coins";

        this.Mesh = (Mesh)GD.Load("res://assets/models/characters/character-c.obj");
    }

    public override void _Process(double delta)
    {
        Vector3 positionOfPlayer = player.Position;
        Vector3 positionOfDirtPatch = Position;
        float distanceToPlayer = Position.DistanceTo(positionOfPlayer);

        if (distanceToPlayer < 7f)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        UiManager.Instance.InteractLabel.Visible = playerInRange;
        InteractWithNpc();
    }

    private void InteractWithNpc()
    {
        if (!(playerInRange && Input.IsActionJustPressed("interact"))) return;
        GD.Print("player interacting with npc");
        GameManager.Instance.AddCoin(GameConstants.CactusPrize);
    }
}
