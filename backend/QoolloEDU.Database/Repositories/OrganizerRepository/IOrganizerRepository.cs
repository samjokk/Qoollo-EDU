using System.Threading.Tasks;
using QoolloEDU.Database.models.Enums;
using QoolloEDU.Dto;

namespace QoolloEDU.Database.Repositories.OrganizerRepository
{
    public interface IOrganizerRepository
    {
        Task<OrganizerDto> GetOrganizerAsync(int orgId);
        Task<RegistrationDto> CreateOrganizerAsync(OrganizerRegDto organizerRegDto);
    }
}