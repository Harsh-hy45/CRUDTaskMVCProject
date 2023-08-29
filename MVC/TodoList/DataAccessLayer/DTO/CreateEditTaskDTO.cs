using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class CreateEditTaskDTO
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CompleteByDate { get; set; }
        public DateTime UpdatedDate { get; set;}=DateTime.Now;  
    }
}
