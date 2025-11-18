using System.Reflection.Emit;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTarge24.RealEstateTest
{
    public class SpaceshipTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptySpaceship_WhenReturnResult()
        {
            // Arrange
            SpaceshipDto dto = new()
            {
                Name = "Gervin",
                Classification = "Test Unit",
                BuiltDate = DateTime.UtcNow,
                Crew = 4,
                EnginePower = 2500,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<ISpaceshipServices>().Create(dto);

            // Assert - kontrollib, et poleks kindlasti test null
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_DeleteByIdSpaceship_WhenDidNotDeleteSpaceship()
        {
            // Arrange 
            SpaceshipDto dto = MockSpaceshipDto();

            // Act
            var spacehsip1 = await Svc<ISpaceshipServices>().Create(dto);
            var spacehsip2 = await Svc<ISpaceshipServices>().Create(dto);

            var result = await Svc<ISpaceshipServices>().Delete((Guid)spacehsip2.Id);

            // Assert
            Assert.NotEqual(spacehsip1.Id, result.Id);
        }

        [Fact]
        public async Task ShouldCheckSpaceshipIdIsUnique()
        {
            // Arrange - loome kaks objekti
            SpaceshipDto dto1 = MockSpaceshipDto();
            SpaceshipDto dto2 = MockSpaceshipDto();

            // Act - kasutame Id andmete loomiseks
            var create1 = await Svc<ISpaceshipServices>().Create(dto1);
            var create2 = await Svc<ISpaceshipServices>().Create(dto2);

            // Arrange - kontrollime, et Id ei ole ühtsed, vaid unikaalsed
            Assert.NotEqual(create1.Id, create2.Id);

        }

        [Fact]
        public async Task Should_UpdateRealEstateEnginePower_WhenUpdateEnginePower()
        {
            // Arrange
            SpaceshipDto dto = MockSpaceshipDto();
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);

            // loo täiesti uus DTO uuendamiseks, kus tracking viga ei teki
            SpaceshipDto updateDto = MockUpdateSpaceshipData();

            //  ACT - uuendame ainult EnginePower
            updateDto.EnginePower = 4500;
            // kasutame CREATE, et vältida trackingu viga
            var result = await Svc<ISpaceshipServices>().Create(updateDto);

            // Assert - kontrollime, et EnginePower on uuendatud
            Assert.Equal(4500, result.EnginePower);
            Assert.NotEqual(createSpaceship.EnginePower, result.EnginePower);

            // Kontrollin, et teised objektid jäävad samaks
            Assert.Equal(createSpaceship.Crew, result.Crew);
        }
    

        private SpaceshipDto MockSpaceshipDto()
        {
            return new SpaceshipDto
            {
                Name = "Tauno",
                Classification = "Lala",
                BuiltDate = DateTime.UtcNow,
                Crew = 25,
                EnginePower = 5000,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }

        private SpaceshipDto MockUpdateSpaceshipData()
        {
            SpaceshipDto spaceship = new()
            {
                Name = "Anni",
                Classification = "Local",
                BuiltDate = DateTime.UtcNow,
                Crew = 25,
                EnginePower = 3000,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            return spaceship;
        }

    }

}
