using Moq;
using ToDo.Application.Dtos;
using ToDo.Application.UseCases;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;
using ToDo.Domain.Intefaces;

namespace ToDo.Test.Application
{
    public class TarefaServiceTest
    {
        #region Add Tarefa
        [Fact]
        public async Task AddTarefa_DeveRetornarOK()
        {
            var agora = DateTime.Now;
            var tarefaDto = new TarefaDto
            {
                Titulo = "Tarefa 1",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.Pendente,
                DataVencimento = agora
            };

            var mockRepository = new Mock<ITarefaRepository>();
            var mockService = new Mock<ITarefaService>();

            mockService.Setup(x => x.AddAsync(tarefaDto)).ReturnsAsync(tarefaDto);

            var tarefaService = new TarefaService(mockRepository.Object);

            var resultado = await tarefaService.AddAsync(tarefaDto);

            Assert.Equal(tarefaDto.Titulo, resultado.Titulo);
            Assert.Equal(tarefaDto.Descricao, resultado.Descricao);
            Assert.Equal(tarefaDto.Status, resultado.Status);
            Assert.Equal(tarefaDto.DataVencimento, resultado.DataVencimento);
        }

        [Fact]
        public async Task AddTarefa_TituloVazio_DeveRetornarArgumentNullException()
        {
            var agora = DateTime.Now;
            var tarefaDto = new TarefaDto
            {
                Titulo = "",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.Pendente,
                DataVencimento = agora
            };

            var mockRepository = new Mock<ITarefaRepository>();
            var mockService = new Mock<ITarefaService>();

            mockService.Setup(x => x.AddAsync(tarefaDto)).ReturnsAsync(tarefaDto);

            var tarefaService = new TarefaService(mockRepository.Object);

            var resultado = await Assert.ThrowsAsync<ArgumentNullException>(() => tarefaService.AddAsync(tarefaDto));

            Assert.Contains("Título obrigatório", resultado.Message);
           
        }

        #endregion

        #region update Tarefa
        [Fact]
        public async Task UpdateTarefa_DeveRetornarOK()
        {
           var mockRepository = new Mock<ITarefaRepository>();
           var _service = new TarefaService(mockRepository.Object);

            var tarefaExistente = new Tarefa
            {
                Id = 1,
                Titulo = "Original",
                Descricao = "Desc",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Today
            };

            var dto = new TarefaDto
            {
                Titulo = "Atualizado",
                Descricao = "Nova descrição",
                Status = StatusTarefa.Concluído,
                DataVencimento = DateTime.Today.AddDays(3)
            };

            mockRepository.Setup(r => r.GetByIdAsync(1))
                           .ReturnsAsync(tarefaExistente);

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Tarefa>()))
                           .Returns(Task.CompletedTask);

            await _service.UpdateAsync(1, dto);


            mockRepository.Verify(r => r.UpdateAsync(It.Is<Tarefa>(t =>
                t.Titulo == dto.Titulo &&
                t.Descricao == dto.Descricao &&
                t.Status == dto.Status &&
                t.DataVencimento == dto.DataVencimento
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateTarefa_TarefaNaoEncontrada_DeveRetornarException()
        {
            var mockRepository = new Mock<ITarefaRepository>();
            var _service = new TarefaService(mockRepository.Object);

            mockRepository.Setup(r => r.GetByIdAsync(99))
                      .ReturnsAsync((Tarefa?)null);

            var dto = new TarefaDto
            {
                Titulo = "Teste",
                Descricao = "Teste",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Today
            };
           
            await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(99, dto));

        }
        #endregion

        #region delete Tarefa
        [Fact]
        public async Task DeleteTarefa_DeveRetornarOK()
        {
            var mockRepository = new Mock<ITarefaRepository>();
            var _service = new TarefaService(mockRepository.Object);
           
            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa para deletar" };          

            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tarefa);                          

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);

            await _service.DeleteAsync(1);

            mockRepository.Verify(r => r.DeleteAsync(tarefa), Times.Once);
        }

        [Fact]
        public async Task DeleteTarefa_TarefaNaoEncontrada_DeveRetornarException()
        {
            var mockRepository = new Mock<ITarefaRepository>();
            var _service = new TarefaService(mockRepository.Object);

            mockRepository.Setup(r => r.GetByIdAsync(99))
                      .ReturnsAsync((Tarefa?)null);           

            await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(99));

        }
        #endregion

        #region Lista Tarefa
        [Fact]
        public async Task ListaTarefa_DeveRetornarOK()
        {
            var mockRepository = new Mock<ITarefaRepository>();
            var _service = new TarefaService(mockRepository.Object);

            var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa para deletar" };

            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tarefa);

            await _service.GetByIdAsync(1);

            mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        #endregion

    }
}
