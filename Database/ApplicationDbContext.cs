using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityExercise1.Database
{

    // O IdentityDbContext é uma classe especializada do Entity Framework Core que inclui todas as configurações e tabelas necessárias para o ASP.NET Core Identity.
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        //Construtor da classe ApplicationDbContext. Ele recebe um objeto DbContextOptions<ApplicationDbContext> que contém as opções de configuração para o context, como a string de conexão do banco de dados. Esse construtor chama o construtor da classe base (IdentityDbContext) passando as options, permitindo que o DbContext seja configurado conforme necessário.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Este método é sobrescrito da classe base IdentityDbContext. Permite adicionar configurações específicas ao modelo de dados, como definir o tamanho máximo de uma propriedade ou alterar o esquema padrão das tabelas.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Essa chamada garante que o comportamento padrão de configuração do Identity (realizado pela classe base IdentityDbContext) seja aplicado. Isso inclui a criação das tabelas padrão do ASP.NET Core Identity, como AspNetUsers, AspNetRoles, etc.
            base.OnModelCreating(builder);

            //Configura a entidade User, especificando que a propriedade Initials deve ter um tamanho máximo de 5 caracteres. Isso significa que, ao mapear o modelo para o banco de dados, a coluna Initials na tabela correspondente terá um limite de 5 caracteres.
            builder.Entity<User>().Property(u => u.Initials).HasMaxLength(5);

            //Define o esquema padrão (schema) para todas as tabelas criadas neste DbContext. Em vez de criar as tabelas no esquema padrão "dbo", elas serão criadas sob o esquema "identity". Isso pode ser útil para organizar melhor as tabelas de identity dentro do banco de dados, especialmente em sistemas onde múltiplos esquemas são usados.
            builder.HasDefaultSchema("identity");
        }

    }
}

/*
Ele ajuda a salvar e buscar dados, como informações de usuários e produtos. 
Também configura como os dados são organizados e armazenados, 
garantindo que tudo funcione corretamente quando a aplicação precisa conversar com o banco de dados.
*/