public class Player
{
    public string Name { get; set; } = "Expected name of player";
    public int RoundsWins { get; set; } = 0;
    public bool Winner { get; set; } = false;
    public bool EndTurn { get; set; } = false;
    public PlayerHand Hand { get; set; } = new PlayerHand();
    public Graveyard Grave { get; set; } = new Graveyard();
    public LiderZone Lider { get; set; } = new LiderZone();
    public CombatField CombatField { get; set; } = new CombatField();
    public Deck Deck { get; set; } = new();

    public Player() { }
    public Player(string name) => Name = name;

    public void ChangeName(string newname) => Name = newname;

    /// <summary>
    /// Determina la suma total de todos los puntos de poder de las cartas del campo del jugador
    /// </summary>
    /// <returns></returns>
    public double TotalPower()
    {
        double totalpower = 0;

        foreach (var card in FieldofPlayer())
            totalpower += card.Power;

        return totalpower;
    }

    /// <summary>
    /// Crea una lista con todas las cartas q estan en el campo del jugador, no se incluyen ni la mano ni el deck ni el cementerio
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Envia todas las cartas del jugador a su cementerio
    /// </summary>
    public void Clear() => Grave.DeadCards.AddRange(FieldofPlayer());

}
