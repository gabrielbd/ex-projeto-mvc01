using ProjetoAspNetMVC02.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC02.Interfaces
{
    public interface IClienteRepository
    {
        void Inserir(Cliente cliente);
        void Alterar(Cliente cliente);
        void Excluir(Cliente cliente);
        List<Cliente> Consultar();
        List<Cliente> ConsultarPorNome(string nome);
        Cliente ObterPorId(Guid idCliente);
    }
}
