using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Inventory.Database.DBPopulater
{
    internal class BogusService : Connection
    {
        private string insertBookSql = "insert into Product values (@TYPE, @NAME, @AUTHOR, NULL, @DESCRIPTION, @PRICE, @AMOUNT, @STATUS, @PUBLISHER, NULL, @PUBLICATIONYEAR, @PAGES, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)";
        private string insertPaperSql = "insert into Product values (@TYPE, @NAME, NULL, @MANU, @DESCRIPTION, @PRICE, NULL, @STATUS, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, @SIZE, @WEIGHT, @COLOR, @COAT)";
        private string insertWritingToolSql = "insert into Product values (@TYPE, @NAME, @AUTHOR, NULL, @DESCRIPTION, @PRICE, @AMOUNT, @STATUS, @PUBLISHER, NULL, @PUBLICATIONYEAR, @PAGES, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)";
        public List<Book> GenerateBooks(int amount)
        {
            Randomizer.Seed = new Random();
            var faker = new Faker("en");
            var CreateBook = new Faker<Book>()
                .StrictMode(true)
                .RuleFor(b => b.Type, b => "B")
                .RuleFor(b => b.Name, b => faker.Lorem.Word().ToUpper() + " " + faker.Lorem.Word().ToUpper())
                .RuleFor(b => b.Description, b => faker.Lorem.Paragraph().ClampLength(0, 200))
                .RuleFor(b => b.Price, b => b.Random.Decimal(10, 200))
                .RuleFor(b => b.Pages, b => b.Random.Int(50, 500))
                .RuleFor(b => b.Author, b => faker.Name.FirstName() + " " + faker.Name.LastName())
                .RuleFor(b => b.Publisher, b => faker.Company.CompanyName() + " Publishing " + faker.Company.CompanySuffix())
                .RuleFor(b => b.PublicationYear, b => faker.Date.Past(15, DateTime.Now).Year);
            var books = CreateBook.Generate(amount);

            return books;
        }
        public List<Paper> GeneratePapers(int amount)
        {
            Randomizer.Seed = new Random();
            var size = new[] { "A1", "A2", "A3", "A4", "A5", "A6" };
            var weight = new[] { 0.5m, 1.0m, 1.5m, 2.0m, 2.5m, 3.0m };
            var coating = new[] { "Shiny", "Matte", "Metallic" };
            var PaperColor = new[] { "Blue", "Black", "Red", "Green", "Purple", "Gold" };
            var faker = new Faker("en");

            var CreatePaper = new Faker<Paper>()
                .StrictMode(true)
                .RuleFor(p => p.Type, p => "P")
                .RuleFor(p => p.Name, p => faker.Lorem.Word().ToUpper())
                .RuleFor(p => p.Manufacturer, faker.Company.CompanyName() + " " + faker.Company.CompanySuffix())
                .RuleFor(p => p.Description, p => faker.Lorem.Paragraph().ClampLength(0, 200))
                .RuleFor(p => p.Price, p => p.Random.Decimal(5, 50))
                .RuleFor(p => p.PaperSize, p => p.PickRandom(size))
                .RuleFor(p => p.PaperWeight, p => p.PickRandom(weight))
                .RuleFor(p => p.Color, p => p.PickRandom(PaperColor))
                .RuleFor(p => p.CoatingType, p => p.PickRandom(coating));
            var papers = CreatePaper.Generate(amount);

            return papers;
        }
        public List<WritingTool> GenerateWritingTools(int amount)
        {
            Randomizer.Seed = new Random(983658659);
            var faker = new Faker("en");
            var InkColor = new[] { "Blue", "Black", "Red", "Green", "Purple", "Gold" };
            var InkType = new[] { "Standard", "Thin", "Dry", "Wet", "IHaveNoIdea??" };
            var TipSize /*PHRASING!!*/ = new[] { 0.5m, 0.75m, 1.0m, 1.25m, 1.5m };

            var CreateWritingTools = new Faker<WritingTool>()
                .StrictMode(true)
                .RuleFor(w => w.Type, w => "WRT")
                .RuleFor(w => w.Name, w => faker.Commerce.ProductName())
                .RuleFor(w => w.Description, w => faker.Lorem.Paragraph().ClampLength(0, 200))
                .RuleFor(w => w.Price, w => w.Random.Decimal(15, 100))
                .RuleFor(w => w.InkColor, w => faker.PickRandom(InkColor))
                .RuleFor(w => w.InkType, w => faker.PickRandom(InkType))
                .RuleFor(w => w.TipSize, w => faker.PickRandom(TipSize))
                .RuleFor(w => w.IsErasable, w => faker.Random.Bool());
            var writingTools = CreateWritingTools.Generate(amount);

            return writingTools;
        }
        public int InsertBooks(List<Book> books)
        {
            int rows = 0;
            foreach (var book in books)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(insertBookSql, connection);
                    command.Parameters.AddWithValue("@TYPE", book.Type);
                    command.Parameters.AddWithValue("@NAME", book.Name);
                    command.Parameters.AddWithValue("@DESCRIPTION", book.Description);
                    command.Parameters.AddWithValue("@PRICE", book.Price);
                    command.Parameters.AddWithValue("@STATUS", "A");
                    command.Parameters.AddWithValue("@AMOUNT", 10);
                    command.Parameters.AddWithValue("@PAGES", book.Pages);
                    command.Parameters.AddWithValue("@AUTHOR", book.Author);
                    command.Parameters.AddWithValue("@PUBLISHER", book.Publisher);
                    command.Parameters.AddWithValue("@PUBLICATIONYEAR", book.PublicationYear);

                    command.Connection.Open();
                    rows += command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            return rows;
        }
        public int InsertPaper(List<Paper> papers)
        {
            int rows = 0;
            foreach (var pap in papers)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(insertPaperSql, connection);
                    command.Parameters.AddWithValue("@TYPE", pap.Type);
                    command.Parameters.AddWithValue("@NAME", pap.Name);
                    command.Parameters.AddWithValue("@MANU", pap.Manufacturer);
                    command.Parameters.AddWithValue("@DESCRIPTION", pap.Description);
                    command.Parameters.AddWithValue("@PRICE", pap.Price);
                    command.Parameters.AddWithValue("@STATUS", "A");
                    command.Parameters.AddWithValue("@SIZE", pap.PaperSize);
                    command.Parameters.AddWithValue("@WEIGHT", pap.PaperWeight);
                    command.Parameters.AddWithValue("@COLOR", pap.Color);
                    command.Parameters.AddWithValue("@COAT", pap.CoatingType);

                    command.Connection.Open();
                    rows += command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            return rows;
        }
    }
    public class Book
    {
        public string Type { get; set; }
        //public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
    }
    public class Paper
    {
        public int ProductId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PaperSize { get; set; }
        public decimal PaperWeight { get; set; }
        public string Color { get; set; }
        public string CoatingType { get; set; }
    }
    public class WritingTool
    {
        public int ProductId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string InkColor { get; set; }
        public string InkType { get; set; }
        public decimal TipSize { get; set; } // phrasing!!!
        public bool IsErasable { get; set; }
    }
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int DepotId { get; set; }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> MyProperty { get; set; }
    }
}
