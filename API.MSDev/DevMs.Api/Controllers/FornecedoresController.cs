using AutoMapper;
using DevMs.Api.ViewModels;
using DevMS.Business.Interfaces;
using DevMS.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevMs.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper, IFornecedorService fornecedorService)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodosFornecedores()
        {
            var fornecedores = _mapper.Map<IEnumerable< FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());            
            return Ok(fornecedores);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterPorId(id));
            
            if (fornecedor == null) return NotFound();

            return Ok(fornecedor);
        }

        [HttpGet("produto-endereco/{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterFornecedorProdutosEnderecoPorId(Guid id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(id);

            if(fornecedor == null) return NotFound();

            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> AdicionarFornecedor(FornecedorViewModel fornecedorViewModel)
        {
            if(!ModelState.IsValid) return BadRequest();

            if(fornecedorViewModel == null) return BadRequest();

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);
            //adicionar validação e apenas se adicionar foi concluido retornar ok
            return Ok(fornecedor);
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco (Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
