namespace Integration.Harness;

public class HttpContextAccessorBuilder
{
    public static IHttpContextAccessor CreateHttpContextAccessorWithClaims(string emailClaim)
    {
        var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Email, emailClaim)
        }));

        httpContextAccessor.HttpContext.Returns(httpContext);

        return httpContextAccessor;
    }
}
