using ToDo.Application.Dtos;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;
using ToDo.Domain.Intefaces;

namespace ToDo.Application.UseCases
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;

        public TarefaService(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public async Task<TarefaDto> AddAsync(TarefaDto dto)
        {
            var tarefa = new Tarefa
            (
                 dto.Titulo,
                 dto.Descricao,
                 dto.Status,
                 dto.DataVencimento
            );

            await _repository.AddAsync(tarefa);

            return new TarefaDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                DataVencimento = tarefa.DataVencimento
            };
        }

        public async Task DeleteAsync(int Id)
        {
            var tarefa = await _repository.GetByIdAsync(Id);

            if (tarefa == null)
                throw new Exception($"Tarefa com Id {Id} não encontrada.");

            await _repository.DeleteAsync(tarefa);
        }

        public async Task<List<TarefaDto>> GetAll()
        {
            var tarefas = await _repository.GetAll();

            List<TarefaDto> listTarefaDtos = new List<TarefaDto>();

            foreach (var tarefa in tarefas)
            {
                listTarefaDtos.Add(new TarefaDto
                {
                    Id = tarefa.Id,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    Status = tarefa.Status,
                    DataVencimento = tarefa.DataVencimento
                });
            }

            return listTarefaDtos;
        }

        public async Task<List<TarefaDto>> GetByFilterAsync(TarefaFiltroDto filtro)
        {
            var tarefas = await _repository.GetByFilterAsync(filtro.Status, filtro.DataVencimento);

            return tarefas.Select(x => new TarefaDto
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                Status = x.Status,
                DataVencimento = x.DataVencimento
            }).ToList();
        }

        public async Task<TarefaDto> GetByIdAsync(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);

            if (tarefa is null)
                throw new Exception($"Tarefa com ID {id} não encontrada.");

            return new TarefaDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                DataVencimento = tarefa.DataVencimento
            };
        }

        public async Task UpdateAsync(int id, TarefaDto dto)
        {            
            var tarefa = await _repository.GetByIdAsync(id);

            if (tarefa is null)
                throw new Exception($"Tarefa com o Id {id} não encontrado");
            
                tarefa.Titulo = dto.Titulo;
                tarefa.Descricao = dto.Descricao;
                tarefa.Status = dto.Status;
                tarefa.DataVencimento = dto.DataVencimento;

                await _repository.UpdateAsync(tarefa);
        }
    }
}
