namespace ConsoleLibrary;

public sealed class Library(Dictionary<int, Book>? books = null)
{
    public Dictionary<int, Book> Books { get; } = books ?? new();
    private readonly Lock _lock = new();

    public bool AddBook(Book book) => Books.TryAdd(book.Id, book);

    public bool RemoveBook(int bookId) => Books.Remove(bookId);

    public IEnumerable<Book> GetAllBooks() => Books.Values;

    public bool BorrowBook(int bookId)
    {
        if (Books.TryGetValue(bookId, out Book? book) && book.IsAvailable)
        {
            book.ChangeAvailability(false);
            return true;
        }
        return false;
    }

    public bool ReturnBook(int bookId)
    {
        if (Books.TryGetValue(bookId, out Book? book) && !book.IsAvailable)
        {
            book.ChangeAvailability(true);
            return true;
        }
        return false;
    }

    public IEnumerable<Book> SearchByName(string name) =>
        Books.Values
             .Where(book => book.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Book> SearchByAuthor(string author) =>
        Books.Values
             .Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

    public bool UpdateBookYear(int bookId, int newYear)
    {
        lock (_lock)
        {
            if (Books.TryGetValue(bookId, out Book? book))
            {
                book.SetYear(newYear);
                return true;
            }
            return false;
        }
    }
}
