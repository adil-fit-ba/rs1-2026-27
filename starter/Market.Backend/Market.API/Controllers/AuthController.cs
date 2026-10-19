using Market.Application.Modules.Auth.Commands.Login;
using Market.Application.Modules.Auth.Commands.Logout;
using Market.Application.Modules.Auth.Commands.Refresh;
using Market.Shared.Dtos;

namespace Market.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Login with email and password. Wrong email, wrong password and a disabled account
    /// all return the same 401 response.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginCommandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginCommandDto>> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    /// <summary>
    /// Exchanges a refresh token for a new access/refresh pair (the old refresh token is revoked).
    /// Any invalid, expired, revoked or reused token returns 401.
    /// </summary>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RefreshTokenCommandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RefreshTokenCommandDto>> Refresh([FromBody] RefreshTokenCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    /// <summary>
    /// Revokes the refresh token (idempotent).
    /// </summary>
    [AllowAnonymous]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task Logout([FromBody] LogoutCommand command, CancellationToken ct)
    {
        await mediator.Send(command, ct);
    }
}
