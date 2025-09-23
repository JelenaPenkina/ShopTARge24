using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;


namespace ShopTARge24.ApplicationServices.Services
{
        public class KindergartenServices : IKindergartenServices
        {
            private readonly ShopTARge24Context _context;

            public KindergartenServices
                (
                    ShopTARge24Context context
                )
            {
                _context = context;
            }

            public async Task<Kindergarten> Create(KindergartenDto dto)
            {
                var kindergarten = new Kindergarten
                {
                    Id = Guid.NewGuid(),
                    GroupName = dto.GroupName,
                    ChildrenCount = dto.ChildrenCount,
                    KindergartenName = dto.KindergartenName,
                    TeacherName = dto.TeacherName,

                    CreatedAt = dto.CreatedAt,  // DateTime - kui kaustada sellist meetodit, siis kasutaja ei saa ise midagi sisestada, kui see on soovitatav
                    UpdatedAt = dto.UpdatedAt
                };

                await _context.Kindergarten.AddAsync(kindergarten);
                await _context.SaveChangesAsync();

                return kindergarten;
            }

            public async Task<Kindergarten> Update(KindergartenDto dto)
            {
                var kindergarten = await _context.Kindergarten.FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (kindergarten == null)
                {
                    return null; // või throw exception saab kasutada ka siin
                }

                kindergarten.KindergartenName = dto.KindergartenName;
                kindergarten.GroupName = dto.GroupName;
                kindergarten.TeacherName = dto.TeacherName;
                kindergarten.ChildrenCount = dto.ChildrenCount;

                kindergarten.CreatedAt = dto.CreatedAt;
                kindergarten.UpdatedAt = dto.UpdatedAt;

            
                _context.Kindergarten.Update(kindergarten);
                await _context.SaveChangesAsync();

                return kindergarten;
            }

            public async Task<Kindergarten> DetailAsync(Guid id)
            {
                var result = await _context.Kindergarten
                    .FirstOrDefaultAsync(x => x.Id == id);

                return result;
            }

            public async Task<Kindergarten> Delete(Guid id)
            {
                var kindergarten = await _context.Kindergarten.FirstOrDefaultAsync(x => x.Id == id);

                if (kindergarten == null)
                {
                    return null;
                }

                _context.Kindergarten.Remove(kindergarten);
                await _context.SaveChangesAsync();

                return kindergarten;
            }
        }
    }
