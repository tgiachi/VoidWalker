namespace VoidWalker.Engine.Core.Data.Auth;

public record JwtTokenResult(bool Success, string? Token, DateTime? ExpiresAt);
