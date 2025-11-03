using ShopTARge24.Core.Dto.Cocktail;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface ICocktailServices
    {
        Task<CocktailRootDto> GetCocktails(CocktailResultDto dto);
    }
}
