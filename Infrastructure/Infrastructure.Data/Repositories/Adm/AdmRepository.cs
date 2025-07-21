using Infrastructure.Data.Interfaces.Adm;
using Infrastructure.Data.Models;
using Infrastructure.Data.Models.Adm.AlteraStatusUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories.Adm
{
    public class AdmRepository : IAdmRepository
    {
        private readonly AppDbContext _context;
        public AdmRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AlteraStatusUser(AlteraStatusUserModel userModel)
        {
            var user = await _context.User.Where(x => x.cGuid == userModel.cGuid).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }
            if (user.bStatus != userModel.bStatus)
            {
                user.bStatus = userModel.bStatus;
                await _context.SaveChangesAsync();
            }
            return user.bStatus;
        }
    }
}
