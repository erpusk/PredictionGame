using itb2203_2024_predictiongame.Backend.Models.Classes;

namespace BackEnd.Models.Classes;
public record ChatMessages
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int SenderId { get; set; }
    public ApplicationUser? Sender { get; set; }
    public string? SenderName { get; set; }
    public string? Message { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now.ToUniversalTime();
    public PredictionGame? Game { get; set; }
}
