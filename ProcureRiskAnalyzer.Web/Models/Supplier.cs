namespace ProcureRiskAnalyzer.Web.Models;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }

    public ICollection<Tender> Tenders { get; set; } = new List<Tender>();
}
