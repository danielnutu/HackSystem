﻿using HackSystem.Web.Authentication.TokenHandlers;
using HackSystem.Web.Infrastructure.Extensions;

namespace HackSystem.Web.Infrastructure.Authentication;

public class AuthenticatedServiceBase
{
    protected readonly ILogger logger;
    protected readonly IHackSystemAuthenticationTokenHandler authenticationTokenHandler;
    protected readonly HttpClient httpClient;

    public AuthenticatedServiceBase(
        ILogger logger,
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        HttpClient httpClient)
    {
        this.logger = logger;
        this.authenticationTokenHandler = authenticationTokenHandler;
        this.httpClient = httpClient;
    }

    protected async Task AddAuthorizationHeaderAsync()
    {
        this.httpClient.AddAuthorizationHeader(await this.authenticationTokenHandler.GetTokenAsync());
    }
}