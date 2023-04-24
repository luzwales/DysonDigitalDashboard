namespace DysonDigitalDashboard.Models.Domain;

public class ScoreList
{
    public int ID { get; set; }
    public string Kpi { get; set; }
    public int ScoreValue { get; set; }
    public DateTime Date { get; set; }
    public string Remark { get; set; }
}