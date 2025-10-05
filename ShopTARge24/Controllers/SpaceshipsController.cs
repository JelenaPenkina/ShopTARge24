using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.Spaceships;
using static System.Net.Mime.MediaTypeNames;

namespace ShopTARge24.Controllers
{

    // F11 näitab kogu koodi järjestuse
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly ISpaceshipServices _spaceshipServices;
        private readonly IFileServices _fileServices;
        public SpaceshipsController
            (
                ShopTARge24Context context,
                ISpaceshipServices spaceshipServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
            _fileServices = fileServices;
        }


        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Classification = x.Classification,
                    BuiltDate = x.BuiltDate,
                    Crew = x.Crew,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files, // FileToApi klassist kutsume välja ning lisasime CreateUpdate modellisse juurde
                FileToApiDtos = vm.Image
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.Filepath, 
                        SpaceshipId = x.SpaceshipId
                    }).ToArray()
            };

            var result = await _spaceshipServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id

                }).ToArrayAsync();

            var vm = new SpaceshipCreateUpdateViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Classification = spaceship.Classification;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            //Tuleb dto ja vm omavahel ära mappida 
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                FileToApiDtos = vm.Image
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.Filepath,
                        SpaceshipId = x.SpaceshipId,
                    }).ToArray()
            };

            var result = await _spaceshipServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }

            // MINU VERSIOON -> 
            //  var vm = await _spaceshipServices.Update(dto);

            //  if (vm == null)
            //  {
            //      return RedirectToAction(nameof(Index));
            //  }

            //  return RedirectToAction(nameof(Index));

     
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipServices.DetailAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDeleteViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Classification = spaceship.Classification;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceship = await _spaceshipServices.Delete(id);

            if (spaceship == null)
            {
                return RedirectToAction(nameof(Index));
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            // kasutada service klassi meetodit, et info kätte saada
            var spaceship = await _spaceshipServices.DetailAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
              .Where(x => x.SpaceshipId == id)
              .Select(y => new ImageViewModel
              {
                  Filepath = y.ExistingFilePath,
                  ImageId = y.Id
              }).ToArrayAsync();

            // toimub viewModeliga mappimine
            var vm = new SpaceshipDetailsViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Classification = spaceship.Classification;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Images.AddRange(images);

            return View(vm);

            // minu versioon ->  var vm = await _context.Spaceships
            //                   .FirstOrDefaultAsync(x => x.Id == id);

            // _context.Spaceships.GetService(id);
            // await _context.SaveChangesAsync();
            // return View(vm);

        }


        public async Task<IActionResult> RemoveImage(ImageViewModel vm)
        {
            //tuleb ühendada dto ja vm
            //Id peab saama edastatud andmebaasi
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };

            //kutsu välja vastav serviceclassi meetod
            var image = await _fileServices.RemoveImageFromApi(dto);

            //kui on null, siis vii Index vaatesse
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

    }

}
