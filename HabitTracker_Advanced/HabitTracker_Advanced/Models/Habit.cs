namespace HabitTracker_Advanced.Models;

public class Habit
{
    public int Id { get; set; }

    public string? HabitName { get; set; }

    public DateTime DateAndTime { get; set; }

    public double Quantity { get; set; }
}