namespace ConsoleLibrary;

public static class ConsoleManager
{
    public const string OPERATION_SELECTION = "Select an operation:";
    public const string OPTION_ADD = "1. Add new book";
    public const string OPTION_REMOVE = "2. Remove book";
    public const string OPTION_BORROW = "3. Borrow book";
    public const string OPTION_RETURN = "4. Return book";
    public const string OPTION_DISPLAY = "5. Display all books";
    public const string OPTION_SEARCH_NAME = "6. Search by name";
    public const string OPTION_SEARCH_AUTHOR = "7. Search by author";
    public const string OPTION_SIMULATE = "8. Run simulation (book year editing)";
    public const string OPTION_EXIT = "9. Save and exit";

    public static void ShowMenu()
    {
        MessagePrinter.ShowInfoMessage(OPERATION_SELECTION);
        MessagePrinter.ShowInfoMessage(OPTION_ADD);
        MessagePrinter.ShowInfoMessage(OPTION_REMOVE);
        MessagePrinter.ShowInfoMessage(OPTION_BORROW);
        MessagePrinter.ShowInfoMessage(OPTION_RETURN);
        MessagePrinter.ShowInfoMessage(OPTION_DISPLAY);
        MessagePrinter.ShowInfoMessage(OPTION_SEARCH_NAME);
        MessagePrinter.ShowInfoMessage(OPTION_SEARCH_AUTHOR);
        MessagePrinter.ShowInfoMessage(OPTION_SIMULATE);
        MessagePrinter.ShowInfoMessage(OPTION_EXIT);
    }

    public static (string, string, int) InputBookDetails()
    {
        MessagePrinter.ShowInfoMessage("Enter book name:");
        string name = Console.ReadLine() ?? string.Empty;

        MessagePrinter.ShowInfoMessage("Enter book author:");
        string author = Console.ReadLine() ?? string.Empty;

        MessagePrinter.ShowInfoMessage("Enter book year:");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year) || year <= 0)
        {
            MessagePrinter.ShowErrorMessage("Invalid input. Please enter a valid year:");
        }
        return (name, author, year);
    }

    public static int InputBookId(string action)
    {
        MessagePrinter.ShowInfoMessage($"Enter the ID of the book to {action}:");
        int bookId;
        while (!int.TryParse(Console.ReadLine(), out bookId) || bookId <= 0)
        {
            MessagePrinter.ShowErrorMessage("Invalid input. Please enter a valid book ID (positive number):");
        }
        return bookId;
    }

    public static string InputSearchQuery(string prompt)
    {
        MessagePrinter.ShowInfoMessage(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public static void DisplayBooks(IEnumerable<Book> books, string header = "Books:")
    {
        if (!books.Any())
        {
            MessagePrinter.ShowInfoMessage("No books found.");
            return;
        }

        MessagePrinter.ShowInfoMessage(header);
        foreach (Book book in books)
        {
            MessagePrinter.ShowInfoMessage($"Name: '{book.Name}', Author: {book.Author}, Year: {book.Year}, ID: {book.Id}, Available: {book.IsAvailable}");
        }
    }

    public static void ShowSimulationMessage(string result)
    {
        if (string.IsNullOrWhiteSpace(result))
            MessagePrinter.ShowErrorMessage("Simulation failed or was empty.");
        else
            MessagePrinter.ShowSuccessMessage(result);
    }

    public static void HandleOperationResult(OperationResult result)
    {
        if (result.Success)
            MessagePrinter.ShowSuccessMessage(result.Message);
        else
            MessagePrinter.ShowErrorMessage(result.Message);
    }
}