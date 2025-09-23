using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.Kindergarten;


namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly IKindergartenServices _kindergartenServices;

        public KindergartenController
            (
                ShopTARge24Context context,
                IKindergartenServices kindergartenServices
            )
        {
            _context = context;
            _kindergartenServices = kindergartenServices;
        }
        public IActionResult Index()
        {
            var result = _context.Kindergarten
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    KindergartenName = x.KindergartenName,
                    GroupName = x.GroupName,
                    TeacherName = x.TeacherName,
                    ChildrenCount = x.ChildrenCount,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KindergartenCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }



        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View("CreateUpdate", vm);
            }
            var dto = new KindergartenDto
            {
                Id = Guid.NewGuid(),
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt
            };

            await _kindergartenServices.Create(dto);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var vm = new KindergartenCreateUpdateViewModel();

            vm.Id = kindergarten.Id;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.GroupName = kindergarten.GroupName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.ChildrenCount = kindergarten.ChildrenCount;
     
            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;
                
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View("CreateUpdate", vm);
            }

            var dto = new KindergartenDto
            {
                Id = vm.Id,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt
            };

            await _kindergartenServices.Update(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            var vm = new KindergartenDeleteViewModel();

            vm.Id = kindergarten.Id;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.GroupName = kindergarten.GroupName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.ChildrenCount = kindergarten.ChildrenCount;

            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            await _kindergartenServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            //kasutada service classi meetodit, et info kätte saada
            var kindergarten = await _kindergartenServices.DetailAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            //toimub viewModeliga mappimine
            var vm = new KindergartenDetailsViewModel();

            vm.Id = kindergarten.Id;
            vm.KindergartenName = kindergarten.KindergartenName;
            vm.GroupName = kindergarten.GroupName;
            vm.TeacherName = kindergarten.TeacherName;
            vm.ChildrenCount = kindergarten.ChildrenCount;

            vm.CreatedAt = kindergarten.CreatedAt;
            vm.UpdatedAt = kindergarten.UpdatedAt;

            return View(vm);
        }
    }
}