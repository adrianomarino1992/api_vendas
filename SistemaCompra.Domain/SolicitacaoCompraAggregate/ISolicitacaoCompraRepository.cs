namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public interface ISolicitacaoCompraRepository : SistemaCompra.Domain.Core.IRepository<SolicitacaoCompra>
    {
        void RegistrarCompra(SolicitacaoCompra solicitacaoCompra);
    }
}
