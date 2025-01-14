using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.DTOs.ApplicationUser;
using BackEnd.DTOs.PredictionGame;
using BackEnd.Models.Classes;

namespace BackEnd.Mappers
{
    public static class ApplicationUserMappers
    {
        public static ApplicationUserDto ToApplicationUserDto(this ApplicationUser applicationUserModel)
        {
            return new ApplicationUserDto
            {
                Id = applicationUserModel.Id,
                UserName = applicationUserModel.UserName,
                CreatedPredictionGames = applicationUserModel.CreatedPredictionGames
                    .Select(x => new PredictionGameDto {
                        Id = x.Id,
                        PredictionGameTitle = x.PredictionGameTitle,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Privacy = x.Privacy,
                        GameCreatorId = x.GameCreatorId,
                        Events = x.Events?.Select(x => x.ToEventDto()).ToList(),
                        UniqueCode = x.UniqueCode
                    })
                    .ToList(),
                ProfilePicture = applicationUserModel.ProfilePicture,
                DateOfBirth = applicationUserModel.DateOfBirth,
                Email = applicationUserModel.Email
            };
        }
        public static ApplicationUser ToApplicationUserFromDto(this ApplicationUserDto applicationUserDto)
        {
            return new ApplicationUser
            {
                Id = applicationUserDto.Id,
                UserName = applicationUserDto.UserName,
            };
        }
        public static ApplicationUser ToApplicationUserFromCreateDto(this CreateApplicationUserRequestDto applicationUserDto)
        {
            return new ApplicationUser
            {
                UserName = applicationUserDto.UserName,
                DateOfBirth = applicationUserDto.DateOfBirth,
            };
        }
        public static ApplicationUser ToApplicationUserFromUpdateDto(this UpdateApplicationUserDto applicationUserDto)
        {
            return new ApplicationUser
            {
                UserName = applicationUserDto.UserName,
                DateOfBirth = applicationUserDto.DateOfBirth,
                ProfilePicture = applicationUserDto.ProfilePicture
            };
        }

        

        public static byte[]? PictureToByteArray(IFormFile? File){
            if (File == null){
                return null;
            }
            using (var memoryStream = new MemoryStream())
            {
                File.CopyTo(memoryStream);
                var profilePicture = memoryStream.ToArray(); // Store as byte[]
                return profilePicture;
            }
        }
    }
}