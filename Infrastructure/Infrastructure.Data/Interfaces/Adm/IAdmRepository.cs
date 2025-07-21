using Infrastructure.Data.Models.Adm.AlteraStatusUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Interfaces.Adm
{
    public interface IAdmRepository
    {
        Task<bool> AlteraStatusUser(AlteraStatusUserModel userModel);
    }
}
