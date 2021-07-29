using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;
using QoolloEDU.Dto.PagesDto;

namespace QoolloEDU.Database.Repositories.OrganizerPageRepository
{
    public interface IOrganizerPageRepository
    {
        Task<OrganizerPageDto> GetOrganizerPageAsync(int orgId);
        Task<RegistrationDto> CreateOrganizerAsync(OrganizerRegDto organizerRegDto);
    }
}