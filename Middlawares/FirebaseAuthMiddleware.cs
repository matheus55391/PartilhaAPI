using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using System.Net;
using System.Linq;
using System.Security.Claims;
using PartilhaAPI.Models;
using PartilhaAPI.Services;

namespace PartilhaAPI.Middleware
{
    public class FirebaseAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public FirebaseAuthMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica se a rota atual requer autorização
            if (IsAuthorizedRoute(context))
            {
                // Verifica e valida o token do Firebase
                var isValidToken = await ValidateFirebaseTokenAsync(context);

                if (!isValidToken)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Invalid or missing token.");
                    return;
                }
            }

            // Chama o próximo middleware
            await _next(context);
        }

        private bool IsAuthorizedRoute(HttpContext context)
        {
            // Verifica se a rota atual requer autorização
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                return endpoint.Metadata.GetMetadata<IAuthorizeData>() != null;
            }
            return false;
        }

        private async Task<bool> ValidateFirebaseTokenAsync(HttpContext context)
        {
            // Obtém o token do cabeçalho Authorization
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return false; // Token ausente
            }

            try
            {
                // Valida o token Firebase
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                // Crie um escopo para resolver IUserService
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                    // Verifica se o usuário já existe no banco de dados
                    var user = await userService.FindByFirebaseUidAsync(decodedToken.Uid);
                    if (user == null)
                    {
                        // Se o usuário não existir, crie um novo usuário
                        user = new User
                        {
                            FirebaseUid = decodedToken.Uid,
                            Name = decodedToken.Claims["name"].ToString() ?? "Unnamed", // Se o nome estiver disponível
                            Email = decodedToken.Claims["email"].ToString() // Se o email estiver disponível
                        };

                        user = await userService.CreateUserAsync(user);
                    }

                    // Configure o usuário no contexto
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // UID do usuário
                        new Claim(ClaimTypes.Email, user.Email) // Email do usuário
                    }, "Firebase"));
                }

                return true; // Token válido
            }
            catch (FirebaseAuthException)
            {
                return false; // Token inválido
            }
        }
    }
}
