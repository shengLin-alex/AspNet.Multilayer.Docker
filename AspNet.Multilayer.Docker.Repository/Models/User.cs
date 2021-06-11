using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet.Multilayer.Docker.Repository.Models
{
    /// <summary>
    /// User
    /// </summary>
    [Table("user")]
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        [Column("user_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        [Column("user_name")]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
