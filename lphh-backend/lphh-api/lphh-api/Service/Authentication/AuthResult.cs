﻿namespace lphh_api.Service.Authentication;

public record AuthResult(
    bool Success,
    string Email,
    string UserName,
    string IdentityID,
    string Token)
{
    //Error code - error message
    public readonly Dictionary<string, string> ErrorMessages = new();
}