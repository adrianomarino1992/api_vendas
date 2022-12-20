using System.Collections.Generic;
using System;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public interface IItemRepository : SistemaCompra.Domain.Core.IRepository<Item>
    {
        List<Item> ObterItensPorSolicitacao(Guid Id);
    }
}
