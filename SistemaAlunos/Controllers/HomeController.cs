using Microsoft.AspNetCore.Mvc;
using SistemaAlunos.Models;
using SistemaAlunos.Models.Domain;
using SistemaAlunos.Repository;
using System.Diagnostics;

namespace SistemaAlunos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RepositorioAluno repositorioAluno = new RepositorioAluno();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }

        public IActionResult Index()
        {
            var aluno = repositorioAluno.GetAll();
            return View(aluno);
        }

        
        public IActionResult Apagar(int matricula)
        {
            repositorioAluno.Remove(matricula);
            return RedirectToAction("Index");
        }

        
        public IActionResult Editar(int matricula)
        {
            Aluno aluno = repositorioAluno.Get(matricula).FirstOrDefault();

            return View("Cadastro",aluno);
        }

  
        public IActionResult Cadastro(Aluno aluno)
        {
            
            try
            {

                if (ModelState.IsValid)
                {


                     repositorioAluno.Add(aluno);
                    
                    return RedirectToAction("Cadastro");
                }

                return View();
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o aluno, tente novamente , detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}