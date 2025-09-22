using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {

        void FilesToApi(SpaceshipDto dto, Spaceships domain);

        // Minu versioon
        // Task<FileToApi> FilesToApi(SpaceshipDto dto, Spaceships domain);
    }
}
