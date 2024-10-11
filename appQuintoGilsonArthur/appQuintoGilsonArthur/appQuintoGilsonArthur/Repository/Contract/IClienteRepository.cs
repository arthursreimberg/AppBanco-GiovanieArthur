using appQuintoGiovaniArthur.Models;

namespace appQuintoGiovaniArthur.Repository.Contract
{
    public interface IClienteRepository
    {
        //CRUD
        IEnumerable<Cliente> ObterTodosClientes();

        void Cadastrar(Cliente cliente);

        void Atualizar(Cliente cliente);

        Cliente ObterCliente(int Id);

        void Excluir(int id);
    }
}
