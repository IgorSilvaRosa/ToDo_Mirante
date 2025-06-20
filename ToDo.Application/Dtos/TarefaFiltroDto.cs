using ToDo.Domain.Enums;

namespace ToDo.Application.Dtos
{
    public class TarefaFiltroDto
    {
        public StatusTarefa? Status { get; set; }
        public DateTime? DataVencimento { get; set; }
    }
}
