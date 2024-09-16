///AKI VA TODO LO Q VIENE SIENDO EJECUTAR EL CODIGO
string errormesage = "";
try
{
    Lexer lexer = new(File.ReadAllText(@"tester.txt"));
    System.Console.WriteLine("empieza el parser");
    Parser parser = new Parser();
    if (parser.actionfortest is BinaryAction binaryAction)
        Console.WriteLine(binaryAction.Operator.Lexeme);
    System.Console.WriteLine("termino el parser");
}
catch (Error e)
{
    System.Console.WriteLine(e.Errormesage);
    errormesage = e.Message;
}
catch (Exception e)
{
    errormesage = e.Message;
}
System.Console.WriteLine(errormesage);



