using Library_Program;

var myLibrary = new LibraryReader("books.json");
Console.WriteLine($"Loaded {myLibrary.Books.Count} books.");

LibraryMenu.Start(myLibrary);