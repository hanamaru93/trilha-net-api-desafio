using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using Microsoft.EntityFrameworkCore;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ProcurarTarefaPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            
            var tarefa = _context.Tarefas.Where(x => x.Id == id).First();

            if (tarefa == null)
            {

                return NotFound();

            }

            return Ok(tarefa);
        }

        [HttpGet("ListarTarefas")]
        public IActionResult ListarTodasAsTarefas()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefas = _context.Tarefas.ToList();

            if (tarefas == null)
            {

                return NotFound();
                
            }

            return Ok(tarefas);
        }

        [HttpGet("ProcurarTarefasPorTitulo")]
        public IActionResult ProcurarTarefasPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Titulo == titulo);

            if(tarefa == null)
            {

                return NotFound();

            }

            return Ok(tarefa);
        }

        [HttpGet("ProcurarTarefasPorData/{data}")]
        public IActionResult ProcurarTarefasPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);

            if(tarefa == null)
            {

                return NotFound();

            }

            return Ok(tarefa);
        }

        [HttpGet("ProcurarTarefaPorStatus")]
        public IActionResult ProcurarTarefaPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            
            if(tarefa == null)
            {

                return NotFound();

            }

            return Ok(tarefa);

        }

        [HttpPost]
        public IActionResult CriarTarefa(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Tarefas.Add(tarefa);

            _context.SaveChanges();
            return CreatedAtAction(nameof(ProcurarTarefaPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarTarefa(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
            {
             
                return NotFound();

            }    

            if (tarefa.Data == DateTime.MinValue){

                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            }

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            tarefa.Titulo = tarefaBanco.Titulo;
            tarefa.Descricao = tarefaBanco.Descricao;
            tarefa.Data = tarefaBanco.Data;
            tarefa.Status = tarefaBanco.Status;

            _context.SaveChanges();


            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Where(y => y.Id == id).First();

            if (tarefa == null)
            {

                return NotFound();

            }   
            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
