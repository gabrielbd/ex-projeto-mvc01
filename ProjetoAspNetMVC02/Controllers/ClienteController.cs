using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC02.Entities;
using ProjetoAspNetMVC02.Interfaces;
using ProjetoAspNetMVC02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC02.Controllers
{
    public class ClienteController : Controller
    {
        //atributo
        private IClienteRepository _clienteRepository;

        //construtor para inicializar o atributo da classe
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        //método que renderiza a página de cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] //indica que o método irá receber os dados do SUBMIT
        public IActionResult Cadastro(ClienteCadastroModel model)
        {
            //verificando se o conteudo da model esta correto!
            if (ModelState.IsValid)
            {
                try
                {
                    //criando um objeto da classe de entidade
                    var cliente = new Cliente();

                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;

                    //mandando gravar no banco de dados
                    _clienteRepository.Inserir(cliente);

                    //gerando uma mensagem para exibir na página
                    TempData["Mensagem"] = $"Cliente {cliente.Nome}, cadastrado com sucesso.";
                    //limpar os campos do formulário
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }

        //método que renderiza a página de consulta
        public IActionResult Consulta()
        {
            try
            {
                //executando a consulta de clientes no banco de dados
                //e armazenando os dados em uma variavel TempData
                TempData["Consulta"] = _clienteRepository.Consultar();
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }

        //método executado pelo formulário da página de consulta de clientes
        //este método é chamado quando o usuario clica no botão SUBMIT
        [HttpPost]
        public IActionResult Consulta(ClienteConsultaModel model)
        {
            try
            {
                //verificar se todos os campos da classe model
                //estão validados com sucesso (se não há erros de validação)
                if (ModelState.IsValid)
                {
                    TempData["Consulta"] = _clienteRepository.ConsultarPorNome(model.Nome);
                }
                else
                {
                    //retornando para a página todos os clientes
                    TempData["Consulta"] = _clienteRepository.Consultar();
                }
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }

        //método para ser chamado pelo link de exclusão de cliente
        //este método recebe o id do cliente enviado pelo link
        public IActionResult Exclusao(Guid id)
        {
            try
            {
                //buscar no banco de dados o cliente atraves do id..
                var cliente = _clienteRepository.ObterPorId(id);
                //excluindo o cliente
                _clienteRepository.Excluir(cliente);

                TempData["Mensagem"] = $"Cliente {cliente.Nome} excluído com sucesso.";
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            //redirecionar para a página de consulta..
            return RedirectToAction("Consulta");
        }

        //método para abrir a página de edição
        //já abre a página exibindo o formulario
        //preenchido com os dados do cliente selecionado
        public IActionResult Edicao(Guid id)
        {
            //criando um objeto da classe model
            var model = new ClienteEdicaoModel();

            try
            {
                //buscar o cliente no banco de dados atraves do id..
                var cliente = _clienteRepository.ObterPorId(id);

                //transferindo os dados do cliente para o model
                model.IdCliente = cliente.IdCliente;
                model.Nome = cliente.Nome;
                model.Email = cliente.Email;
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View(model); //segue para a página..
        }

        [HttpPost] //captura o SUBMIT do formulario (envio dos dados)
        public IActionResult Edicao(ClienteEdicaoModel model)
        {
            //verificando se não há erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    var cliente = new Cliente();

                    cliente.IdCliente = model.IdCliente;
                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;

                    _clienteRepository.Alterar(cliente);

                    TempData["Mensagem"] = "Dados alterados com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }
    }
}


