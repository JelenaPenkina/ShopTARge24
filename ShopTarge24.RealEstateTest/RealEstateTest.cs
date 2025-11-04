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

        // ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        // Should_GetByIdRealEstate_WhenReturnsEqual()
        // Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        // Should_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()

        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            var realEstateService = Svc<IRealEstateServices>();

            // Act
            var result = await realEstateService.GetById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            var realEstateService = Svc<IRealEstateServices>();

            var dto = new RealEstateDto
            {
                Area = 75.0,
                Location = "Tallinn",
                RoomNumber = 2,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var created = await realEstateService.Create(dto);
            
            // Act
            var result = await realEstateService.GetById(created.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal("Tallinn", result.Location);
        }


        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            // Arrange
            var realEstateService = Svc<IRealEstateServices>();

            var dto = new RealEstateDto
            {
                Area = 90.0,
                Location = "Tartu",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var created = await realEstateService.Create(dto);

            // Act
            var deleted = await realEstateService.Delete(created.Id);
            var checkDeleted = await realEstateService.GetById(created.Id);

            // Assert
            Assert.True(deleted);
            Assert.Null(checkDeleted);
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            // Arrange
            var realEstateService = Svc<IRealEstateServices>();

            // Act
            var deleted = await realEstateService.Delete(Guid.NewGuid());

            // Assert
            Assert.False(deleted);
        }
    }
}
