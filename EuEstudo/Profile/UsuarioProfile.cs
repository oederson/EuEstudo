using AutoMapper;
using EuEstudo.Data.DTO;
using EuEstudo.Models;

namespace EuEstudo.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CriarUsuarioDTO, Usuario>();
    }
}
