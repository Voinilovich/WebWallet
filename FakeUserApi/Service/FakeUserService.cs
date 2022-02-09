﻿using FakeUserApi.Helpers;
using FakeUserApi.Interface;
using FakeUserApi.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FakeUserApi.Service
{
    public class FakeUserService:IFakeUserService
    {
        private readonly FakeUserContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FakeUserService> _logger;
        public FakeUserService( IConfiguration configuration,FakeUserContext context, ILogger<FakeUserService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public AuthenticateUser Authenticate(AuthenticateCommand command)
        {
            var account = _context.FakeUsers.SingleOrDefault(x => x.Login == command.Login);
            if (account == null || !BC.Verify(command.Pass, account.HashPass))
            {
                _logger.LogWarning(MyLogEvents.GenerateItems, "authenticate faild");
                return null;
            }
            var token = _configuration.GenerateJwtToken(account);
            _logger.LogInformation(MyLogEvents.GenerateItems, "authenticate FakeUser {Id}", account.Id);
            return new AuthenticateUser(account, token);
        }
    }
}
