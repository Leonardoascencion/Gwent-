public class Player
{
    public string Name { get; set; } = "Expected name of player";
    public int RoundsWins { get; set; } = 0;
    public bool Winner { get; set; } = false;
    public bool EndTurn { get; set; } = false;
    public bool PlayersTurn { get; set; } = false;
    public PlayerHand Hand { get; } = new PlayerHand();
    public Graveyard Grave { get; set; } = new Graveyard();
    public LiderZone LiderOfPlayer { get; set; } = new LiderZone();
    public CombatField PlayerField { get; set; } = new CombatField();
    public Deck PlayerDeck { get; set; } = new();

    public Player() { }
    public Player(string name) => Name = name;

    public void ChangeName(string newname) => Name = newname;

}
