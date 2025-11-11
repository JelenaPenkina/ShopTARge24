using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<RealEstate> Create(RealEstateDto dto);
        Task<RealEstate> Update(RealEstateDto dto);
        Task<RealEstate> DetailAsync(Guid id);
        Task<RealEstate> Delete(Guid id);

        // //UnitTest jaoks
        //Task<RealEstateDto> GetById(Guid id);
        //Task<RealEstateDto> CreateUnit(RealEstateDto dto);
        //Task<bool> DeleteUnit(Guid? id);
        //Task Update(Guid? id);
    }
}
