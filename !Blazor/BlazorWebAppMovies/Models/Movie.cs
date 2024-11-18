using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Models;

public class Movie
{
    public int Id { get; set; }

    public string? Title { get; set; } // ? - can hold null value

    public DateOnly ReleaseDate { get; set; }

    public string? Genre { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal 18, 2")] // The database column is a decimal of 18 digits with 2 decimal places.
    public decimal Price { get; set; }
}
