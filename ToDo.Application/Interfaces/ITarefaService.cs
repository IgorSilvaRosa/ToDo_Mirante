using ToDo.Application.Dtos;

namespace ToDo.Domain.Intefaces
{
    public interface ITarefaService
    {
        Task<TarefaDto> AddAsync(TarefaDto dto);
        Task UpdateAsync(int id, TarefaDto dto);
        Task DeleteAsync(int id);
        Task<List<TarefaDto>> GetAll();
        Task<TarefaDto> GetByIdAsync(int id);
        Task<List<TarefaDto>> GetByFilterAsync(TarefaFiltroDto filtro);
    }
}
