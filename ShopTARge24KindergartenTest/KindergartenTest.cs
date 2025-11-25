using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24KindergartenTest
{
    public class KindergartenTest : TestBase
    {
        [Fact]
        public async Task Should_RemoveImage_WhenImageIsDeleted()
        {

            // Arrange
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            var db = Svc<ShopTARge24Context>();

            var image = new FileToDatabase
            {
                Id = Guid.NewGuid(),
                KindergartenId = (Guid)created.Id,
                ImageTitle = "test",
                ImageData = new byte[] { 1, 2, 3 }
            };

            db.FileToDatabases.Add(image);
            await db.SaveChangesAsync();

            // Act
            var result = await Svc<IKindergartenServices>().RemoveImage(image.Id);

            // Assert
            Assert.True(result);
            Assert.Null(db.FileToDatabases.FirstOrDefault(x => x.Id == image.Id));
        }

        [Fact]
        public async Task Should_DeleteImages_WhenKindergartenDeleted()
        {
            // Arrange
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);
            var id = (Guid)created.Id;

            var db = Svc<ShopTARge24Context>();

            db.FileToDatabases.Add(new FileToDatabase
            {
                Id = Guid.NewGuid(),
                KindergartenId = id,
                ImageData = new byte[] { 5, 6, 7 }
            });

            await db.SaveChangesAsync();

            // Act
            await Svc<IKindergartenServices>().Delete(id);

            // Assert
            var images = db.FileToDatabases.Where(x => x.KindergartenId == id).ToList();
            Assert.Empty(images);
        }

        [Fact]
        public async Task Should_AddImage_WhenUpdateKindergarten()
        {
            // Arrange
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            dto.Id = created.Id;
            dto.Files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(new byte[] {1,2,3}), 0, 3, "file", "test.jpg")
            };

            // Act
            var result = await Svc<IKindergartenServices>().Update(dto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_UpdateChildrenCount()
        {

            // Arrange
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            var originalChildrenCount = created.ChildrenCount;
            dto.Id = created.Id;
            dto.ChildrenCount = 25;

            // Act
            var updated = await Svc<IKindergartenServices>().Update(dto);

            // Assert
            Assert.Equal(25, updated.ChildrenCount);
            Assert.NotEqual(originalChildrenCount, updated.ChildrenCount);
        }

        [Fact]
        public async Task Should_UpdateModifiedAt_WhenUpdateData()
        {
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            var originalUpdatedAt = created.UpdatedAt;
            dto.Id = created.Id;
            dto.UpdatedAt = DateTime.UtcNow.AddMinutes(5);

            // Act
            var updated = await Svc<IKindergartenServices>().Update(dto);

            // Assert
            Assert.NotEqual(originalUpdatedAt, updated.UpdatedAt);
        }

        [Fact]
        public async Task Should_RemoveKindergartenFromDatabase()
        {
            // Arrange
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto);

            await Svc<IKindergartenServices>().Delete((Guid)created.Id);

            // Act
            var result = await Svc<IKindergartenServices>().DetailAsync((Guid)created.Id);

            // Assert
            Assert.Null(result);
        }

        private KindergartenDto MockKindergartenDto()
        {
            return new KindergartenDto
            {
                Id = Guid.NewGuid(),
                GroupName = "Milka",
                ChildrenCount = 10,
                KindergartenName = "Test",
                TeacherName = "Marko",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Files = null

            };
        }

    }
}