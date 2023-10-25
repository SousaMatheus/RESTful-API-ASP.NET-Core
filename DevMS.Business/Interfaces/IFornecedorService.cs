using DevMS.Business.Models;

namespace DevMS.Business.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task <bool>Adicionar (Fornecedor fornecedor);
        Task <bool>Atualizar(Fornecedor forncedor);
        Task <bool>AtualizarEndereco (Endereco endereco);
        Task <bool>Remover(Guid id);
    }
}
