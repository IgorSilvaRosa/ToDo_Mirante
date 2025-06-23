using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Dtos;
using ToDo.Domain.Enums;
using ToDo.Domain.Intefaces;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefas(int pageNumer = 1, int pageSize = 5)
        {
            var listTarefas = await _tarefaService.GetAll();

          var resultado =  listTarefas.Skip((pageNumer - 1) * pageSize)
                .Take(pageSize);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> AddTarefa([FromBody] TarefaDto dto)
        {
            if (!Enum.IsDefined(typeof(StatusTarefa), dto.Status))
                return BadRequest("Status inválido.");

            if (string.IsNullOrEmpty(dto.Titulo))
                return BadRequest("Título obrigatório");

            if (dto.DataVencimento < DateTime.Now)
                return BadRequest("Favor não inserir data retroativa");


            var tarefa = await _tarefaService.AddAsync(dto);

            return Created(string.Empty, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarefa(int id, [FromBody] TarefaDto dto)
        {
            try
            {
                if (!Enum.IsDefined(typeof(StatusTarefa), dto.Status))
                    return BadRequest("Status inválido.");

                if (dto == null)
                    return BadRequest("Dados inválidos.");

                if(id <= 0)
                    return BadRequest("Insira um Id maior que zero.");

                if(dto.Id != id)
                    return BadRequest($"Os Ids estão divergentes id:{id} e TarefaDto: {dto.Id}");

                if (string.IsNullOrEmpty(dto.Titulo))
                    return BadRequest("Favor inserir o título da tarefa.");                

                await _tarefaService.UpdateAsync(id, dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Insira um Id maior que zero.");

                await _tarefaService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("filtro")]
        public async Task<IActionResult> FiltrarTarefas([FromQuery] TarefaFiltroDto filtro)
        {
            var tarefas = await _tarefaService.GetByFilterAsync(filtro);           

            return Ok(tarefas);
        }

    }
}
