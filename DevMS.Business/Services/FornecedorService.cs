using DevMS.Business.Interfaces;
using DevMS.Business.Models;
using DevMS.Business.Models.Validations;

namespace DevMS.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, 
                                 IEnderecoRepository enderecoRepository, 
                                 INotificador notificador) : base (notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task <bool> Adicionar(Fornecedor fornecedor)
        {
            if(!ExecutarValidacao(new FornecedorValidation(), fornecedor) ||
               !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return false;

            if(_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com o documento informado!");
                return false;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
            return true;
        }

        public async Task <bool> Atualizar(Fornecedor fornecedor)
        {
            if(!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return false;

            if(_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com o documento informado!");
                return false;
            }

            await _fornecedorRepository.Atualizar(fornecedor);
            return true;
        }

        public async Task <bool> AtualizarEndereco(Endereco endereco)
        {
            if(!ExecutarValidacao(new EnderecoValidation(), endereco)) return false;

            await _enderecoRepository.Atualizar(endereco);
            return true;
        }

        public async Task <bool> Remover(Guid id)
        {
            if(_fornecedorRepository.Buscar(f => f.Id == id).Result.Any())
            {
                Notificar("O fornecedor tem produtos cadastrados!");
                return false;
            }

            await _fornecedorRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _enderecoRepository?.Dispose();
            _fornecedorRepository?.Dispose();
        }
    }
}
