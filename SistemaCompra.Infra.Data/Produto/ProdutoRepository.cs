using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;


namespace SistemaCompra.Infra.Data.Produto
{
    public class ProdutoRepository : Repository<ProdutoAgg.Produto>, ProdutoAgg.IProdutoRepository
    {      

        public ProdutoRepository(SistemaCompraContext context) : base(context)
        {

        }
        
    }
}
