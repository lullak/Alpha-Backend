using Infrastructure.Data.Entities;
using Infrastructure.Models;

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

        public static UserEntity ToEntity(EditUserFormData formData, string? newImageFileName = null)
        {
            return formData == null
                ? null!
                : new UserEntity
                {
                    Id = formData.Id,
                    Image = newImageFileName ?? formData.Image,
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

        public static UserEntity ToEntity(AddUserFormData formData, string? newImageFileName = null)
        {
            return formData == null
                ? null!
                : new UserEntity
                {
                    Image = newImageFileName,
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
