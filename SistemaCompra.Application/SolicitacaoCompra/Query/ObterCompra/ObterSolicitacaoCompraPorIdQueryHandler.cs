using MediatR;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Linq;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;


namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterCompra
{
    public class ObterSolicitacaoCompraPorIdQueryHandler : CommandHandler, IRequestHandler<ObterSolicitacaoCompraPorIdQuery, ObterSolicitacaoCompraPorIdResponse>
    {
        private readonly SolicitacaoAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly ProdutoAgg.IProdutoRepository _produtoRepository;
        private readonly SolicitacaoAgg.IItemRepository _itemRepository;

        public ObterSolicitacaoCompraPorIdQueryHandler(
            SolicitacaoAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, 
            SolicitacaoAgg.IItemRepository itemRepository, 
            ProdutoAgg.IProdutoRepository produtoRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this._solicitacaoCompraRepository = solicitacaoCompraRepository;
            this._produtoRepository = produtoRepository;
            this._itemRepository = itemRepository;
        }

        public Task<ObterSolicitacaoCompraPorIdResponse> Handle(ObterSolicitacaoCompraPorIdQuery query, CancellationToken cancellationToken)
        {
            try
            {

                var solic = _solicitacaoCompraRepository.Obter(query.Id);

                if(solic == null)
                {
                    return Task.FromResult(new ObterSolicitacaoCompraPorIdResponse
                    {
                        Message = $"Solicitação {solic.Id} não existe",
                        Sucesso = false
                    });
                }

                var itens = _itemRepository.ObterItensPorSolicitacao(query.Id);

                return Task.FromResult(new ObterSolicitacaoCompraPorIdResponse
                {                    
                    Message = $"Solicitação {solic.Id}",  
                    Solicitacao = new ObterSolicitacaoCompraPorIdResponse.SolicitacaoCompra 
                    {
                        Id = solic.Id, 
                        DateTime = solic.Data,
                        NomeFornecedor = solic.NomeFornecedor.Nome,
                        UsuarioSolicitante = solic.UsuarioSolicitante.Nome,
                        Itens = itens.Select(i => new ObterSolicitacaoCompraPorIdResponse.SolicitacaoCompra.Item 
                        {
                            Produto = i.Produto.Nome,
                            ProdutoId = i.Id,
                            Qtd = i.Qtde,
                            SubTotal = (decimal)i.Subtotal
                        }).ToList()
                    }, 
                    Sucesso = true                    
                });

            }            
            catch (Exception ex)
            {
                return Task.FromResult(new ObterSolicitacaoCompraPorIdResponse
                {
                    Exception = ex.InnerException ?? ex,
                    Message = "Erro ao obter a solicitação de compra"
                });
            }

        }
    }
}
