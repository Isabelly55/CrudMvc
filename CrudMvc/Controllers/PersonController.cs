using CrudMvc.Models;
using CrudMvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace CrudMvc.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly PessoaDao dao = PessoaDao.getInstance();

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            List<Pessoa> lista = dao.GetAll();
            return View(lista);
        }

        public IActionResult Delete(int id)
        {
            Pessoa pessoa = dao.BuscarPessoa(id);
            dao.Excluir(pessoa);
            return RedirectToAction(nameof(Index));
        }

        #region Criar
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nome,Telefone,Email")] Pessoa pessoa)
        {
            dao.Cadastrar(pessoa);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Editar
        public IActionResult Edit(int id)
        {
            Pessoa pessoa = dao.BuscarPessoa(id);
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Telefone,Email")] Pessoa pessoa)
        {
            dao.Alterar(pessoa);
            return RedirectToAction(nameof(Index));
        }
        #endregion

       
    }
}
