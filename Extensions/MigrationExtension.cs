using IdentityExercise1.Database;
using Microsoft.EntityFrameworkCore;

namespace IdentityExercise1.Extensions
{
    //Métodos de extensão precisam ser definidos em classes estáticas.
    public static class MigrationExtension
    {
        //IApplicationBuilder é o tipo da variável "app" no Program.cs
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            //Cria um escopo de serviço. Um IServiceScope é usado para resolver dependências do contêiner de serviços, garantindo que os serviços sejam corretamente descartados após o uso. O "using" garante que o scope seja descartado automaticamente quando o código sair do bloco.
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            //O GetRequiredService<ApplicationDbContext>() obtém a instância do ApplicationDbContext configurado, que é necessário para interagir com o banco de dados.
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //Este método aplica todas as migrações pendentes ao banco de dados. Se o banco de dados já estiver atualizado, este comando não fará nada.
            context.Database.Migrate();
        }
    }
}

//O método ApplyMigrations é implementado como uma extensão para melhorar a organização, reutilização e manutenibilidade do código. Ele encapsula a lógica de migração em um único método que pode ser chamado facilmente em qualquer ponto onde o IApplicationBuilder estiver disponível. Além disso, a criação de um escopo de serviço e a aplicação de migrações dentro desse escopo garante que as migrações sejam aplicadas de forma segura e eficaz, utilizando o context do banco de dados configurado pela aplicação.
