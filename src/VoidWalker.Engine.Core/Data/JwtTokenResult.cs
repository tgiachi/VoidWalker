namespace VoidWalker.Engine.Core.Data;

public record JwtTokenResult(bool Success, string? Token, DateTime? ExpiresAt);
