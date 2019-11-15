﻿using Culture.Contracts.IServices;
using Culture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.ViewModels;

namespace Culture.Services.Services
{
	public class UserService:IUserService
	{
        private readonly IUnitOfWork _unitOfWork;

		public UserService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
        }

		public async Task<IEnumerable<Guid>> GetEventParticipants(int id)
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

            if(category == null && query == null)
            {
                return userWithCalendar
                    .Calendar
                    .Events
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

            return new UserDetailsDto(user);
        }

        public async Task<string> UpdateUserData(string userId, UpdateUserViewModel userData, string avatarPath)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(userId);

            var oldAvatarPath = user.AvatarPath;

            user.AvatarPath = avatarPath ?? oldAvatarPath;
            user.FirstName = userData.FirstName;
            user.LastName = userData.LastName;
            user.Email = userData.Email;

            return oldAvatarPath;
        }

        public Task Commit()
        {
           return _unitOfWork.Commit();
        }

    }
}
