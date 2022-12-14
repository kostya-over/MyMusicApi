using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;

namespace MyMusic.Data.Repositories;

public class ArtistRepository : Repository<Artist>, IArtistRepository
{
    public ArtistRepository(DbContext context) : base(context)
    {
    }

    private MyMusicDbContext MyMusicDbContext
    {
        get {return Context as MyMusicDbContext;}
    }

    public async Task<IEnumerable<Artist>> GetAllByMusicsAsync()
    {
        return await MyMusicDbContext.Artists
            .Include(a => a.Musics)
            .ToListAsync();
    }

    public async Task<Artist> GetWithMusicByIdAsync(int id)
    {
        return await MyMusicDbContext.Artists
            .Include(a => a.Musics)
            .SingleOrDefaultAsync(a => a.Id == id);
    }
}