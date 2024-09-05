public static class Context
{
    public static Player Player1 { get; set; } = new Player("Player1");
    public static Player Player2 { get; set; } = new Player("Player2");

    public static Tuple<int, int> ScoreBoard { get; set; } = new Tuple<int, int>(0, 0);
    public static bool EndGame { get; set; }
    public static Player Winner { get; set; } = new Player();

}