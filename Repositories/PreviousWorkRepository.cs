using AnnapurnaEnterprises.Api.Data;
using AnnapurnaEnterprises.Api.Models;
using AnnapurnaEnterprises.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnnapurnaEnterprises.Api.Repositories;

public class PreviousWorkRepository : GenericRepository<PreviousWork>, IPreviousWorkRepository
{
    public PreviousWorkRepository(AppDbContext db) : base(db) { }

    public async Task<List<PreviousWork>> GetAllWithImagesAsync()
    {
        return await _db.PreviousWorks
            .Include(w => w.Images)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<PreviousWork?> GetByIdWithImagesAsync(int id)
    {
        return await _db.PreviousWorks
            .Include(w => w.Images)
            .FirstOrDefaultAsync(w => w.Id == id);
    }
}