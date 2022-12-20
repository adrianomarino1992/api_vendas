using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : Repository<SolicitacaoAgg.SolicitacaoCompra>, SolicitacaoAgg.ISolicitacaoCompraRepository
    {
        protected readonly SolicitacaoAgg.IItemRepository _itemRepository;

        public SolicitacaoCompraRepository(SistemaCompraContext context, SolicitacaoAgg.IItemRepository itemRepository) : base(context)
        {
            _itemRepository = itemRepository;
        }

        public void RegistrarCompra(SolicitacaoAgg.SolicitacaoCompra solicitacaoCompra)
        {
            this.Registrar(solicitacaoCompra);

            foreach (var i in solicitacaoCompra.Itens)
                _itemRepository.Registrar(i);
        }
    }
}
