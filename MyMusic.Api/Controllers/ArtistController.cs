using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Api.Resources;
using MyMusic.Core.Models;
using MyMusic.Core.Services;

namespace MyMusic.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistController : ControllerBase
{
    private readonly IArtistService _artistService;
    private readonly IMapper _mapper;

    public ArtistController(IArtistService artistService, IMapper mapper)
    {
        _artistService = artistService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArtistResource>>> GetAllArtists()
    {
        var artists = await _artistService.GetAllArtists();
        var artistResources = _mapper.Map<IEnumerable<ArtistResource>>(artists);
        return Ok(artistResources);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ArtistResource>>> GetArtistById(int id)
    {
        var artist = await _artistService.GetArtistById(id);
        var artistResources = _mapper.Map<ArtistResource>(artist);
        return Ok(artistResources);
    }

    [HttpPost]
    public async Task<ActionResult<ArtistResource>> CreateArtist([FromBody] SaveArtistResource saveArtistResource)
    {
        var artistToCreate = _mapper.Map<Artist>(saveArtistResource);
        var newArtist = await _artistService.CreateArtist(artistToCreate);

        if (newArtist == null)
            return BadRequest();

        var artist = await _artistService.GetArtistById(newArtist.Id);
        var artistResource = _mapper.Map<ArtistResource>(artist);
        return Ok(artistResource);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ArtistResource>> UpdateArtist(int id, [FromBody] SaveArtistResource saveArtistResource)
    {
        if (id == 0)
            return BadRequest("Invalid id");
        
        var artistToBeUpdated = await _artistService.GetArtistById(id);
        if (artistToBeUpdated == null)
            return NotFound();
        
        var artist = _mapper.Map<Artist>(saveArtistResource);
        await _artistService.UpdateArtist(artistToBeUpdated, artist);

        var updatedArtist = await _artistService.GetArtistById(id);
        var artistResource = _mapper.Map<ArtistResource>(updatedArtist);
        return Ok(artistResource);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteArtist(int id)
    {
        var artistToDelete = await _artistService.GetArtistById(id);
        await _artistService.DeleteArtist(artistToDelete);
        return NoContent();
    }
}