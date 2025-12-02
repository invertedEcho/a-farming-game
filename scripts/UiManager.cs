using agame.scripts.Player;
using Godot;
using Godot.Collections;

public partial class UiManager : Control {
    public static UiManager Instance;

    GameManager ObjectiveManager;

    [Export]
    public Label CurrentObjectiveLabel { get; set; }

    [Export]
    public Label InteractLabel { get; set; }

    [Export]
    public Label CoinsLabel { get; set; }

    [Export]
    public Array<TextureRect> InventorySlotTextures = [];

    public override void _Ready() {
        Instance = this;
        CurrentObjectiveLabel.Text = GameManager.GetCurrentObjectiveDescription(GameManager.Instance.CurrentObjective);

        for (int i = 0; i < Player.InventorySize; i++) {
            InventorySlotTextures.Add(GetNode<TextureRect>($"/root/World/UiRoot/HotbarInventory/InventorySlot{i + 1}"));
        }
    }
}
