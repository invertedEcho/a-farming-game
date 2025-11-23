using Godot;

public partial class GameManager : Node
{
    public static GameManager Instance;

    public enum GameObjective
    {
        GrowFirstPlant,
        SellFirstPlant
    }

    public GameObjective CurrentObjective { get; set; } = GameObjective.GrowFirstPlant;
    public int CoinCount { get; set; } = 0;

    public override void _Ready()
    {
        Instance = this;
    }

    public void UpdateObjective(GameObjective objective)
    {
        CurrentObjective = objective;
        string newObjectiveText = GetCurrentObjectiveDescription(objective);

        UiManager.Instance.CurrentObjectiveLabel.Text = newObjectiveText;
    }

    private static string GetCurrentObjectiveDescription(GameManager.GameObjective currentGameObjective)
    {
        string currentObjectiveText = "Current Objective: ";
        switch (currentGameObjective)
        {
            case GameManager.GameObjective.GrowFirstPlant:
                currentObjectiveText += "Grow your first cactus!";
                break;
            case GameManager.GameObjective.SellFirstPlant:
                currentObjectiveText += "Sell your first cactus!";
                break;
        }
        return currentObjectiveText;
    }

    public void AddCoin(int toAdd)
    {
        this.CoinCount += toAdd;
        UiManager.Instance.CoinsLabel.Text = CoinCount.ToString();
    }
}
