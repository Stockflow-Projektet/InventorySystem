using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Serilog;
using Inventory.Core.Database;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.RepositoryInterfaces;

namespace Inventory.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseConnection _db;

        public ProductRepository()
        {
            _db = DatabaseConnection.Instance;
        }

        // --------------------------------------------------------------
        // -------------- Old Method Names (Service references) ---------
        // --------------------------------------------------------------

        public async Task AddProductToDb(Product product)
        {
            // Just delegate to AddAsync
            await AddAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsFromDb()
        {
            // Delegate to the new method
            return await GetAllAsync();
        }

        public async Task<Product> GetProductByIdFromDb(int id)
        {
            // Delegate to the new method
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> QueryProductsFromDb(string query)
        {
            Log.Verbose("QueryProductsFromDb called with query={Query}", query);

            const string sql = @"
                SELECT
                    [productId],
                    [type],
                    [name],
                    [manufacturer],
                    [description],
                    [price],
                    [status],
                    [author],
                    [publisher],
                    [ISBN],
                    [PublicationYear],
                    [NumberOfPages],
                    [InkColor],
                    [InkType],
                    [TipSize],
                    [PencilLeadHardness],
                    [IsErasable],
                    [PaperSize],
                    [PaperWeight],
                    [PaperColor],
                    [CoatingType]
                FROM [dbo].[Product]
                WHERE [name]        LIKE '%' + @Query + '%'
                   OR [description] LIKE '%' + @Query + '%'
                   OR [publisher]   LIKE '%' + @Query + '%'
                   OR [author]      LIKE '%' + @Query + '%';
            ";

            // Get an IDbConnection from the singleton
            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                // Use <dynamic> so Dapper returns IEnumerable<dynamic>
                IEnumerable<dynamic> rows = await conn.QueryAsync<dynamic>(sql, new { Query = query });

                // Map each row to a concrete Product subtype
                var results = rows
                    .Select(row => (Product)MapToConcreteProduct(row))
                    .ToList();

                Log.Information("Found {Count} products matching query='{Query}'", results.Count, query);
                return results;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "QueryProductsFromDb failed for query='{Query}'", query);
                throw;
            }
        }

        public async Task UpdateAsync(Product product)
        {
            Log.Verbose("UpdateAsync called for productId={Id}", product.ProductId);

            const string sql = @"
                UPDATE [dbo].[Product]
                SET
                    [type]               = @type,
                    [name]               = @name,
                    [manufacturer]       = @manufacturer,
                    [description]        = @description,
                    [price]              = @price,
                    [status]             = @status,
                    [author]             = @author,
                    [publisher]          = @publisher,
                    [ISBN]               = @ISBN,
                    [PublicationYear]    = @PublicationYear,
                    [NumberOfPages]      = @NumberOfPages,
                    [InkColor]           = @InkColor,
                    [InkType]            = @InkType,
                    [TipSize]            = @TipSize,
                    [PencilLeadHardness] = @PencilLeadHardness,
                    [IsErasable]         = @IsErasable,
                    [PaperSize]          = @PaperSize,
                    [PaperWeight]        = @PaperWeight,
                    [PaperColor]         = @PaperColor,
                    [CoatingType]        = @CoatingType
                WHERE [productId] = @productId;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                var parameters = BuildProductParameters(product);
                parameters.Add("@productId", product.ProductId);

                Log.Debug("Executing UPDATE for productId={Id}", product.ProductId);
                await conn.ExecuteAsync(sql, parameters);
                Log.Information("Successfully updated productId={Id}", product.ProductId);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to update productId={Id}", product.ProductId);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            Log.Verbose("DeleteAsync called for productId={Id}", id);

            const string sql = @"
                DELETE FROM [dbo].[Product]
                WHERE [productId] = @id;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                Log.Debug("Executing DELETE for productId={Id}", id);
                await conn.ExecuteAsync(sql, new { id });
                Log.Information("Successfully deleted productId={Id}", id);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to delete productId={Id}", id);
                throw;
            }
        }

        // --------------------------------------------------------------
        // -------------- "Modern" method names (optional) --------------
        // --------------------------------------------------------------

        public async Task<int> AddAsync(Product product)
        {
            Log.Verbose("AddAsync called for product {@Product}", product);

            const string sql = @"
                INSERT INTO [dbo].[Product]
                (
                    [type],
                    [name],
                    [manufacturer],
                    [description],
                    [price],
                    [status],
                    [author],
                    [publisher],
                    [ISBN],
                    [PublicationYear],
                    [NumberOfPages],
                    [InkColor],
                    [InkType],
                    [TipSize],
                    [PencilLeadHardness],
                    [IsErasable],
                    [PaperSize],
                    [PaperWeight],
                    [PaperColor],
                    [CoatingType]
                )
                VALUES
                (
                    @type,
                    @name,
                    @manufacturer,
                    @description,
                    @price,
                    @status,
                    @author,
                    @publisher,
                    @ISBN,
                    @PublicationYear,
                    @NumberOfPages,
                    @InkColor,
                    @InkType,
                    @TipSize,
                    @PencilLeadHardness,
                    @IsErasable,
                    @PaperSize,
                    @PaperWeight,
                    @PaperColor,
                    @CoatingType
                );
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                var parameters = BuildProductParameters(product);
                Log.Debug("Executing INSERT for product name={Name}", product.Name);

                var newId = await conn.ExecuteScalarAsync<int>(sql, parameters);
                Log.Information("Successfully inserted product with newId={NewId}", newId);
                return newId;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to insert product {@Product}", product);
                throw;
            }
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            Log.Verbose("GetByIdAsync called for productId={Id}", productId);

            const string sql = @"
                SELECT
                    [productId],
                    [type],
                    [name],
                    [manufacturer],
                    [description],
                    [price],
                    [status],
                    [author],
                    [publisher],
                    [ISBN],
                    [PublicationYear],
                    [NumberOfPages],
                    [InkColor],
                    [InkType],
                    [TipSize],
                    [PencilLeadHardness],
                    [IsErasable],
                    [PaperSize],
                    [PaperWeight],
                    [PaperColor],
                    [CoatingType]
                FROM [dbo].[Product]
                WHERE [productId] = @id;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                // Dapper returns dynamic if we don't specify a strong type
                var row = await conn.QuerySingleOrDefaultAsync<dynamic>(sql, new { id = productId });
                if (row == null)
                {
                    Log.Warning("No product found with productId={Id}", productId);
                    return null;
                }

                var product = MapToConcreteProduct(row);
                Log.Information("Successfully retrieved productId={Id}", productId);
                return product;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to retrieve productId={Id}", productId);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            Log.Verbose("GetAllAsync called.");

            const string sql = @"
                SELECT
                    [productId],
                    [type],
                    [name],
                    [manufacturer],
                    [description],
                    [price],
                    [status],
                    [author],
                    [publisher],
                    [ISBN],
                    [PublicationYear],
                    [NumberOfPages],
                    [InkColor],
                    [InkType],
                    [TipSize],
                    [PencilLeadHardness],
                    [IsErasable],
                    [PaperSize],
                    [PaperWeight],
                    [PaperColor],
                    [CoatingType]
                FROM [dbo].[Product];
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                // Cast to dynamic
                IEnumerable<dynamic> rows = await conn.QueryAsync<dynamic>(sql);

                var results = rows
                    .Select(row => (Product)MapToConcreteProduct(row))
                    .ToList();

                Log.Information("Retrieved {Count} products.", results.Count);
                return results;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to retrieve all products.");
                throw;
            }
        }

        // --------------------------------------------------------------
        // -------------------- Private Helpers --------------------------
        // --------------------------------------------------------------

        /// <summary>
        /// If underlying connection is DbConnection, do OpenAsync(). Otherwise do Open().
        /// </summary>
        private async Task OpenConnectionAsync(IDbConnection conn)
        {
            if (conn is DbConnection dbConn)
            {
                await dbConn.OpenAsync();
            }
            else
            {
                // Fallback for fake or non-async connections
                conn.Open();
            }
        }

        private DynamicParameters BuildProductParameters(Product product)
        {
            var p = new DynamicParameters();

            p.Add("@type", product.Type);
            p.Add("@name", product.Name);
            p.Add("@manufacturer", product.Manufacturer);
            p.Add("@description", product.Description);
            p.Add("@price", product.Price);
            p.Add("@status", product.Status);

            // Specialized columns default to null
            string author = null, publisher = null, isbn = null, pencilLead = null;
            string paperColor = null, coatingType = null, inkColor = null, inkType = null;
            bool? isErasable = null;
            int? publicationYear = null, numberOfPages = null;
            decimal? tipSize = null, paperWeight = null;
            string paperSize = null;

            switch (product)
            {
                case Book b:
                    author = b.Author;
                    publisher = b.Publisher;
                    isbn = b.ISBN;
                    publicationYear = b.PublicationYear;
                    numberOfPages = b.NumberOfPages;
                    break;

                case Paper pap:
                    paperSize = pap.PaperSize;
                    paperWeight = pap.PaperWeight;
                    paperColor = pap.PaperColor;
                    coatingType = pap.CoatingType;
                    break;

                case WritingImplements wt:
                    inkColor = wt.InkColor;
                    inkType = wt.InkType;
                    tipSize = wt.TipSize;
                    pencilLead = wt.PencilLeadHardness;
                    isErasable = wt.IsErasable;
                    break;
            }

            p.Add("@author", author);
            p.Add("@publisher", publisher);
            p.Add("@ISBN", isbn);
            p.Add("@PublicationYear", publicationYear);
            p.Add("@NumberOfPages", numberOfPages);
            p.Add("@InkColor", inkColor);
            p.Add("@InkType", inkType);
            p.Add("@TipSize", tipSize);
            p.Add("@PencilLeadHardness", pencilLead);
            p.Add("@IsErasable", isErasable);
            p.Add("@PaperSize", paperSize);
            p.Add("@PaperWeight", paperWeight);
            p.Add("@PaperColor", paperColor);
            p.Add("@CoatingType", coatingType);

            return p;
        }

        private Product MapToConcreteProduct(dynamic row)
        {
            string t = row.type;
            Product result;

            switch (t)
            {
                case "B":
                    result = new Book
                    {
                        Author = row.author,
                        Publisher = row.publisher,
                        ISBN = row.ISBN,
                        PublicationYear = row.PublicationYear,
                        NumberOfPages = row.NumberOfPages
                    };
                    break;

                case "P":
                    result = new Paper
                    {
                        PaperSize = row.PaperSize,
                        PaperWeight = row.PaperWeight,
                        PaperColor = row.PaperColor,
                        CoatingType = row.CoatingType
                    };
                    break;

                case "W":
                    result = new WritingImplements
                    {
                        InkColor = row.InkColor,
                        InkType = row.InkType,
                        TipSize = row.TipSize,
                        PencilLeadHardness = row.PencilLeadHardness,
                        IsErasable = row.IsErasable
                    };
                    break;

                default:
                    Log.Warning("Unknown product type '{Type}' found, returning Book as fallback.", t);
                    result = new Book();
                    break;
            }

            // Common fields
            result.ProductId = row.productId;
            result.Type = row.type;
            result.Name = row.name;
            result.Manufacturer = row.manufacturer;
            result.Description = row.description;
            result.Price = (decimal)row.price;
            result.Status = row.status;

            return result;
        }
    }
}
