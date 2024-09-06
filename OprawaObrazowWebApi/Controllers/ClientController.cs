using Microsoft.AspNetCore.Mvc;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Services.Interfaces;

namespace OprawaObrazowWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController(IBaseService<Client> service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await service.GetAll());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int key)
    {
        return Ok(await service.GetById(key));
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] Client client)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        service.Create(client);
        return Created("client", client);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(int key, [FromBody] Client client)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await service.Update(key, client);
        return NoContent();
    }
    
    [HttpDelete]
    public IActionResult Delete(int key)
    {
        service.Delete(key);
        return NoContent();
    }
}