namespace Loaner.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? PAN { get; set; }
        public string? AADHAR { get; set; }
        public string? VoterId { get; set; }
        public string? Remarks { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedOnDateUtc { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime LastUpdatedOnUtc { get; set; }
        public string? LastUpdateBy { get; set; }
        public DateTime AuthOnUtc { get; set; }
        public string? AuthBy { get; set; }

    }
}
