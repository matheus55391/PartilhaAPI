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
            return endpoint?.Metadata.GetMetadata<IAuthorizeData>() != null;
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
                var firebaseUid = decodedToken.Uid;

                // Crie um escopo para resolver IUserService
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                    // Verifica se o usuário já existe no banco de dados
                    var user = await userService.FindByFirebaseUidAsync(firebaseUid);
                    if (user == null)
                    {
                        // Se o usuário não existir, crie um novo usuário
                        user = new User
                        {
                            FirebaseUid = firebaseUid,
                            Name = decodedToken.Claims.ContainsKey("name") ? decodedToken.Claims["name"].ToString() : "Unnamed",
                            Email = decodedToken.Claims.ContainsKey("email") ? decodedToken.Claims["email"].ToString() : string.Empty
                        };

                        user = await userService.CreateUserAsync(user);
                    }

                    // Adiciona o usuário e FirebaseUid ao HttpContext.Items
                    context.Items["User"] = user;
                    context.Items["FirebaseUid"] = firebaseUid;
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
