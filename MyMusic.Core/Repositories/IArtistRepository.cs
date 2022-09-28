﻿using MyMusic.Core.Models;

namespace MyMusic.Core.Repositories;

public interface IArtistRepository : IRepository<Artist>
{
    Task<IEnumerable<Artist>> GetAllByMusicsAsync();
    Task<Artist> GetWithMusicByIdAsync(int id);
}