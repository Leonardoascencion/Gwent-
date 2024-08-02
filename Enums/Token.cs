namespace TokenClass
{

    public class Token
    {
        public TokenType Type { get; set; } //Type for the parse

        public string Lexeme { get; set; } = string.Empty; //Value of the Source

        public object? TypeObject { get; set; }//Type of object of the Source

        public Token(TokenType type, string lexeme)
        {
            Type = type;
            Lexeme = lexeme;
        }



    }
}