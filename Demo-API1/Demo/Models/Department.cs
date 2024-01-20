using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Demo.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Manager { get; set; }
        
        //[JsonIgnore]
        public virtual List<Employee> Employee { get; set; }
    }
}
