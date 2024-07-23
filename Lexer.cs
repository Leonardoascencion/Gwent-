public class Lexer
{
    public string Source;
    public List<Token> tokens = new();
    public int StateOfLexer;
    public int Postion;
    public Lexer(string Source)
    {
        this.Source = Source;
    }
}