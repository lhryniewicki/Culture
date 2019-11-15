using Culture.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
    public interface IGeolocationService
    {
        Task<GeometryDto> Localize(string city, string address);
        Task<IEnumerable<MapGeometryDto>> GetMap(Guid userId);
        Task<IEnumerable<MapGeometryDto>> GetEventMap(int eventId);
    }
}
