using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Airline_Reservation_System.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("Passport Number")]
        public int PassportNumber { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("Cellphone Number")]
        public int CellphoneNumber { get; set; }

        
    }
}
