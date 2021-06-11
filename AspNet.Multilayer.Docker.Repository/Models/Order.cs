using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet.Multilayer.Docker.Repository.Models
{
    /// <summary>
    /// Order
    /// </summary>
    [Table("ORDER")]
    public class Order
    {
        /// <summary>
        /// order id
        /// </summary>
        [Column("ORDER_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        [Column("ORDER_USERID")]
        public int UserId { get; set; }
    }
}
