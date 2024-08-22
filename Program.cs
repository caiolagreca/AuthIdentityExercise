using IdentityExercise1.Database;
using IdentityExercise1.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Configura o Swagger para gerar a documenta��o interativa da API
builder.Services.AddSwaggerGen(options =>
{
    // Define como a autentica��o deve ser tratada (neste caso, usando um token no cabe�alho da requisi��o).
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    //Assegura que todos os endpoints protegidos na API exibam a necessidade de um token de autentica��o na documenta��o do Swagger.
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme).AddBearerToken(IdentityConstants.BearerScheme);

/*Utilizar AddIdentityCore() ao inves de AddDefaultIdentity():
D� mais controle sobre quais partes do Identity voc� quer configurar, �til para APIs ou quando n�o precisa da UI padr�o.
Evita carregar servi�os e depend�ncias desnecess�rias, o que pode ser melhor para APIs ou servi�os que n�o precisam de uma UI.
Pode exigir mais configura��o manual, j� que n�o inclui algumas das funcionalidades que o AddDefaultIdentity traz por padr�o.
*/
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDbContext>().AddApiEndpoints();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Em vez de voc� precisar rodar manualmente o comando dotnet ef database update para aplicar as migra��es pendentes ao banco de dados, o m�todo ApplyMigrations() faz isso automaticamente ao iniciar a aplica��o.
    app.ApplyMigrations();
}

app.UseHttpsRedirection();


// forma r�pida de expor endpoints de API para gerenciamento de usu�rios com Identity.
app.MapIdentityApi<User>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
