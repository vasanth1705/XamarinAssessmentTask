using SQLite.Net.Attributes;

namespace XamarinAssessmentTaskiOS.Models
{
    [Table("Payments")]
    public class PaymentModel
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int BeneficiaryId { get; set; }
        public string Username { get; set; }
        public string MobileNumber { get; set; }
        public int TopUpAmount { get; set; }
        public int RechargeFee { get; set; }
    }
}