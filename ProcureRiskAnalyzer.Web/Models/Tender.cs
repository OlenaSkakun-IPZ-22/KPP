namespace ProcureRiskAnalyzer.Web.Models;

public class Tender
{
    public int Id { get; set; }
    public string TenderCode { get; set; }
    public string Buyer { get; set; }
    public DateTime Date { get; set; }
    public decimal ExpectedValue { get; set; }

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}
