using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;



namespace SistemaCompra.Infra.Data.Produto
{
    public class SolicitacaoCompraConfiguration : IEntityTypeConfiguration<SolicitacaoAgg.SolicitacaoCompra>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAgg.SolicitacaoCompra> builder)
        {
            builder.ToTable("SolicitacaoCompra");

            builder.HasMany(s => s.Itens).WithOne(c => c.SolicitacaoCompra);

            builder
                 .Property(p => p.TotalGeral)
                 .HasConversion(
                     c => (decimal)c,
                     i => new Domain.Core.Model.Money(i)
                 );

            builder
               .Property(p => p.NomeFornecedor)
               .HasConversion(
                   c => c.Nome,
                   i => new SolicitacaoAgg.NomeFornecedor(i)
               );

            builder
                .Property(p => p.UsuarioSolicitante)
                .HasConversion(
                    c => c.Nome,
                    i => new SolicitacaoAgg.UsuarioSolicitante(i)
                );


            builder
               .Property(p => p.CondicaoPagamento)
               .HasConversion(
                   c => (int)c,
                   i => new SolicitacaoAgg.CondicaoPagamento(i)
               );

        }
    }
}
