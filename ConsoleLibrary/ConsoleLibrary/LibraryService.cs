using ConsoleLibrary;

public sealed class LibraryService
{
    private readonly Library _library;
    private readonly BookFactory _bookFactory;

    public LibraryService(Library library, BookFactory bookFactory)
    {
        _library = library;
        _bookFactory = bookFactory;
    }

    public OperationResult AddBook(string name, string author, int year)
    {
        Book book = _bookFactory.CreateNewBook(name, author, year, _library);
        return _library.AddBook(book)
            ? OperationResult.Ok("Book added successfully.")
            : OperationResult.Fail("Book with this ID already exists.");
    }

    public OperationResult RemoveBook(int id)
    {
        return _library.RemoveBook(id)
            ? OperationResult.Ok("Book removed successfully.")
            : OperationResult.Fail("Book not found.");
    }

    public OperationResult BorrowBook(int id)
    {
        return _library.BorrowBook(id)
            ? OperationResult.Ok("Book borrowed successfully.")
            : OperationResult.Fail("Book is not available.");
    }

    public OperationResult ReturnBook(int id)
    {
        return _library.ReturnBook(id)
            ? OperationResult.Ok("Book returned successfully.")
            : OperationResult.Fail("Book is already available or not found.");
    }

    public async Task<OperationResult> RunSimulationAsync()
    {
        if (_library.Books.Count == 0)
        {
            return OperationResult.Fail("Cannot run simulation: the library has no books.");
        }

        await Simulator.RunSimulationAsync(_library);
        return OperationResult.Ok("Simulation completed.");
    }

    public IEnumerable<Book> GetAllBooks() => _library.Books.Values.OrderBy(b => b.Id);

    public IEnumerable<Book> SearchBooksByName(string name) => _library.SearchByName(name);

    public IEnumerable<Book> SearchBooksByAuthor(string author) => _library.SearchByAuthor(author);
}