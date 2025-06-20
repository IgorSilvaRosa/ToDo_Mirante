using ToDo.Domain.Enums;


namespace ToDo.Domain.Entities
{
    public class Tarefa
    {
        public Tarefa()
        {
            
        }
        public Tarefa(string? titulo, string? descricao, StatusTarefa status, DateTime dataVencimento)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentNullException("Título obrigatório");

            Titulo = titulo;
            Descricao = descricao;
            Status = status;
            DataVencimento = dataVencimento;
        }

        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime DataVencimento { get; set; }



    }
}
