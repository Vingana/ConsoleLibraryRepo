namespace ConsoleLibrary;

public sealed class BookFactory
{
    public Book CreateNewBook(string name, string author, int year, Library library)
    {
        ArgumentNullException.ThrowIfNull(library);

        int uniqueId = GenerateUniqueId(library);
        return new Book(name, author, year, uniqueId);
    }

    private static int GenerateUniqueId(Library library)
    {
        HashSet<int> usedIds = [..library.Books.Values.Select(book => book.Id)];

        int newId = 1;
        while (usedIds.Contains(newId))
        {
            newId++;
        }

        return newId;
    }
}