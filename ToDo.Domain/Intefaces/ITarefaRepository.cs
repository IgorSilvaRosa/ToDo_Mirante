using ToDo.Domain.Entities;
using ToDo.Domain.Enums;

namespace ToDo.Domain.Intefaces
{
    public interface ITarefaRepository
    {
        Task AddAsync(Tarefa tarefa);
        Task UpdateAsync(Tarefa tarefa);
        Task DeleteAsync(Tarefa tarefa);
        Task<List<Tarefa>> GetAll();
        Task<Tarefa?> GetByIdAsync(int id);
        Task<List<Tarefa>> GetByFilterAsync(StatusTarefa? status, DateTime? dataVencimento);
    }
}
