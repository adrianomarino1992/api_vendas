using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;

namespace SistemaCompra.Infra.Data.Produto
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<ProdutoAgg.Produto>
    {
        public void Configure(EntityTypeBuilder<ProdutoAgg.Produto> builder)
        {
            builder.ToTable("Produto");

            builder
                .Property(p => p.Categoria)
                .HasConversion(
                    c => (int)c,
                    i => (ProdutoAgg.Categoria)i
                );

            builder
               .Property(p =>p.Preco)
               .HasConversion(
                p => p.Value, 
                d => new SistemaCompra.Domain.Core.Model.Money(d));

            builder.HasData(
                new ProdutoAgg.Produto("Tabua 200cm", "Tabua para construção 2M", "Madeira", 100),
                new ProdutoAgg.Produto("Tabua 300cm", "Tabua para construção 2M", "Madeira", 150),
                new ProdutoAgg.Produto("Fixador 3M", "Fixador marca 3M", "Fixadores", 50),
                new ProdutoAgg.Produto("Juncao", "Juncao de aço", "Juncao", 25.65M)
                );


        }
    }
}
