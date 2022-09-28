using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Api.Resources;
using MyMusic.Core.Models;
using MyMusic.Core.Services;

namespace MyMusic.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MusicController : ControllerBase
{
    private readonly IMusicService _musicService;
    private readonly IMapper _mapper;

    public MusicController(IMusicService musicService, IMapper mapper)
    {
        _musicService = musicService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MusicResource>>> GetAllMusic()
    {
        var musics = await _musicService.GetAllWithArtist();
        var musicResources = _mapper.Map<IEnumerable<MusicResource>>(musics);
        return Ok(musicResources);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Music>> GetMusicById(int id)
    {
        var music = await _musicService.GetMusicById(id);
        var musicResource = _mapper.Map<MusicResource>(music);
        return Ok(musicResource);
    }
    
    [HttpPost]
    public async Task<ActionResult<MusicResource>> CreateMusic([FromBody] SaveMusicResource saveMusicResource)
    {
        var musicToCreate = _mapper.Map<Music>(saveMusicResource);
        var newMusic = await _musicService.CreateMusic(musicToCreate);
        if (newMusic == null)
            return BadRequest();
        var music = await _musicService.GetMusicById(newMusic.Id);
        var musicResource = _mapper.Map<MusicResource>(music);
        return Ok(musicResource);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MusicResource>> UpdateMusic(int id, [FromBody] SaveMusicResource saveMusicResource)
    {
        if (id == 0)
            return BadRequest("Invalid id");

        var musicToBeUpdate = await _musicService.GetMusicById(id);
        if (musicToBeUpdate == null)
            return NotFound();

        var music = _mapper.Map<Music>(saveMusicResource);
        await _musicService.UpdateMusic(musicToBeUpdate, music);

        var updatedMusic = await _musicService.GetMusicById(id);
        if (updatedMusic == null)
            return NotFound();
        var musicResource = _mapper.Map<MusicResource>(updatedMusic);

        return Ok(musicResource);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMusic(int id)
    {
        if (id == 0)
            return BadRequest("Invalid id");

        var music = await _musicService.GetMusicById(id);

        if (music == null)
            return NotFound();

        await _musicService.DeleteMusic(music);
        return NoContent();
    }
}