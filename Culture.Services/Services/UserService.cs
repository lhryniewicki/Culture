using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

		public UserService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
        }

		public async Task<IEnumerable<AppUser>> GetEventParticipants(int id)
		{
            return await _unitOfWork.UserRepository.GetEventParticipants(id);
		}

		public  Task<AppUser> GetUserById(string id)
		{
			return _unitOfWork.UserRepository.GetUserById(id);
		}

		public  Task<AppUser> GetUserByName(string name)
		{
			return _unitOfWork.UserRepository.GetUserByName(name);
		}

        public Task<AppUser> GetUserByNameWithCalendar(string userName)
        {
            return _unitOfWork.UserRepository.GetUserByNameWithCalendar(userName);
        }
        public Task<AppUser> GetUserByEmail(string email)
        {
            return _unitOfWork.UserRepository.GetUserByEmail(email);
        }

        public  Task<IList<string>> GetUserRoles(AppUser user)
        {
            return _unitOfWork.UserRepository.GetUserRoles(user);
        }

        public async Task<bool> IsUserSigned(Guid userId, int eventId)
        {
            return await _unitOfWork.UserInEventRepository.IsUserSigned(userId, eventId) != null ? true:false;
        }

        public async Task<IEnumerable<DateTime>> GetUserCalendarDays(Guid userId, string category, string query)
        {
            var userWithCalendar = await _unitOfWork.UserRepository.GetUserByIdWithCalendar(userId);
            var userConfig = await this.GetUserConfiguration(userWithCalendar.Id);


            if (category == null && query == null)
            {
                return userWithCalendar
                    .Calendar
                    .Events
                    .Where(x => userConfig.CalendarPastEvents == false ? x.Event.TakesPlaceDate > DateTime.Now : true)
                    .Select(x => x.Event.TakesPlaceDate)
                    .ToList();
            }

            return userWithCalendar
                     .Calendar
                     .Events.Where(x=> (query != null ? (x.Event.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1 
                     || x.Event.CityName.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1) : true)
                     &&
                     (category != null ? x.Event.Category == category: true))
                     .Select(x => x.Event.TakesPlaceDate)
                     .ToList();

        }

        public async Task<IEnumerable<EventInCalendarDto>> GetUserEventsInDay(Guid userId, DateTime day)
        {
            var userWithCalendar = await _unitOfWork.UserRepository.GetUserByIdWithCalendar(userId);

            return userWithCalendar
                .Calendar
                .Events
                .Where(x => x.Event.TakesPlaceDate.ToString("MM/dd/yyyy") == day.ToString("MM/dd/yyyy"))
                .Select(x => new EventInCalendarDto()
                {
                    EventName = x.Event.Name,
                    EventSlug = x.Event.UrlSlug
                });
        }

        public async Task<UserDetailsDto> GetUserDetailsByName(string name)
        {
            var user = await _unitOfWork.UserRepository.GetUserByName(name);
            var userConfig = await this.GetUserConfiguration(user.Id);

            var model = new UserDetailsDto(user)
            {
                UserConfiguration = new UserConfigurationDto(userConfig)
            };

            return model;
        }

        public async Task<string> UpdateUserData(string userId, UpdateUserViewModel userData, string avatarPath)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(userId);

            var oldAvatarPath = user.AvatarPath;

            user.AvatarPath = avatarPath ?? oldAvatarPath;
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            await _unitOfWork.UserRepository.SetEmail(user, userData.Email);

            return oldAvatarPath;
        }

        public Task Commit()
        {
           return _unitOfWork.Commit();
        }

        public Task<UserConfiguration> GetUserConfiguration(Guid userId)
        {
            return _unitOfWork.UserRepository.GetUserConfiguration(userId);
        }

        public async Task<UserConfiguration> UpdateUserConfig(UpdateUserConfigViewModel userConfig, Guid userId)
        {
            var userConfiguration = await _unitOfWork.UserRepository.GetUserConfiguration(userId);

            userConfiguration.LogOutAfter = userConfig.LogOutAfter;
            userConfiguration.CommentsDisplayAmount = userConfig.CommentsDisplayAmount;
            userConfiguration.EventsDisplayAmount = userConfig.EventsDisplayAmount;
            userConfiguration.CalendarPastEvents = userConfig.CalendarPastEvents;
            userConfiguration.SendEmailNotification = userConfig.SendEmailNotification;
            userConfiguration.Anonymous = userConfig.Anonymous;
            return userConfiguration;
        }
    }
}
