using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Application.Interfaces;
using School.Application.ViewModels;
using School.Domain.Interfaces;
using School.Identity.Constants;

namespace School.Api.Controllers;

[Authorize]
[ApiController]
[Route("person")]
public class PersonController : Controller
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly ILogger<PersonController> _logger;

    public PersonController(
        IPersonService personService,
        IMapper mapper,
        IUnitOfWork uow,
        ILogger<PersonController> logger)
    {
        _personService = personService;
        _mapper = mapper;
        _uow = uow;
        _logger = logger;
    }

    /// <summary>
    /// Método utilizado para retornar todas as persons
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        Serilog.Log.Information("Obtendo persons.");
        _logger.LogInformation("GetAll People - Teste");
        var persons = await _personService.GetAll();
        Serilog.Log.Error("Testando serilog");
        _logger.LogError("Getted all people - testando");
        return Ok(persons);
    }

    /// <summary>
    /// Método utilizado para realizar a consulta da person pelo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPersonById(int id)
    {
        var person = await _personService.GetById(id);

        if (person == null)
            return BadRequest("Aluno não foi encontrado!");

        return Ok(person);
    }

    /// <summary>
    /// Método utilizado para fazer o registro de uma nova person
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> InsertPerson(PersonViewModel model)
    {
        _personService.Add(model);
        if (await _uow.Commit())
            return Created($"/person/{model.Id}", model);

        return BadRequest("Person não cadastrada!");
    }

    /// <summary>
    /// Método utilizado para altear uma person.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AlterPerson(int id, PersonViewModel model)
    {
        var person = await _personService.GetById(id);

        if (person is null)
            return BadRequest("Person não encontrada!");

        _mapper.Map(model, person);

        _personService.Update(person);
        if (await _uow.Commit())
            return Created($"person/{model.Id}", person);

        return BadRequest("Erro ao alterar person!");
    }

    /// <summary>
    /// Método utilizado para excluir uma person
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Policy = Policies.BussinessHours)]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await _personService.GetById(id);

        if (person == null)
            return BadRequest("Person não encontrada!");

        _personService.Remove(person);
        if (await _uow.Commit())
            return Ok("Person deletada com sucesso!");

        return BadRequest("Erro ao deletar person!");
    }
}