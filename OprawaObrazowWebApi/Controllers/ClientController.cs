using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Services.Interfaces;

namespace OprawaObrazowWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController(IBaseService<Client> service) : ControllerBase
{
    [EnableQuery]
    [HttpGet]
    public IQueryable<Client> Get()
    {
        return service.GetAll();
    }
    
    [EnableQuery]
    [HttpGet("{id}")]
    public SingleResult<Client> Get([FromODataUri] int key)
    {
        return SingleResult.Create(service.GetById(key));
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
    public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Client client)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await service.Update(key, client);
        return NoContent();
    }
    
    [HttpDelete]
    public IActionResult Delete([FromODataUri] int key)
    {
        service.Delete(key);
        return NoContent();
    }
}