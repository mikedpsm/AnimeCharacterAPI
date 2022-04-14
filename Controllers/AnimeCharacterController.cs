using Microsoft.AspNetCore.Mvc;

namespace AnimeCharacterAPI.Controllers
{
    [Route("api/[controller]")]
    public class AnimeCharacterController : Controller
    {
        private static List<AnimeCharacter> characters = new List<AnimeCharacter>
            {
                
            };
        private readonly DataContext dataContext;

        public AnimeCharacterController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimeCharacter>>> Get()
        {
            return Ok(await this.dataContext.AnimeCharacters.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeCharacter>> Get(int id)
        {
            var character = await this.dataContext.AnimeCharacters.FindAsync(id);
            if (character == null)
                return BadRequest("Character not found.");
            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<AnimeCharacter>>> AddHero(AnimeCharacter character)
        {
            this.dataContext.AnimeCharacters.Add(character);
            await this.dataContext.SaveChangesAsync();
            return Ok(await this.dataContext.AnimeCharacters.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<AnimeCharacter>>> UpdateHero(AnimeCharacter request)
        {
            var dbCharacter = await this.dataContext.AnimeCharacters.FindAsync(request.Id);
            if (dbCharacter == null)
                return BadRequest("Character not found.");

            dbCharacter.Name = request.Name;
            dbCharacter.FirstName = request.FirstName;
            dbCharacter.LastName = request.LastName;
            dbCharacter.Place = request.Place;

            await this.dataContext.SaveChangesAsync();

            return Ok(await this.dataContext.AnimeCharacters.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<AnimeCharacter>>> Delete(int id)
        {
            var dbCharacter = await this.dataContext.AnimeCharacters.FindAsync(id);
            if (dbCharacter == null)
                return BadRequest("Character not found.");

            this.dataContext.AnimeCharacters.Remove(dbCharacter);
            await this.dataContext.SaveChangesAsync();

            return Ok(await this.dataContext.AnimeCharacters.ToListAsync());
        }
    }

}
