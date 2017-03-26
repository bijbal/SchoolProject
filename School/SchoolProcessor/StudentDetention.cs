using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//CLUBBING EVERYTHING INTO ONE FOR SPEED - #POC
namespace SchoolProcessor
{
   public class StudentDetention
    {
       public SchoolModel.Student Student { get; set; }
       public List<SchoolModel.DetentionType> StudentDetentions { get; set; }

    }


}
