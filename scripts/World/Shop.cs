using System;
using Godot;

namespace agame.scripts.World;

public partial class Shop : Node3D {
    private bool _playerInRange;

    [Export]
    private Area3D _area3D;

    private Player.Player _player;

    public override void _Ready() {
        // hmm i dont know how i feel about this, maybe better way to access player inventory?
        _player = GetNode<Player.Player>("/root/World/Player");
        _area3D = GetNode<Area3D>("Area3D");

        // TODO: only connect this once we have reached GameObjective.SellFirstPlant
        _area3D.BodyEntered += OnBodyEntered;
        _area3D.BodyExited += OnBodyExit;
    }

    private void OnBodyEntered(Node3D body) {
        if (!body.IsInGroup("player")) return;

        GD.Print("player has entered shop range!");
        if (GameManager.Instance.CurrentObjective == GameManager.GameObjective.GrowFirstPlant) {
            GD.Print("current objective is growfirstplant, ignoring player entered shop range");
            return;
        }

        UiManager.Instance.InteractLabel.Text = $"Press (F) to sell your Cactus for {GameConstants.CactusSellPrize} coins!";
        _playerInRange = true;
        UiManager.Instance.InteractLabel.Visible = true;
    }

    public void OnBodyExit(Node3D body) {
        if (!body.IsInGroup("player")) return;

        GD.Print("player has exited shop range!");
        _playerInRange = false;
        UiManager.Instance.InteractLabel.Visible = false;
    }

    public override void _Process(double delta) {
        HandleInteractionWithShop();
    }

    private void HandleInteractionWithShop() {
        var interactActionJustPressed = Input.IsActionJustPressed("interact");

        if (!(_playerInRange && interactActionJustPressed)) return;

        switch (GameManager.Instance.CurrentObjective) {
            case GameManager.GameObjective.GrowFirstPlant:
                break;
            case GameManager.GameObjective.SellFirstPlant:
                var inventoryItem = _player.GetInventoryItemByType(GameItem.GameItemType.Plant);
                if (inventoryItem is (PlantItem plantItem, var index)) {
                    _player.RemoveItemFromInventory(index);
                    Player.Player.Instance.AddCoin(plantItem.SellPrice);
                    GameManager.Instance.UpdateObjective(GameManager.GameObjective.BuyFirstPlot);
                    GD.Print("Player has sold plant");
                }
                break;
            case GameManager.GameObjective.BuyFirstPlot:
                if (Player.Player.Instance.CoinCount < GameConstants.PlotPrize) {
                    GD.Print("player doesnt have enough money to buy their first plot!");
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
