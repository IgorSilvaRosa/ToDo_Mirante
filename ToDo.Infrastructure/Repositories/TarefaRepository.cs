using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;
using ToDo.Domain.Intefaces;
using ToDo.Infrastructure.Data;

namespace ToDo.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly BancoContext _bancoContext;

        public TarefaRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public async Task AddAsync(Tarefa tarefa)
        {
            _bancoContext.Tarefas.Add(tarefa);
            await _bancoContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tarefa tarefa)
        {
            _bancoContext.Tarefas.Remove(tarefa);
            await _bancoContext.SaveChangesAsync();
        }

        public Task<List<Tarefa>> GetAll()
        {
            return _bancoContext.Tarefas.ToListAsync();
        }

        public async Task<List<Tarefa>> GetByFilterAsync(StatusTarefa? status, DateTime? dataVencimento)
        {
            var query = _bancoContext.Tarefas.AsQueryable();

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (dataVencimento.HasValue)
                query = query.Where(x => x.DataVencimento.Date == dataVencimento.Value.Date);

            return await query.ToListAsync();
        }

        public async Task<Tarefa?> GetByIdAsync(int id)
        {
            return await _bancoContext.Tarefas
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Tarefa tarefa)
        {
            _bancoContext.Tarefas.Update(tarefa);
            await _bancoContext.SaveChangesAsync();
        }
    }
}
