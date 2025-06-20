using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDo.API.Controllers;
using ToDo.Application.Dtos;
using ToDo.Domain.Enums;
using ToDo.Domain.Intefaces;

namespace ToDo.Test.API
{
    public class TarefaControllerTest
    {
        #region Add Tarefa
        [Fact]
        public async Task AddTarefa_DeveRetornarOK()
        {
            var tarefaDto = new TarefaDto
            {
                Titulo = "Tarefa 1",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Now
            };

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.AddAsync(tarefaDto)).ReturnsAsync(tarefaDto);

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.AddTarefa(tarefaDto);

            Assert.IsType<CreatedResult>(resultado);
        }

        [Fact]
        public async Task AddTarefa_TituloVazio_DeveRetornarBadRequest()
        {
            var tarefaDto = new TarefaDto
            {
                Titulo = "",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Now
            };

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.AddAsync(tarefaDto)).ReturnsAsync(tarefaDto);

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.AddTarefa(tarefaDto);
            var result = Assert.IsType<BadRequestObjectResult>(resultado);

            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Título obrigatório", result.Value);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task AddTarefa_StatusIncorreto_DeveRetornarBadRequest()
        {
            var tarefaDto = new TarefaDto
            {
                Titulo = "Tarefa",
                Descricao = "Descrição Tarefa ",
                Status = (StatusTarefa)10,
                DataVencimento = DateTime.Now
            };

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.AddAsync(tarefaDto)).ReturnsAsync(tarefaDto);

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.AddTarefa(tarefaDto);
            var result = Assert.IsType<BadRequestObjectResult>(resultado);

            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Status inválido.", result.Value);
            Assert.Equal(400, result.StatusCode);
        }

        #endregion

        #region Update tarefa
        [Fact]
        public async Task UpdateTarefa_DeveRetornarOK()
        {
            int Id = 1;
            var tarefaDto = new TarefaDto
            {
                Id = 1,
                Titulo = "Tarefa 1",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.EmAndamento,
                DataVencimento = DateTime.Now
            };

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.UpdateAsync(Id, tarefaDto));

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.UpdateTarefa(1,tarefaDto);

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task UpdateTarefa_TituloVazio_DeveRetornarBadRequest()
        {
            var tarefaDto = new TarefaDto
            {
                Id = 1,
                Titulo = "",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Now
            };

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.UpdateAsync(1, tarefaDto));

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.UpdateTarefa(1, tarefaDto);
            var result = Assert.IsType<BadRequestObjectResult>(resultado);

            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Favor inserir o título da tarefa.", result.Value);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task UpdateTarefa_IdsDivergentes_DeveRetornarBadRequest()
        {
            int Id = 1;
            var tarefaDto = new TarefaDto
            {
                Id = 10,
                Titulo = "",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Now
            };

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.UpdateAsync(1, tarefaDto));

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.UpdateTarefa(1, tarefaDto);
            var result = Assert.IsType<BadRequestObjectResult>(resultado);

            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal($"Os Ids estão divergentes id:{Id} e TarefaDto: {tarefaDto.Id}", result.Value);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task UpdateTarefa_IdIgualAZero_DeveRetornarBadRequest()
        {
            int Id = 0;
            var tarefaDto = new TarefaDto
            {
                Id = 1,
                Titulo = "Tarefa 1",
                Descricao = "Descrição Tarefa 1",
                Status = StatusTarefa.Pendente,
                DataVencimento = DateTime.Now
            };

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.UpdateAsync(Id, tarefaDto));

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.UpdateTarefa(Id, tarefaDto);
            var result = Assert.IsType<BadRequestObjectResult>(resultado);

            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Insira um Id maior que zero.", result.Value);
            Assert.Equal(400, result.StatusCode);
        }
        #endregion

        #region Delete Tarefa
        [Fact]
        public async Task DeleteTarefa_DeveRetornarOK()
        {           
            int Id = 1;

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.DeleteAsync(Id));

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.DeleteTarefa(Id);

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task DeleteTarefa_IdIgualAZero_DeveRetornarBadRequest()
        {
            int Id = 0;         

            var mockService = new Mock<ITarefaService>();
            mockService.Setup(x => x.DeleteAsync(Id));

            var controller = new TarefaController(mockService.Object);

            var resultado = await controller.DeleteTarefa(Id);
            var result = Assert.IsType<BadRequestObjectResult>(resultado);

            Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Insira um Id maior que zero.", result.Value);
            Assert.Equal(400, result.StatusCode);
        }
        #endregion
    }
}
