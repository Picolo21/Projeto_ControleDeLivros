using Projeto_ControleDeLivros.Entities;

class Program
{
    private static int Menu()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("|              MENU DE OPÇÕES              |");
        Console.WriteLine("|".PadRight(43) + "|");
        Console.WriteLine("|   " + "[1] - Adicionar livro".PadRight(35) + "    |");
        Console.WriteLine("|   " + "[2] - Imprimir lista de livros".PadRight(35) + "    |");
        Console.WriteLine("|   " + "[3] - Sair".PadRight(35) + "    |");
        Console.WriteLine("|".PadRight(43) + "|");
        Console.WriteLine("--------------------------------------------\n");
        Console.Write("Escolha uma opção: ");
        int op = int.Parse(Console.ReadLine());
        return op;
    }

    private static void Main(string[] args)
    {
        Console.Title = "Controle de Livros";

        int option;
        List<Book> books = new List<Book>();
        string myBooks = "Minha Estante de Livros.txt";
        string booksReading = "Lendo.txt";
        string borrowedBooks = "Emprestados.txt";
        string path = @"C:\Users\" + Environment.UserName + @"\";

        string pathMyBooks = path + myBooks;
        string pathBooksReading = path + booksReading;
        string pathBorrowedBooks = path + borrowedBooks;

        do
        {
            option = Menu();
            switch (option)
            {
                case 1:
                    books = Create(books, pathMyBooks, pathBooksReading, pathBorrowedBooks);
                    break;
                case 2:
                    Print(books, pathMyBooks, pathBooksReading, pathBorrowedBooks);
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("Fim do programa!\n");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida! Por favor, escolha uma opção válida");
                    Thread.Sleep(2000);
                    break;
            }
        } while (option != 3);
    }

    private static List<Book> Create(List<Book> books, string pathMyBooks, string pathBooksReading, string pathBorrowedBooks)
    {
        Console.Clear();
        Console.Write("Digite o nome do livro: ");
        string nameBook = Console.ReadLine();
        Console.Clear();
        Console.Write("Digite o(s) nome(s) do(s) autor(es) (separe por vírgula): ");
        string authors = Console.ReadLine();
        Console.Clear();
        Console.Write("Digite a edição do livro: ");
        int edition = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.Write("Digite o código ISBN do livro: ");
        string isbn = Console.ReadLine();
        char situation;
        do
        {
            Console.Clear();
            Console.Write("Qual a situação do livro ([M] - minha estante | [L] - lendo | [E] - emprestado): ");
            situation = char.Parse(Console.ReadLine().ToUpper());
        } while ((situation != 'M') && (situation != 'L') && (situation != 'E'));
        books.Add(new Book(nameBook, authors, edition, isbn, situation));
        SaveFile(books, pathMyBooks, pathBooksReading, pathBorrowedBooks);
        Console.Clear();
        Console.WriteLine("Livro salvo no arquivo com sucesso!");
        Thread.Sleep(3000);
        books.Clear();
        return books;
    }

    private static int PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine("|                     MENU DE OPÇÕES DE IMPRESSÃO                    |");
        Console.WriteLine("|".PadRight(69) + "|");
        Console.WriteLine("|   " + "[1] - Imprimir lista de livros que estão na estante".PadRight(65) + "|");
        Console.WriteLine("|   " + "[2] - Imprimir lista de livros que estou lendo".PadRight(65) + "|");
        Console.WriteLine("|   " + "[3] - Imprimir lista de livros que estão emprestados".PadRight(65) + "|");
        Console.WriteLine("|   " + "[4] - Retornar ao Menu Principal".PadRight(65) + "|");
        Console.WriteLine("|".PadRight(69) + "|");
        Console.WriteLine("----------------------------------------------------------------------\n");
        Console.Write("Escolha uma opção: ");
        int op = int.Parse(Console.ReadLine());
        Console.WriteLine();
        return op;
    }

    private static void Print(List<Book> books, string pathMyBooks, string pathBooksReading, string pathBorrowedBooks)
    {
        int option2;

        do
        {
            option2 = PrintMenu();
            switch (option2)
            {
                case 1:
                    if (File.Exists(pathMyBooks))
                    {
                        PrintFile(books, pathMyBooks);
                    }
                    else
                    {
                        Console.WriteLine("O arquivo não existe ou não está neste diretório");
                        Thread.Sleep(3000);
                    }
                    break;
                case 2:
                    if (File.Exists(pathBooksReading))
                    {
                        PrintFile(books, pathBooksReading);
                    }
                    else
                    {
                        Console.WriteLine("O arquivo não existe ou não está neste diretório");
                        Thread.Sleep(3000);
                    }
                    break;
                case 3:
                    if (File.Exists(pathBorrowedBooks))
                    {
                        PrintFile(books, pathBorrowedBooks);
                    }
                    else
                    {
                        Console.WriteLine("O arquivo não existe ou não está neste diretório");
                        Thread.Sleep(3000);
                    }
                    break;
                case 4:
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção inválida! Por favor, escolha uma opção válida");
                    Thread.Sleep(3000);
                    break;
            }
        } while (option2 != 4);
    }

    private static void PrintFile(List<Book> books, string path)
    {
        do
        {
            Console.Clear();
            books = ReadFile(path);
            Console.WriteLine("Nome do livro".PadRight(60) + "| Autor(es)".PadRight(60) + "| Edição".PadRight(12) + "| ISBN".PadRight(20) + "| Status".PadRight(12));
            Console.WriteLine("------------------------------------------------------------+-----------------------------------------------------------+-----------+-------------------+--------------");
            foreach (Book item in books)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Aperte ENTER para voltar ao Menu de Impressão");
        } while (Console.ReadKey().Key != ConsoleKey.Enter);
    }

    private static List<Book> ReadFile(string path)
    {
        List<Book> list = new List<Book>();

        using (StreamReader sr = new StreamReader(path))
        {
            do
            {
                string[] text = sr.ReadLine().Split(';');
                list.Add(new Book(text[0], text[1], int.Parse(text[2]), text[3], char.Parse(text[4])));
            } while (!sr.EndOfStream);
        }

        return list;
    }

    private static void SaveFile(List<Book> books, string pathMyBooks, string pathBooksReading, string pathBorrowedBooks)
    {
        List<Book> myBooks = new List<Book>();
        List<Book> booksReading = new List<Book>();
        List<Book> borrowedBooks = new List<Book>();

        try
        {
            foreach (Book item in books)
            {
                if (item.Status == 'M')
                {
                    myBooks.Add(item);
                    using (StreamWriter sw = File.AppendText(pathMyBooks))
                    {
                        foreach (Book x in myBooks)
                        {
                            sw.WriteLine(x.SaveToFile());
                        }
                    }
                }
                else if (item.Status == 'L')
                {
                    booksReading.Add(item);
                    using (StreamWriter sw2 = File.AppendText(pathBooksReading))
                    {
                        foreach (Book x in booksReading)
                        {
                            sw2.WriteLine(x.SaveToFile());
                        }
                    }
                }
                else
                {
                    borrowedBooks.Add(item);
                    using (StreamWriter sw3 = File.AppendText(pathBorrowedBooks))
                    {
                        foreach (Book x in borrowedBooks)
                        {
                            sw3.WriteLine(x.SaveToFile());
                        }
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}