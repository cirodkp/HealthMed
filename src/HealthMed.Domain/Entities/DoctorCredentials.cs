using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthMed.Domain.Entities
{
    [Table("DoctorCredentials", Schema = "public")]
    public class DoctorCredentials
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("crm")]
        public string Crm { get; set; } = "";
        [Column("password")]
        public string Password { get; set; } = "";
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
        [Column("active")]
        public bool Active { get; set; }
    }
}
