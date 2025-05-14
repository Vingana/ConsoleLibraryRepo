using System.Text.Json;

namespace ConsoleLibrary;

public sealed class LibraryRepository
{
    private static LibraryRepository? _instance;
    private const string FilePath = "library.json";

    private LibraryRepository() { }

    public static LibraryRepository Instance
    {
        get
        {
            _instance ??= new LibraryRepository();
            return _instance;
        }
    }

    public void Load(Library library)
    {
        if (!File.Exists(FilePath))
            return;

        string jsonData = File.ReadAllText(FilePath);
        Dictionary<int, Book>? books = JsonSerializer.Deserialize<Dictionary<int, Book>>(jsonData);

        if (books != null)
        {
            foreach (Book book in books.Values)
            {
                if (!library.Books.ContainsKey(book.Id))
                {
                    library.AddBook(book);
                }
                else
                {
                    library.Books[book.Id] = book;
                }
            }
        }
    }

    public void Save(Library library)
    {
        string jsonData = JsonSerializer.Serialize(library.Books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, jsonData);
    }
}