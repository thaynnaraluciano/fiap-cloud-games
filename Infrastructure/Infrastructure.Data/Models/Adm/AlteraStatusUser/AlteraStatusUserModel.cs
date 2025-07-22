using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models.Adm.AlteraStatusUser
{
    public class AlteraStatusUserModel
    {
        [Key]
        public Guid cGuid { get; set; }
        public bool bStatus { get; set; }

    }
}
