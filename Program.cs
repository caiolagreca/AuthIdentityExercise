using IdentityExercise1.Database;
using IdentityExercise1.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Configura o Swagger para gerar a documentação interativa da API
builder.Services.AddSwaggerGen(options =>
{
    // Define como a autenticação deve ser tratada (neste caso, usando um token no cabeçalho da requisição).
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    //Assegura que todos os endpoints protegidos na API exibam a necessidade de um token de autenticação na documentação do Swagger.
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme).AddBearerToken(IdentityConstants.BearerScheme);

/*Utilizar AddIdentityCore() ao inves de AddDefaultIdentity():
Dá mais controle sobre quais partes do Identity você quer configurar, útil para APIs ou quando não precisa da UI padrão.
Evita carregar serviços e dependências desnecessárias, o que pode ser melhor para APIs ou serviços que não precisam de uma UI.
Pode exigir mais configuração manual, já que não inclui algumas das funcionalidades que o AddDefaultIdentity traz por padrão.
*/
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDbContext>().AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Em vez de você precisar rodar manualmente o comando dotnet ef database update para aplicar as migrações pendentes ao banco de dados, o método ApplyMigrations() faz isso automaticamente ao iniciar a aplicação.
    app.ApplyMigrations();
}

app.UseHttpsRedirection();


// forma rápida de expor endpoints de API para gerenciamento de usuários com Identity.
app.MapIdentityApi<User>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
