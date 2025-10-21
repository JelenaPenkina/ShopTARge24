using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using System.Xml;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(KindergartenDto dto, Kindergarten domain);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dto);
        void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain);
        void FilesToApi(SpaceshipDto dto, Spaceships domain);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);

        // Minu versioon
        // Task<FileToApi> FilesToApi(SpaceshipDto dto, Spaceships domain);
    }
}
