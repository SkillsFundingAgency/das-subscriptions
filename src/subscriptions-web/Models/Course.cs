using System.ComponentModel.DataAnnotations;

namespace Esfa.Recruit.Subscriptions.Web.Models
{
    public class Course
    {
        [Display(Name = "Number")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }
    }
}