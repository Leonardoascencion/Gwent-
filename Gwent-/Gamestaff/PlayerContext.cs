public class Player
{
    public string Name { get; set; } = "Expected name of player";
    public int RoundsWins { get; set; } = 0;
    public bool Winner { get; set; } = false;
    public bool EndTurn { get; set; } = false;
    public bool Turn { get; set; } = false;
    public PlayerHand Hand { get; } = new PlayerHand();
    public Graveyard Grave { get; set; } = new Graveyard();
    public LiderZone Lider { get; set; } = new LiderZone();
    public CombatField CombatField { get; set; } = new CombatField();
    public Deck Deck { get; set; } = new();

    public Player() { }
    public Player(string name) => Name = name;

    public void ChangeName(string newname) => Name = newname;

    public List<Card> FieldofPlayer()
    {
        List<Card> field = new();

        field.Add(Lider.Lider);
        field.AddRange(CombatField.MeleeZone);
        field.AddRange(CombatField.RangedZone);
        field.AddRange(CombatField.SiegeZone);
        field.Add(CombatField.BuffMelee);
        field.Add(CombatField.BuffRanged);
        field.Add(CombatField.BuffSiege);

        return field;
    }

}
