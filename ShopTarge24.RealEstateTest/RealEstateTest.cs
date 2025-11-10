using Nest;
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
            var addRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deletRealEstate = await Svc<IRealEstateServices>().Delete((Guid)addRealEstate.Id);

            // Assert
            Assert.Equal(addRealEstate.Id, deletRealEstate.Id);

        }

        //[Fact]
        //public async Task Should_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        //{
        //    // Arrange
        //    var realEstateService = Svc<IRealEstateServices>();

        //    // Act
        //    await realEstateService.Delete(Guid.NewGuid());

        //    // Assert
        //    Assert.NotNull(realEstateService);
        //}
        
        // õpetaja versioon:
        [Fact]
        public async Task Should_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            // Arrange 
            var dto = MockRealEstateDto();

            // Act
            var realEstate1 = await Svc<IRealEstateServices>().Create(dto);
            var realEstate2 = await Svc<IRealEstateServices>().Create(dto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);

            // Assert
            Assert.NotEqual(realEstate1.Id, result.Id);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            // Arrange
            //var dto = MockRealEstateDto(); -> minu versiooni algus
            RealEstateDto dto = MockRealEstateDto();

            Guid guid = Guid.Parse("ade2c601-537e-4760-90dc-0c3f7f25b382");

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("ade2c601-537e-4760-90dc-0c3f7f25b382");
            domain.Area = 200.0;
            domain.Location = "Update Location";
            domain.RoomNumber = 4;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            // Act
            await Svc<IRealEstateServices>().Update(dto);

            // Assert
            //Assert.Equal(dto.Id, domain.Id); - minu lõplik versioon

            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.Area, domain.Area);
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            // Võrrelda RoomNumbri ning Location ja kasutada DoesNotMatch
            Assert.DoesNotMatch(dto.Location, domain.Location);
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());

        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateDataVersion2()
        {
            // Arrange ja Act koos

            // Alguses andmeid loome ja kasutame selleks MockRealEstateDto meetodit
            RealEstateDto dto = MockRealEstateDto();

            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            // andmeid uuendame ja kasutame uut Mock meetodit(selle peame ise looma) 
            RealEstateDto updateDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updateDto);         

            // Assert

            // lõpus kontrollime, et andmed erinevad
            Assert.NotEqual(createRealEstate.ModifiedAt, result.ModifiedAt);

            Assert.DoesNotMatch(result.Location, createRealEstate.Location);
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

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100.0,
                Location = "Secret Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return realEstate;
        }
    }
}
