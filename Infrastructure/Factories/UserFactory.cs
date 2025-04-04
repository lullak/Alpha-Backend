using Infrastructure.Data.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Infrastructure.Factories
{
    public class UserFactory
    {
        public static User ToModel(UserEntity entity, string role)
        {
            return entity == null
                ? null!
                : new User
                {
                    Id = entity.Id,
                    Image = entity.Image,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    JobTitle = entity.JobTitle,
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                    City = entity.Address?.City,
                    PostalCode = entity.Address?.PostalCode,
                    StreetName = entity.Address?.StreetName,
                    Role = role
                };
        }

        public static UserEntity ToEntity(EditUserFormData formData)
        {
            return formData == null
                ? null!
                : new UserEntity
                {
                    Id = formData.Id,
                    Image = formData.Image,
                    FirstName = formData.FirstName,
                    LastName = formData.LastName,
                    JobTitle = formData.JobTitle,
                    PhoneNumber = formData.PhoneNumber,
                    Email = formData.Email,
                    Address = new UserAddressEntity
                    {
                        City = formData.City,
                        PostalCode = formData.PostalCode,
                        StreetName = formData.StreetName
                    }
                };
        }

        public static UserEntity ToEntity(AddUserFormData formData)
        {
            return formData == null
                ? null!
                : new UserEntity
                {
                    Image = formData.Image,
                    FirstName = formData.FirstName,
                    LastName = formData.LastName,
                    JobTitle = formData.JobTitle,
                    PhoneNumber = formData.PhoneNumber,
                    Email = formData.Email,
                    Address = new UserAddressEntity
                    {
                        City = formData.City,
                        PostalCode = formData.PostalCode,
                        StreetName = formData.StreetName
                    }
                };
        }
    }
}
