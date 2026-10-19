// === COMMANDS (WRITE) ===

/**
 * Command for POST /api/auth/login
 * Corresponds to: LoginCommand.cs
 */
export interface LoginCommand {
  email: string;
  password: string;
  fingerprint?: string | null;
}

/**
 * Response for POST /api/auth/login
 * Corresponds to: LoginCommandDto.cs (isti oblik kao odgovor na refresh)
 */
export interface LoginCommandDto {
  accessToken: string;
  refreshToken: string;
  /**
   * ISO string (UTC) when access token expires
   * Example: "2025-12-02T23:59:59Z"
   */
  accessTokenExpiresAtUtc: string;
  /**
   * ISO string (UTC) when refresh token expires
   */
  refreshTokenExpiresAtUtc: string;
}

/**
 * Command for POST /api/auth/refresh
 * Corresponds to: RefreshTokenCommand.cs
 */
export interface RefreshTokenCommand {
  refreshToken: string;
  fingerprint?: string | null;
}

/**
 * Response for POST /api/auth/refresh
 * Corresponds to: RefreshTokenCommandDto.cs
 */
export interface RefreshTokenCommandDto {
  accessToken: string;
  refreshToken: string;
  /**
   * ISO string (UTC) when access token expires
   */
  accessTokenExpiresAtUtc: string;
  /**
   * ISO string (UTC) when refresh token expires
   */
  refreshTokenExpiresAtUtc: string;
}

/**
 * Command for POST /api/auth/logout
 * Corresponds to: LogoutCommand.cs
 */
export interface LogoutCommand {
  refreshToken: string;
}
