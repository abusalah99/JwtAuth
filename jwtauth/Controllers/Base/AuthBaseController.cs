namespace jwtauth.Controllers;

public class AuthBaseController : ControllerBase
{
    protected void SetCookie(string name, string value , DateTime expireTime)
        => Response.Cookies.Append(name , value
            , new CookieOptions()
            {
                HttpOnly = true,
                Expires = expireTime
            });
}
