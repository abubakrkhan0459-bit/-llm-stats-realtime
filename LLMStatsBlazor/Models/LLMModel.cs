namespace LLMStatsBlazor.Models;

public class LLMModel
{
    public string Name { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "proprietary" or "opensource"
    public int Rank { get; set; }
    public string Description { get; set; } = string.Empty;
}
