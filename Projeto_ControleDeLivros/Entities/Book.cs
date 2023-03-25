namespace Projeto_ControleDeLivros.Entities
{
    class Book
    {
        public string Name { get; set; }
        public string Authors { get; set; }
        public int Edition { get; set; }
        public string ISBN { get; set; }
        public char Status { get; set; }

        public Book(string name, string authors, int edition, string isbn, char status)
        {
            Name = name;
            Authors = authors;
            Edition = edition;
            ISBN = isbn;
            Status = status;
        }

        public string SaveToFile()
        {
            return $"{Name};{Authors};{Edition};{ISBN};{Status}";
        }

        public override string ToString()
        {
            string status;

            if (Status == 'M')
            {
                status = "Na estante";
            }
            else if (Status == 'L')
            {
                status = "Lendo";
            }
            else
            {
                status = "Emprestado";
            }

            return $"{Name}".PadRight(60) + $"| {Authors}".PadRight(60) + $"| {Edition}ª Ed".PadRight(12) + $"| {ISBN}".PadRight(20) + $"| {status}".PadRight(12);
        }
    }
}
