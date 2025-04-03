using Infrastructure.Data.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories
{
    public class ProjectFactory
    {
        public static Project ToModel(ProjectEntity entity)
        {
            return entity == null
                ? null!
                : new Project
                {
                    Id = entity.Id,
                    Image = entity.Image,
                    ProjectName = entity.ProjectName,
                    Description = entity.Description,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    Budget = entity.Budget,
                    Created = entity.Created,
                    Client = new Client
                    {
                        Id = entity.Client.Id,
                        ClientName = entity.Client.ClientName
                    },
                    User = new User
                    {
                        Id = entity.User.Id,
                        FirstName = entity.User.FirstName,
                        LastName = entity.User.LastName
                    },
                    Status = new Status
                    {
                        Id = entity.Status.Id,
                        StatusName = entity.Status.StatusName
                    }
                };
        }

        public static ProjectEntity ToEntity(EditProjectFormData formData)
        {
            return formData == null
                ? null!
                : new ProjectEntity
                {
                    Id = formData.Id,
                    Image = formData.Image,
                    ProjectName = formData.ProjectName,
                    Description = formData.Description,
                    StartDate = formData.StartDate,
                    EndDate = formData.EndDate,
                    Budget = formData.Budget,
                    ClientId = formData.ClientId,
                    UserId = formData.UserId,
                    StatusId = formData.StatusId,
                    Created = formData.Created,
                };
        }

        public static ProjectEntity ToEntity(AddProjectFormData formData)
        {
            return formData == null
                ? null!
                : new ProjectEntity
                {
                    Image = formData.Image,
                    ProjectName = formData.ProjectName,
                    Description = formData.Description,
                    StartDate = formData.StartDate,
                    EndDate = formData.EndDate,
                    Budget = formData.Budget,
                    ClientId = formData.ClientId,
                    UserId = formData.UserId,
                    Created = DateTime.Now,
                };
        }
    }
}
