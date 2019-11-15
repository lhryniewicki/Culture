using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.IServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public GeolocationService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MapGeometryDto>> GetMap(Guid userId)
        {
            var userWithCalendar = await _unitOfWork.UserRepository.GetUserByIdWithCalendar(userId);

            var geometryList = new List<MapGeometryDto>();

            foreach(var calendarEvent in userWithCalendar.Calendar.Events)
            {
                geometryList.Add(new MapGeometryDto
                {
                    Name = calendarEvent.Event.Name,
                    Latitude = calendarEvent.Event.Latitude,
                    Longitude = calendarEvent.Event.Longitude,
                    Address= calendarEvent.Event.CityName+", "+ calendarEvent.Event.StreetName,
                    UrlSlug = calendarEvent.Event.UrlSlug
                });
            }
            return geometryList;
        }

        public async Task<GeometryDto> Localize(string city, string address)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var queryString = $"{city.Replace(' ', '+') + '+' + address.Replace(' ', '+')}";

            var response = await getRequest(queryString, httpClient);

            return getLatLong(response, city, address);

        }
        private  async Task<HttpResponseMessage> getRequest(string queryString, HttpClient httpClient)
        {
            var apiKey = _configuration.GetValue<string>("Values:GeolocationKey");

            HttpResponseMessage response;

            do
            {
                response = await httpClient.GetAsync($"https://eu1.locationiq.com/v1/search.php?key={apiKey}&q={queryString}&format=json");

                Thread.Sleep(1000);

            } while (response.StatusCode == (HttpStatusCode)429);

            return response;
        }
        private GeometryDto getLatLong(HttpResponseMessage response, string city, string address)
        {

            try
            {
                var addressNumber = getFormatedAddressNumber(address);
                var addressStreet = getFormatedAddressStreetName(address);

                var data = (JArray)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                GeometryDto geometry;

                foreach (var obj in data)
                {

                    geometry = obj.ToObject<GeometryDto>();

                    if ( geometry.DisplayName.Contains(addressStreet) && 
                        geometry.DisplayName.Contains(addressNumber))
                    {
                        return geometry;
                    }
                }
            }
            catch (Exception e)
            {

            }


            return null;
        }
        private  string getFormatedAddressNumber(string address)
        {
            return string.Concat(
                address
                .ToArray()
                .Reverse()
                .TakeWhile(x => char.IsNumber(x))
                .Reverse());

             
        }
        private  string getFormatedAddressStreetName(string address)
        {
           return string.Concat(
                address.Replace("al. ", "")
                .Replace("Al. ", "")
                .ToArray()
                .TakeWhile(x => char.IsLetter(x) || char.IsWhiteSpace(x)))
                .TrimEnd(' ');
        }

        /// <summary>
        /// Zwraca liste zeby na froncie reuzywalna funkcja byla
        /// </summary>
        /// <param name="eventId">event id</param>
        public async Task<IEnumerable<MapGeometryDto>> GetEventMap(int eventId)
        { 
            var eventData = await _unitOfWork.EventRepository.GetEventAsync(eventId);

            return new List< MapGeometryDto>()
            {
                new MapGeometryDto()
                {
                Name = eventData.Name,
                Address = eventData.CityName + ", " + eventData.StreetName,
                Latitude = eventData.Latitude,
                Longitude = eventData.Longitude,
                UrlSlug = eventData.UrlSlug
                }
            };
        }
    }
}
