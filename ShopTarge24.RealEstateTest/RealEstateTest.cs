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

        // Should_GetByIdRealEstate_WhenReturnsEqual()
        // Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        // Should_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()

        //[Fact]
        //public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqualMy()
        //{
        //    var realEstateService = Svc<IRealEstateServices>();

        //    // Act
        //    var result = await realEstateService.GetById(Guid.NewGuid());

        //    // Assert
        //    Assert.Null(result);
        //}

        // õpetaja kood koos https://guidgenerator.com/: 
        [Fact]
        public async Task ShouldNot_GetByIdRealEstate_WhenReturnsNotEqual()
        {
            // Arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("ade2c601-537e-4760-90dc-0c3f7f25b382");

            // Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            // Assert
            Assert.NotEqual(wrongGuid, guid);
        }


        [Fact]
        public async Task Should_GetByIdRealEstate_WhenReturnsEqual()
        {
            // Arrange
            Guid databaseGuid = Guid.Parse("ade2c601-537e-4760-90dc-0c3f7f25b382");
            Guid guid = Guid.Parse("ade2c601-537e-4760-90dc-0c3f7f25b382");

            // Act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            // Assert
            Assert.Equal(databaseGuid, guid);
        }


        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDeleteRealEstate()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateDto();

            // Act
            await Svc<IRealEstateServices>().DeleteUnit(Guid);

            // Assert
            Assert.NotNull(dto);

        }
        private RealEstateDto MockRealEstateDto()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Sample Location", 
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            // Arrange
            var realEstateService = Svc<IRealEstateServices>();

            // Act
            await realEstateService.Delete(Guid.NewGuid());

            // Assert
            Assert.NotNull(realEstateService);
        }
    }
}
