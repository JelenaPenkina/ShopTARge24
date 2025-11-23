using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ShopTARge24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopTARge24Context _context;

        public FileServices
            (
            IHostEnvironment webHost, 
            ShopTARge24Context context
            )
        {
            _webHost = webHost;
            _context = context;
        }

        public void FilesToApi(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                string uploadFolder = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                foreach (var file in dto.Files)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    var fileToApi = new FileToApi
                    {
                        Id = Guid.NewGuid(),
                        ExistingFilePath = uniqueFileName,
                        KindergartenId = domain.Id
                    };

                    _context.FileToApis.Add(fileToApi);
                }

                _context.SaveChanges();
            }
        }
        public void FilesToApi(SpaceshipDto dto, Spaceships domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath +"\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files) 
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName); // annab unique nime file, et neid eristada 

                    using (var fileStream = new FileStream(filePath, FileMode.Create)) // uurida võrgu teemat, sest seda jagatakse väiksemateks tükkideks, et saan korraga mitu inimest teha
                    {
                        file.CopyTo(fileStream);

                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName, 
                            SpaceshipId = domain.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                }
            }
        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            ////kui soovin kustutada, siis pean läbi Id pildi ülesse otsima
            //var imageId = await _context.FileToApis
            //    .FirstOrDefaultAsync(x => x.Id == dto.Id);

            ////kus asuvad pildid, mida hakatakse kustutama
            //var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
            //    + imageId.ExistingFilePath;

            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}

            //_context.FileToApis.Remove(imageId);
            //await _context.SaveChangesAsync();

            //return null;
            var image = await _context.FileToApis.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (image != null)
            {
                var filePath = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload", image.ExistingFilePath);
                if (File.Exists(filePath)) File.Delete(filePath);

                _context.FileToApis.Remove(image);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var file = await _context.FileToDatabases.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (file != null)
                {
                    _context.FileToDatabases.Remove(file);
                }
            }

            await _context.SaveChangesAsync();
        }


        public void UploadFilesToDatabase(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files == null || dto.Files.Count == 0) return;

            foreach (var file in dto.Files)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);

                var fileDb = new FileToDatabase
                {
                    Id = Guid.NewGuid(),
                    ImageTitle = file.FileName,
                    ImageData = ms.ToArray(),
                    KindergartenId = domain.Id
                };

                _context.FileToDatabases.Add(fileDb);
            }

            _context.SaveChanges();
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            var removedImages = new List<FileToApi>();

            foreach (var dto in dtos)
            {
                var file = await _context.FileToApis.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (file != null)
                {
                    // kustuta fail serverist
                    var filePath = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload", file.ExistingFilePath);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    _context.FileToApis.Remove(file);
                    removedImages.Add(file);
                }
            }

            await _context.SaveChangesAsync();
            return removedImages;
        }
        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
        {

           // pärast lugemiseks koju->https://www.tutkit.com/et/tekstiopetused/8141-c-parimise-alused-loo-tohusad-klassid

           //  toimub kontroll, kas on vähemalt üks fail või mitu

            if (dto.Files != null && dto.Files.Count > 0)
            {
               // tuleb kaustada foreach´i, et mitu faili ülesse laadida
                foreach (var file in dto.Files)
                {
                    // foreach´i sees tuleb kasutada using´t
                    using (var target = new MemoryStream()) // MemoryStream -> salvestab byte´te jada ning saatma database file
                    {
                       // mappimine
                       FileToDatabase files = new FileToDatabase()
                       {
                           Id = Guid.NewGuid(),
                           ImageTitle = file.FileName,
                           RealEstateId = domain.Id
                       };

                        file.CopyTo(target);
                        files.ImageData = target.ToArray();

                        // andmed salvestada andmebaasi
                        _context.FileToDatabases.Add(files);
                    }
                }
            }
        }
    }
}

//public async Task RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
//{
//    foreach (var dto in dtos)
//    {
//        var file = await _context.FileToDatabases.FirstOrDefaultAsync(x => x.Id == dto.Id);
//        if (file != null)
//        {
//            _context.FileToDatabases.Remove(file);
//        }
//    }

//    await _context.SaveChangesAsync();
//}

//kui soovin kustutada, siis pean läbi Id pildi ülesse otsima
//            var imageId = await _context.FileToApis
//                .FirstOrDefaultAsync(x => x.Id == dto.Id);

//kus asuvad pildid, mida hakatakse kustutama
//            var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
//                + imageId.ExistingFilePath;

//if (File.Exists(filePath))
//{
//    File.Delete(filePath);
//}

//_context.FileToApis.Remove(imageId);
//await _context.SaveChangesAsync();

//return null;
//        }

//        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
//{
//    foreach (var dto in dtos)
//    {
//        var imageId = await _context.FileToApis
//            .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);

//        var filePath = _webHost.ContentRootPath + "\\wwwroot\\multipleFileUpload\\"
//            + imageId.ExistingFilePath;

//        if (File.Exists(filePath))
//        {
//            File.Delete(filePath);
//        }

//        _context.FileToApis.Remove(imageId);
//        await _context.SaveChangesAsync();
//    }

//    return null;
//}


