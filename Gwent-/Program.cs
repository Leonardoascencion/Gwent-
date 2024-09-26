///AKI VA TODO LO Q VIENE SIENDO EJECUTAR EL CODIGO
string errormesage = "";
try
{
    Lexer lexer = new(File.ReadAllText(@"tester.txt"));

    foreach (var item in Lexer.Tokens)
        System.Console.WriteLine(item.Type + " " + item.Lexeme);

    System.Console.WriteLine("empieza el parser");
    Parser parser = new Parser();
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




