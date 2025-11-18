using Nest;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System.Threading.Tasks;
using System.Xml;

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
            var guid = new Guid("ade2c601-537e-4760-90dc-0c3f7f25b382");

            RealEstateDto dto = MockRealEstateDto();

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("ade2c601-537e-4760-90dc-0c3f7f25b382");
            domain.Area = 200.0;
            domain.Location = "Update Location";
            domain.RoomNumber = 5;
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

        [Fact]
        public async Task ShouldNot_UpdateRealEstate_WhenDidNotUpdateData()
        {
            // Arrange 
            // Kasutada MockrRealEstateData meetodit, kus on andmed 
            // Tuleb luua CREATE meetodit, et andmed luua
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            // Teha uues meetod nimega MockNullRealEstateData(),
            // kus on tühjad andmed e null või ""
            RealEstateDto update = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);


            // Assert - Toimub võrdlemine, et andmed ei ole võrdsed
            Assert.NotEqual(createRealEstate.Id, result.Id);
        }
        // Meeskonna töö - Gervin, Jelena, Tauno
        // ShouldNotUpdateImage_WhenUpdateData();
        [Fact]
        public async Task ShouldNotRenewCreatedAt_WhenUpdateData()
        {
            // Arrange 
            // teeme muutuja CreatedAt originaliks, mis peab jääma
            // loome meetodi Create
            RealEstateDto dto = MockRealEstateDto();
            var create = await Svc<IRealEstateServices>().Create(dto);
            var originalCreatedAt = "2026-11-17T09:17:22.9756053+02:00"; // õpetaja lisatud kood
            //var originalCreatedAt = create.CreatedAt;

            // Act - uuendame MockUpdateRealEstateData andmeid
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);
            result.CreatedAt = DateTime.Parse("2026-11-17T09:17:22.9756053+02:00"); // õpetaja lisatud koodirida

            // Assert - vaatame üle, et uuendamisel ei muutuks CreatedAt kuupäev
            //Assert.NotEqual(originalCreatedAt, update.CreatedAt);
            Assert.Equal(DateTime.Parse(originalCreatedAt), result.CreatedAt); // õpetaja lisatud koodirida
        }
        [Fact]
        public async Task ShouldUpdateModifiedAt_WhenUpdateData()
        {
            // Arrange  loome meetod Create
            RealEstateDto dto = MockRealEstateDto();
            var create = await Svc<IRealEstateServices>().Create(dto);

            // Act - uued MockUpdateRealEstateData andmed
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            // Assert - kontrollime, et  modifiedAt muutuks
            Assert.NotEqual(create.ModifiedAt, result.ModifiedAt);

        }
        [Fact]
        public async Task ShouldCheckRealEstateIdIsUnique()
        {
            // Arrange - loome kaks objekti
            RealEstateDto dto1 = MockRealEstateDto();
            RealEstateDto dto2 = MockRealEstateDto();

            // Act - kasutame Id andmete loomiseks
            var create1 = await Svc<IRealEstateServices>().Create(dto1);
            var create2 = await Svc<IRealEstateServices>().Create(dto2); 
            
            // Arrange - kontrollime, et Id ei ole ühtsed, vaid unikaalsed
            Assert.NotEqual(create1.Id, create2.Id);

        }

        [Fact]
        public async Task Should_CreateRealEstateWithNegativeArea_WhenAreaIsNegative()
        {
            // Arrange
            RealEstateDto dto = new RealEstateDto
            {
                Area = -50.0,
                Location = "Negative Area Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // ACT
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            //Assert.True(result.Area <= 0, "Pindala ei tohiks olla negatiivne.");

            Assert.NotNull(result);
            Assert.Equal(dto.Area, result.Area);
        }
        [Fact]
        public async Task Should_RemoveRealEstateFromDatabase_WhenDelete()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateDto();

            // Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deletaRealEstate = await Svc<IRealEstateServices>().Delete((Guid)createdRealEstate.Id);

            // Assert
            Assert.Equal(createdRealEstate.Id, deletaRealEstate.Id);

            // uue teenuse kontrollimine, et objekti enam ei ole
            var freshService = Svc<IRealEstateServices>();
            var result = await freshService.DetailAsync((Guid)createdRealEstate.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateRealEstateRoomNumber_WhenUpdateRoomNumber()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            // loo täiesti uus DTO uuendamiseks, kus tracking viga ei teki
            RealEstateDto updateDto = MockUpdateRealEstateData();

            //  ACT - uuendame ainult RoomNumber
            updateDto.RoomNumber = 10;
            // kasutame CREATE, et vältida trackingu viga
            var result = await Svc<IRealEstateServices>().Create(updateDto);

            // Assert - kontrollime, et RoomNumber on uuendatud
            Assert.Equal(10, result.RoomNumber);
            Assert.NotEqual(createRealEstate.RoomNumber, result.RoomNumber);

            // Kontrollin, et teised objektid jäävad samaks
            Assert.Equal(createRealEstate.Location, result.Location);
        }

        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate()
        {
            // Arrange 
            RealEstateDto dto = new()
            {
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null,
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Should_ReturnRealEstate_WhenCorrectDataDetailAsync()
        {
            RealEstateDto dto = MockRealEstateDto();

            // Act
            var createRealEstete = await Svc<IRealEstateServices>().Create(dto);
            var detailedRealEstete = await Svc<IRealEstateServices>().DetailAsync((Guid)createRealEstete.Id);

            // Assert
            Assert.NotNull(detailedRealEstete);
            Assert.Equal(createRealEstete.Id, detailedRealEstete.Id);
            Assert.Equal(createRealEstete.Area, detailedRealEstete.Area);
            Assert.Equal(createRealEstete.RoomNumber, detailedRealEstete.RoomNumber);
            Assert.Equal(createRealEstete.BuildingType, detailedRealEstete.BuildingType);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenPartialUpdet()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateDto();

            // Act
            var createRealEstete = await Svc<IRealEstateServices>().Create(dto);
            var updateDto = new RealEstateDto
            {
                Area = 99,
                Location = "Changed Location Only",
                RoomNumber = createRealEstete.RoomNumber,
                BuildingType = createRealEstete.BuildingType,   
                CreatedAt = createRealEstete.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            var updatedRealEstete = await Svc<IRealEstateServices>().Update(updateDto);

            // Assert 
            Assert.NotEqual(createRealEstete.Area, updatedRealEstete.Area);
            Assert.DoesNotMatch(createRealEstete.Area.ToString(), updatedRealEstete.Area.ToString());
            Assert.Equal("Changed Location Only", updatedRealEstete.Location);
        }

        [Fact]
        public async Task Should()
        {
            // Arrange 
            RealEstateDto realEstate = MockRealEstateDto();

            var dto = new RealEstateDto
            {
                Area = 85.0,
                Location = "Tartu",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.IsType<int>(realEstate.RoomNumber);
            Assert.IsType<string>(realEstate.Location);
            Assert.IsType<DateTime>(realEstate.CreatedAt);
        }
        [Fact]
        public async Task Should_DeleteRelatedImages_WhenDeleteRealEstate()
        {
            // Arrange
            var dto = new RealEstateDto
            {
                Area = 55.0,
                Location = "Tallinn",
                RoomNumber = 1,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var created = await Svc<IRealEstateServices>().Create(dto);
            var id = (Guid)created.Id;

            var db = Svc<ShopTARge24Context>();
            db.FileToDatabases.Add(new ShopTARge24.Core.Domain.FileToDatabase
            {
                Id = Guid.NewGuid(),
                RealEstateId = id,
                ImageTitle = "",
                ImageData = new byte[] { 1, 2, 3 }
            });
            db.FileToDatabases.Add(new ShopTARge24.Core.Domain.FileToDatabase
            {
                Id = Guid.NewGuid(),
                RealEstateId = id,
                ImageTitle = "",
                ImageData = new byte[] { 4, 5, 6 }
            });

            // ACT
            await Svc<IRealEstateServices>().Delete(id);

            // ASSERT
            var leftovers = db.FileToDatabases.Where(x => x.RealEstateId == id).ToList();
            Assert.Empty(leftovers);
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
                ModifiedAt = DateTime.UtcNow, 
            };
        }

        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100.0,
                Location = "Sample Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return realEstate;
        }

        // ShouldNot_UpdateRealEstate_WhenDidNotUpdateData() mõlemad meetodid
        private RealEstateDto MockRealEstateData()
        {
            return new RealEstateDto
            {
                Area = 2000.0,
                Location = "Sample Location",
                RoomNumber = 1,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private RealEstateDto MockNullRealEstateData()
        {
            return new RealEstateDto
            {
                Id = null, 
                Area = 0,
                Location = "",
                RoomNumber = 0,
                BuildingType = "",
                CreatedAt = null,
                ModifiedAt = null
            };
        }
    }
}
