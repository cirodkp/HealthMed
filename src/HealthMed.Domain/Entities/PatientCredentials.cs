using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthMed.Domain.Entities
{
    [Table("PatientCredentials", Schema = "public")]
    public class PatientCredentials
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("crm")]
        public string CPF { get; set; } = "";
        [Column("password")]
        public string Password { get; set; } = "";
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("active")]
        public bool Active { get; set; }
    }
}
