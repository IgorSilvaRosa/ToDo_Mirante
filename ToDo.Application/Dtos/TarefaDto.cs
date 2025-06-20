using ToDo.Domain.Enums;

namespace ToDo.Application.Dtos
{
    public class TarefaDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
