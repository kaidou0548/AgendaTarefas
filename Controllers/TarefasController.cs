using AgendaTarefasVitor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTarefasVitor.Controllers
{
    public class TarefasController : Controller
    {
        private readonly Contexto _contexto;

        public TarefasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return View(PegarDatas());
        }

        private List<DatasViewModel> PegarDatas()
        {
            DateTime  dataAtual = DateTime.Now;
            DateTime dataLimite = DateTime.Now.AddDays(10);
            int qtdDias = 0;
            DatasViewModel data;
            List<DatasViewModel> ListaDatas = new List<DatasViewModel>();

            while(dataAtual < dataLimite)
            {
                data = new DatasViewModel();
                data.Datas = dataAtual.ToShortDateString();
                data.Identificadores = "collapse" + dataAtual.ToShortDateString().Replace("/", "");
                ListaDatas.Add(data);
                qtdDias++;
                dataAtual = DateTime.Now.AddDays(qtdDias);
            }
            return ListaDatas;
        }

        [HttpGet]
        public IActionResult CriarTarefa(string dataTarefa)
        {
            Tarefa tarefa = new Tarefa
            {
                Data = dataTarefa
            };
            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa(Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                _contexto.Tarefas.Add(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
               return View(tarefa);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarTarefas(int tarefaId)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(tarefaId);

            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarTarefas(Tarefa tarefa)
        {
            if(ModelState.IsValid)
            {
                _contexto.Update(tarefa);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        [HttpPost]
        public async Task<JsonResult> ExcluirTarefa(int tarefaId)
        {
            Tarefa tarefa = await _contexto.Tarefas.FindAsync(tarefaId);
            _contexto.Tarefas.Remove(tarefa);
            await _contexto.SaveChangesAsync();
            return Json(true);
        }
    }
}
