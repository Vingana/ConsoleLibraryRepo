using System.Text.Json.Serialization;

namespace ConsoleLibrary;

public class Book(string name, string author, int year, int id)
{
    public string Name { get; } = name;
    public string Author { get; } = author;
    public int Year { get; private set; } = year;
    public int Id { get; } = id;

    [JsonInclude]
    public bool IsAvailable { get; private set; } = true;

    public void ChangeAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public override bool Equals(object? obj) => obj is Book other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public void SetYear(int newYear)
    {
        Year = newYear;
    }
}
