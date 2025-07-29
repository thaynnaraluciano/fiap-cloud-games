using AutoMapper;
using Infrastructure.Data.Interfaces.Adm;
using Infrastructure.Data.Models.Adm.AlteraStatusUser;
using MediatR;

namespace Domain.Commands.v1.Adm.AlteraStatusUser
{
    public class AlteraUserStatusCommandHandler : IRequestHandler<AlteraUserStatusCommand, AlteraUserStatusCommandResponse>
    {
        private readonly IAdmRepository _admRepository;
        private readonly IMapper _mapper;
        public AlteraUserStatusCommandHandler(IAdmRepository admRepository, IMapper mapper)
        {
            _admRepository = admRepository;
            _mapper = mapper;
        }
        public async Task<AlteraUserStatusCommandResponse> Handle(AlteraUserStatusCommand request, CancellationToken cancellationToken)
        {
            var UserAlterar = _mapper.Map<AlteraStatusUserModel>(request);
            var AlteraStatusUserResult = await _admRepository.AlteraStatusUser(UserAlterar);
            return _mapper.Map<AlteraUserStatusCommandResponse>(AlteraStatusUserResult);
        }
    }
}
