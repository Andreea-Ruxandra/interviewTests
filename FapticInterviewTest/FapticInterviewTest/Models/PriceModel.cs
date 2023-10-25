using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FapticInterviewTest.Models
{  
       /// <summary>
          /// Structure of Prices table - columns: Id - primary key, AveragePrice - price after aggregation, Start and End - period of time for which we request the price
       /// </summary>
    [PrimaryKey(nameof(Id))]
    public class PriceModel
    {
        public int Id { get; set; }
        public double AveragePrice { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
