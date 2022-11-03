using Gateway.API.Service;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Gateway.API.Middleware
{
    public class GroMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider serviceProvider;
        public GroMiddleware(RequestDelegate next, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _next = next;
            _configuration = configuration;
            this.serviceProvider = serviceProvider;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachAccountToContext(context, token, serviceProvider);

            await _next(context);
        }
        private void attachAccountToContext(HttpContext context, string token, IServiceProvider serviceProvider)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                //var email = jwtToken.Claims.First(x => x.Type == "id").Value;
                // attach account to context on successful jwt validation
                //using (var scope = serviceProvider.CreateScope())
                //{
                //    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                //    context.Items["User"] = userService.GetUserByEmail(email);
                //}
            }
            catch
            {
            }
        }
    }
}
