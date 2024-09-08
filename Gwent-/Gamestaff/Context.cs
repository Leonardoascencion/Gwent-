public static class Context
{
    public static Player Player1 { get; set; } = new Player("Player1");
    public static Player Player2 { get; set; } = new Player("Player2");

    public static Tuple<int, int> ScoreBoard { get; set; } = new Tuple<int, int>(0, 0);
    public static bool EndGame { get; set; }
    public static Player Winner { get; set; } = new Player();


    /// <summary>
    /// Calcula de quien es el turno
    /// </summary>
    /// <returns>Devuelve el Player dueño del turno si ambos players coinciden se deja al azar (no debe coincidir)</returns>
    public static Player TriggerPlayer()
    {
        if (Player1.PlayersTurn && !Player2.PlayersTurn)
            return Player1;

        if (Player2.PlayersTurn && !Player1.PlayersTurn)
            return Player2;

        Random random = new();
        int n = random.Next(1, 3);

        if (n == 1)
        {
            Player1.PlayersTurn = true;
            Player2.PlayersTurn = false;
            return Player1;
        }

        Player2.PlayersTurn = true;
        Player1.PlayersTurn = false;
        return Player2;
    }

    public static List<Card> Board() => new Board(Player1, Player2).AllCards;
    public static List<Card> HandofPlayer(Player player) => player.Hand.Hand;
    public static List<Card> DeckofPlayer(Player player) => player.PlayerDeck.PlayerDeck;
    public static List<Card> GraveyardofPlayer(Player player) => player.Grave.DeadCards;
    public static List<Card> FieldofPlayer(Player player) => new Field(player).FieldCards;



}