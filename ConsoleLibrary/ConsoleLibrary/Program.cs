using ConsoleLibrary;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Library library = new();
        BookFactory bookFactory = new();
        LibraryService service = new(library, bookFactory);
        LibraryRepository repository = LibraryRepository.Instance;

        try
        {
            repository.Load(library);
            MessagePrinter.ShowSuccessMessage("Library data loaded successfully.");
        }
        catch (Exception ex)
        {
            MessagePrinter.ShowErrorMessage($"Error loading library data: {ex.Message}");
        }

        while (true)
        {
            ConsoleManager.ShowMenu();
            int operation = GetOperationChoice();

            switch (operation)
            {
                case 1:
                    (string name, string author, int year) = ConsoleManager.InputBookDetails();
                    OperationResult addResult = service.AddBook(name, author, year);
                    ConsoleManager.HandleOperationResult(addResult);
                    break;

                case 2:
                    int removeId = ConsoleManager.InputBookId("remove");
                    OperationResult removeResult = service.RemoveBook(removeId);
                    ConsoleManager.HandleOperationResult(removeResult);
                    break;

                case 3:
                    int borrowId = ConsoleManager.InputBookId("borrow");
                    OperationResult borrowResult = service.BorrowBook(borrowId);
                    ConsoleManager.HandleOperationResult(borrowResult);
                    break;

                case 4:
                    int returnId = ConsoleManager.InputBookId("return");
                    OperationResult returnResult = service.ReturnBook(returnId);
                    ConsoleManager.HandleOperationResult(returnResult);
                    break;

                case 5:
                    IEnumerable<Book> allBooks = service.GetAllBooks();
                    ConsoleManager.DisplayBooks(allBooks, "All books in library:");
                    break;

                case 6:
                    string nameQuery = ConsoleManager.InputSearchQuery("Enter book name to search: ");
                    IEnumerable<Book> nameResults = service.SearchBooksByName(nameQuery);
                    ConsoleManager.DisplayBooks(nameResults, "Found books by name:");
                    break;

                case 7:
                    string authorQuery = ConsoleManager.InputSearchQuery("Enter author name to search: ");
                    IEnumerable<Book> authorResults = service.SearchBooksByAuthor(authorQuery);
                    ConsoleManager.DisplayBooks(authorResults, "Found books by author:");
                    break;

                case 8:
                    OperationResult simulationResult = await service.RunSimulationAsync();
                    ConsoleManager.HandleOperationResult(simulationResult);
                    break;

                case 9:
                    MessagePrinter.ShowInfoMessage("Saving library data...");
                    try
                    {
                        repository.Save(library);
                        MessagePrinter.ShowSuccessMessage("Library data saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessagePrinter.ShowErrorMessage($"Error saving library data: {ex.Message}");
                    }
                    MessagePrinter.ShowInfoMessage("Exiting the program.");
                    Environment.Exit(0);
                    break;

                default:
                    MessagePrinter.ShowErrorMessage("Invalid operation. Please select a valid option.");
                    break;
            }
        }
    }

    static int GetOperationChoice()
    {
        MessagePrinter.ShowInfoMessage("Enter operation number: ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return -1;
        }
        if (int.TryParse(input, out int operation))
        {
            return operation;
        }
        return -1;
    }
}
