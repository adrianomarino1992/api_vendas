using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using prod = SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Linq;


namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class ItemRepository : Repository<SolicitacaoAgg.Item>, SolicitacaoAgg.IItemRepository
    {

        public ItemRepository(SistemaCompraContext context) : base(context)
        {

        }

        public List<Item> ObterItensPorSolicitacao(Guid id)
        {
            var itens = _context.Set<Item>().Where(s => s.SolicitacaoCompra.Id == id).ToList();

            itens.ForEach(i => i.Produto = _context.Set<prod.Produto>().Where(p => p.Id == i.ProdutoId).FirstOrDefault());

            return itens;
        }
    }
}
