using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ToDo.Application.Dtos;
using ToDo.Application.UseCases;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;
using ToDo.Domain.Intefaces;

namespace ToDo.Test.Domain
{
    public class TarefaTest
    {
        [Fact]
        public void CriarObjetoTarefa_DeveRetornarOK()
        {

            var tarefa = new Tarefa
            (
                "Tarefa 1",
                "Descrição Tarefa 1",
                StatusTarefa.Pendente,
                DateTime.Now
            );

            Assert.NotNull(tarefa);
            Assert.Equal("Tarefa 1", tarefa.Titulo);
            Assert.Equal("Descrição Tarefa 1", tarefa.Descricao);
            Assert.Equal(StatusTarefa.Pendente, tarefa.Status);           
        }

        [Fact]
        public void CriarObjetoTarefa_TituloVazio_DeveRetornarArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new Tarefa(
                    "",
                    "Descrição Tarefa 1",
                    StatusTarefa.Pendente,
                    DateTime.Now
                )
            );

            Assert.Contains("Título obrigatório", ex.Message);
        }
    }
}
