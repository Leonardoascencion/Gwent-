public static class Context
{
    public static Player Player1 { get; set; } = new Player("Player1");
    public static Player Player2 { get; set; } = new Player("Player2");
    public static bool Player1Turn { get; set; } = true;
    public static bool Player2Turn { get; set; } = false;
    public static Player RoundWinner { get; set; } = new Player();


    public static Tuple<int, int> ScoreBoard { get; set; } = new Tuple<int, int>(0, 0);
    public static bool EndGame { get; set; }
    public static Player GameWinner { get; set; } = new Player();


    /// <summary>
    /// Calcula de quien es el turno
    /// </summary>
    /// <returns>Devuelve el Player dueño del turno si ambos players coinciden se deja al azar (no debe coincidir)</returns>
    public static Player TriggerPlayer()
    {
        if (Player1Turn && !Player2Turn)
            return Player1;

        if (Player2Turn && !Player1Turn)
            return Player2;

        Random random = new();
        int n = random.Next(1, 3);

        if (n == 1)
        {
            Player1Turn = true;
            Player2Turn = false;
            return Player1;
        }

        Player2Turn = true;
        Player1Turn = false;
        return Player2;
    }

    /// <summary>
    /// Define el ganador comparando el total power de ambos jugadores y luego inicia un nuevo juego empezando el player q no gano
    /// </summary>
    public static void DefineRoundinner()
    {
        if (Player1.EndTurn && Player2.EndTurn)
            if (Player1.TotalPower() > Player2.TotalPower())
            {
                RoundWinner = Player1;
                int score1 = ScoreBoard.Item1;
                int score2 = ScoreBoard.Item1;
                ScoreBoard = new(score1 + 1, score2);
            }

            else
            if (Player1.TotalPower() < Player2.TotalPower())
            {
                int score1 = ScoreBoard.Item1;
                int score2 = ScoreBoard.Item1;
                ScoreBoard = new(score1, score2 + 1);
                RoundWinner = Player2;
            }
            else
            {
                int score1 = ScoreBoard.Item1;
                int score2 = ScoreBoard.Item1;
                ScoreBoard = new(score1 + 1, score2 + 1);
                RoundWinner = Player2;
            }

        Player1.Clear();
        Player2.Clear();

        if (RoundWinner == Player1)
        {
            Player1Turn = false;
            Player2Turn = true;
        }
        else
        {
            Player1Turn = true;
            Player2Turn = false;
        }


    }

    /// <summary>
    /// Define el ganador en cuanto algun lado del marcador llega a 2 puntos
    /// </summary>
    public static void DefineGameWinner()
    {
        if (ScoreBoard.Item1 == 2)
            GameWinner = Player1;

        if (ScoreBoard.Item2 == 2)
            GameWinner = Player2;
    }


    public static List<Card> Board() => new Board(Player1, Player2).AllCards;
    public static List<Card> HandofPlayer(Player player) => player.Hand.Hand;
    public static List<Card> DeckofPlayer(Player player) => player.Deck.PlayerDeck;
    public static List<Card> GraveyardofPlayer(Player player) => player.Grave.DeadCards;
    public static List<Card> FieldofPlayer(Player player) => new Field(player).FieldCards;



}