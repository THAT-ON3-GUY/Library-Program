namespace Library_Program;

public static class LibraryMenu
{
    public static void Start(LibraryReader lib)
    {
        Console.WriteLine(new string('=', Console.WindowWidth));
        Console.WriteLine("Welcome to the Library Program");
        Console.WriteLine("Hit the Key for the Number of the Operation You'd Like to Execute:");
        Console.WriteLine("1. View whole library contents");
        Console.WriteLine("2. Search for Book");
        Console.WriteLine("3. Add a Book to the Library");
        Console.WriteLine("4. Update Book Information");
        Console.WriteLine("5. Save Changes");
        Console.WriteLine("6. Quit Program");
        Console.WriteLine(new string('=', Console.WindowWidth));
        Console.WriteLine("Waiting for number input...");
        var key = Console.ReadKey(intercept: true);
        Console.Clear();

        switch (key.KeyChar)
        {
            case '1':
                Console.WriteLine("Your Library Contains:");
                foreach (var book in lib.Books)
                {
                     Console.WriteLine($"{book.Title} by {book.Author}");
                }
                break;
            
            case '2':
                var text = "";
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Search here, hit ESC or ENTER to quit to menu: {text}");
                    if (!string.IsNullOrEmpty(text))
                    {
                        foreach (var book in lib.Search(text))
                        {
                            Console.WriteLine($"{book.Title} by {book.Author}");
                        }
                    }
                    var search = Console.ReadKey(intercept: true);
                    if (search.Key == ConsoleKey.Escape || search.Key == ConsoleKey.Enter) break;
                    if (search.Key == ConsoleKey.Backspace && text.Length > 0)
                    {
                        text = text[..^1];
                    }
                    else if (!char.IsControl(search.KeyChar))
                    {
                        text += search.KeyChar;
                    } 
                }
                break;
            case '3':
                Console.WriteLine("Adding Book:");
                
                Console.WriteLine("What is the title of the book? ");
                var title = (Console.ReadLine() ?? "").Trim();
                
                Console.WriteLine("What is the author's name? ");
                var author = (Console.ReadLine() ?? "").Trim();
                
                Console.WriteLine("What year was the book published? ");
                
        }

    }
}