namespace Library_Program;

//This class runs the main menu and it's functions for the program
public static class LibraryMenu
{
    //The menu has a library reader passed in on call so it has access to the list of books
    public static void Start(LibraryReader lib)
    {
        var running = true;
        while (running)
        { 
            //This prints out the basic menu and waits for the user to input a number
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

            //The menu uses a switch to account for the different options a user can pick
            switch (key.KeyChar)
            {
                //This iterates through the list of books printing out their titles and authors
                case '1':
                    Console.WriteLine("Your Library Contains:");
                    foreach (var book in lib.Books)
                    {
                         Console.WriteLine($"{book.Title} by {book.Author}");
                    }
                    break;
                
                //This allows the user to search through the library to see if a book is included
                //This search functionality allows the user to search by title or author
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
                
                //This allows the user to add a new book to the library by collecting the values needed to create a book
                //object before initializing a book with those values and adding it to the library's list of books
                case '3':
                    Console.WriteLine("Adding Book:");
                    
                    Console.WriteLine("What is the title of the book? ");
                    var title = (Console.ReadLine() ?? "").Trim();
                    
                    Console.WriteLine("What is the author's name? ");
                    var author = (Console.ReadLine() ?? "").Trim();
                    
                    Console.WriteLine("What year was the book published? ");
                    var yearInput = (Console.ReadLine() ?? "").Trim();
                    var year = int.Parse(yearInput);
                    
                    Console.WriteLine("Have you read the book already or not? Y/N");
                    var readInput = Console.ReadKey(intercept: true);
                    bool read;
                    if (readInput.KeyChar == 'Y' || readInput.KeyChar == 'y')
                    {
                        read = true;
                    }
                    else
                    {
                        read = false;
                    }
                    

                    var newBook = new Book(title, author, year, read);
                    lib.Add(newBook);
                    break;
                
                //This allows the user to select a book in the library by number and then select which part of the book
                //they'd like to update then collects the new value and updates the associated value for that item in
                //the library's list
                case '4':
                    Console.WriteLine("Input the number of the book you'd like to update:");
                    for (int i = 0; i < lib.Books.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {lib.Books[i].Title} by {lib.Books[i].Author}");
                    }

                    var updateInput = Console.ReadLine();
                    if (int.TryParse(updateInput, out var choice) && choice >= 1 && choice <= lib.Books.Count)
                    {
                        var selected = lib.Books[choice - 1];
                        Console.Clear();
                        Console.WriteLine("Here's the book you've picked input the number of the value you'd like to update for this book:");
                        Console.WriteLine($"1. Title:{selected.Title}");
                        Console.WriteLine($"2. Author:{selected.Author}");
                        Console.WriteLine($"3. Year of Publication:{selected.Year}");
                        Console.WriteLine($"4. If you've read the book or not:{selected.Read}");
                        var bookUpdate = Console.ReadLine();
                        if (int.TryParse(bookUpdate, out var data) && data >= 1 && data <= 4)
                        {
                            switch (data)
                            {
                                case 1:
                                    Console.WriteLine("What's the new title? ");
                                    selected.Title = Console.ReadLine() ?? "";
                                    break;
                                case 2:
                                    Console.WriteLine("What's the new author? ");
                                    selected.Author = Console.ReadLine() ?? "";
                                    break;
                                case 3:
                                    Console.WriteLine("What's the new year of publication? ");
                                    selected.Year = int.Parse(Console.ReadLine() ?? "");
                                    break;
                                case 4:
                                    Console.WriteLine("Have you read the book or not? ");
                                    selected.Read = bool.Parse(Console.ReadLine() ?? "");
                                    break;
                            }
                        }
                    }
                    break;
                
                //This just uses the library reader's Save() method to push all changes to the library to the JSON
                //this allows data to persist in between uses.
                case '5':
                    lib.Save();
                    Console.WriteLine("All changes saved.");
                    break;
                
                //This closes the program.
                case '6':
                    running = false;
                    break;
            }

        }
    }
}