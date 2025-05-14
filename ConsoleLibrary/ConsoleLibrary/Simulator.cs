namespace ConsoleLibrary;

public static class Simulator
{
    private static readonly Random _random = new();

    public static async Task RunSimulationAsync(Library library)
    {
        const int tasksCount = 100;

        List<Book> books = library.GetAllBooks().ToList();
        Task[] tasks = new Task[tasksCount];

        for (int i = 0; i < tasksCount; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                Book book = books[_random.Next(books.Count)];
                int newYear = _random.Next(1900, 2025);

                book.SetYear(newYear);
                MessagePrinter.ShowInfoMessage($"Book '{book.Name}' year changed to {newYear}.");
            });
        }

        await Task.WhenAll(tasks);
    }
}