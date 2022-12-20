using MediatR;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Threading;
using System.Threading.Tasks;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarSolicitacaoCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarSolicitacaoCompraCommand, RegistrarSolicitacaoCompraResponse>
    {
        private readonly SolicitacaoAgg.ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly ProdutoAgg.IProdutoRepository _produtoRepository;

        public RegistrarSolicitacaoCompraCommandHandler(
            SolicitacaoAgg.ISolicitacaoCompraRepository solicitacaoCompraRepository, ProdutoAgg.IProdutoRepository produtoRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            this._solicitacaoCompraRepository = solicitacaoCompraRepository;
            this._produtoRepository = produtoRepository;
        }

        public Task<RegistrarSolicitacaoCompraResponse> Handle(RegistrarSolicitacaoCompraCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var solicitacao = new SolicitacaoAgg.SolicitacaoCompra(request.UsuarioSolicitante, request.NomeFornecedor);

                foreach (var it in request.Itens)
                {
                    var produto = _produtoRepository.Obter(it.ProdutoId);                    

                    solicitacao.AdicionarItem(produto, it.Qtd);
                }

                _solicitacaoCompraRepository.Registrar(solicitacao);

                Commit();

                PublishEvents(solicitacao.Events);

                return Task.FromResult(new RegistrarSolicitacaoCompraResponse
                {
                    Exception = null,
                    Message = "Solicitação processada com sucesso!",
                    SolicitacaoId = solicitacao.Id, 
                    Sucesso = true
                });

            }
            catch(SistemaCompra.Domain.Core.BusinessRuleException ex)
            {
                return Task.FromResult(new RegistrarSolicitacaoCompraResponse
                {                  
                    Message = ex.Message
                });
            }
            catch(Exception ex)
            {
                return Task.FromResult(new RegistrarSolicitacaoCompraResponse
                {
                    Exception = ex.InnerException ?? ex,
                    Message = "Erro ao processar a solicitação de compra"                    
                });
            }
            
        }
    }
}
