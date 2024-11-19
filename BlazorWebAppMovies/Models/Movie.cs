using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Genre { get; set; } // ? -> Can hold null value.

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")] // type decimal with ,00 a zaoukr. na 18 čísel za , // [] vlastnosti Price typu.
    public decimal Price { get; set; }

}
