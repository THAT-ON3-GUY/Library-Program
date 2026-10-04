namespace Library_Program;

public class Book(string title, string author, int year, bool read)
{
    public string Title { get; set; } = title;
    public string Author { get; set; } = author;
    public int Year { get; set; } = year;
    public bool Read { get; set; } = read;
}