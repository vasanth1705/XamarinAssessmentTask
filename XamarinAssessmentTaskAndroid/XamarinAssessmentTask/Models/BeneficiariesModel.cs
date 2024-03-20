using SQLite;

namespace XamarinAssessmentTask.Models
{
    [Table("Beneficiaries")]
    public class BeneficiariesModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public bool IsVerified { get; set; }
        public int TotalTopUpAllowance { get; set; }
    }
}