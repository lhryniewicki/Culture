using Culture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.DataAccess.Context
{
	public class CultureDbContext:IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
	{
		public DbSet<Calendar> Calendars { get; set; }
		public DbSet<Emoticon> Emoticons { get; set; }
		public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<EventReaction> EventReactions { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<UserInEvent> UsersInEvent { get; set; }
		public DbSet<EventInCalendar> EventsInCalendar { get; set; }
        public DbSet<UserConfiguration> UserConfigurations { get; set; }



        public CultureDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			ConfigureAppUser(builder);
			ConfigureEventInCalendar(builder);
			ConfigureEvent(builder);
			ConfigureUserNotification(builder);
			ConfigureUserInEvent(builder);
			ConfigureEventReaction(builder);
            ConfigureUserConfig(builder);


            base.OnModelCreating(builder);

		}

        private void ConfigureUserConfig(ModelBuilder builder)
        {
            var configBuilder = builder.Entity<UserConfiguration>();

            configBuilder.HasKey(x => x.UserId);
        }

		private void ConfigureAppUser(ModelBuilder builder)
		{
			var userBuilder = builder.Entity<AppUser>();

            userBuilder
                .HasOne(x => x.UserConfiguration)
                .WithOne(x => x.User)
                .HasForeignKey<UserConfiguration>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

			userBuilder
				.HasOne(x => x.Calendar)
				.WithOne(x => x.BelongsTo)
				.HasForeignKey<Calendar>(x=>x.BelongsToId)
				.OnDelete(DeleteBehavior.Cascade);

			userBuilder
				.HasMany(x => x.HostedEvents)
				.WithOne(x => x.CreatedBy)
				.OnDelete(DeleteBehavior.Restrict);

			userBuilder
				.HasMany(x => x.Comments)
				.WithOne(x => x.Author)
				.OnDelete(DeleteBehavior.Restrict);


		}
		private void ConfigureEventInCalendar(ModelBuilder builder)
		{
			var eventCalendarBuilder = builder.Entity<EventInCalendar>();

			eventCalendarBuilder
				.HasKey(x => new { x.CalendarId, x.EventId });

			eventCalendarBuilder
				.HasOne(x => x.Calendar)
				.WithMany(x => x.Events)
				.HasForeignKey(x => x.CalendarId)
				.OnDelete(DeleteBehavior.Restrict);

			eventCalendarBuilder
				.HasOne(x => x.Event)
				.WithMany(x => x.EventsInCalendar)
				.HasForeignKey(x => x.EventId)
				.OnDelete(DeleteBehavior.Restrict);


		}
		private void ConfigureEvent(ModelBuilder builder)
		{
			var eventBuilder = builder.Entity<Event>();

            eventBuilder
                .HasMany(x => x.EventsInCalendar)
                .WithOne(x => x.Event)
                .HasForeignKey(x => x.EventId)
                .OnDelete(DeleteBehavior.Cascade);

			eventBuilder
				.HasMany(x => x.Comments)
				.WithOne(x => x.Event);

			eventBuilder
				.HasMany(x => x.Reactions)
				.WithOne(x => x.Event);

			eventBuilder
				.HasMany(x => x.Notifications)
				.WithOne(x => x.Event);
		}
		private void ConfigureUserNotification(ModelBuilder builder)
		{
            var userNotificationBuilder = builder.Entity<Notification>();

            userNotificationBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.Notifications);

		}
		private void ConfigureUserInEvent(ModelBuilder builder)
		{
			builder.Entity<UserInEvent>()
			.HasKey(k => new { k.EventId, k.UserId });

			builder.Entity<UserInEvent>()
				.HasOne(x => x.User)
				.WithMany(x => x.ParticipatedEvents)
				.HasForeignKey(x => x.UserId);

			builder.Entity<UserInEvent>()
				.HasOne(x => x.Event)
				.WithMany(x => x.Participants)
				.HasForeignKey(x => x.EventId);


		}
		private void ConfigureEventReaction(ModelBuilder builder)
		{
			var reactionBuilder = builder.Entity<EventReaction>();

			reactionBuilder
				.HasKey(k => new { k.EventId, k.UserId });

			reactionBuilder
				.HasOne(x => x.User)
				.WithMany(x => x.EventReactions);

		}
	}

}

