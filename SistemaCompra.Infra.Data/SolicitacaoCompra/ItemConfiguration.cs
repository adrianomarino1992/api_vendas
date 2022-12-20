using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;


namespace SistemaCompra.Infra.Data.Produto
{
    public class ItemConfiguration : IEntityTypeConfiguration<SolicitacaoAgg.Item>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAgg.Item> builder)
        {
            builder.ToTable("Item");

            builder.HasOne(s => s.Produto);

            builder.HasOne(s => s.SolicitacaoCompra);

            builder
               .Ignore(p => p.Subtotal);

        }
    }
}
