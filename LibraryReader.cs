namespace Library_Program;
using System.Text.Json;


//LibraryReader class which takes my JSON and then using JsonSerializer
//turns each book's data from the JSON file into a Book object
public class LibraryReader(string path)
{
    //JsonSerializerOptions allows me to set how I want the JSON file's format
    //to be interpreted when I read it and write to it
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    //This is the actual line that takes the books from my file and Deserializes them into a list of book objects
    private readonly List<Book> _books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText(path), Options)!;

    //This interface makes it so the list of books can only be read which is great
    //for just displaying my whole list of books or getting other information for List methods
    public IReadOnlyList<Book> Books => _books;

    //This function allows me to iterate through my list of books using a string to filter
    //out all books that don't contain that string either in their title or in the author's name
    public IEnumerable<Book> Search(string query) =>
        _books.Where(b =>
            b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase));

    //This method adds a book object passed to it to the book list
    public void Add(Book book) => _books.Add(book);
    
    //This method saves all the changes I've made to my book list back to the JSON file.
    //It uses the book list and the formatting I set earlier with JsonSerializerOptions
    public void Save() => File.WriteAllText(path, JsonSerializer.Serialize(_books, Options));
}