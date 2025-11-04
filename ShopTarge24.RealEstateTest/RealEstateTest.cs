using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using System.Threading.Tasks;

namespace ShopTarge24.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange nii öelda ette valmistus
            RealEstateDto dto = new()
            {
                Area = 120.5,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert - kontrollib, et poleks kindlasti test null
            Assert.NotNull(result);
        }
    }
}
