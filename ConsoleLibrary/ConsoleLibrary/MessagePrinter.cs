namespace ConsoleLibrary;

public static class MessagePrinter
{
    public static void ShowSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowInfoMessage(string message)
    {
        Console.WriteLine(message);
    }
}