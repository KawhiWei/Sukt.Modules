﻿using Microsoft.Extensions.Logging;
using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Identity.Dto.Identity.Users;
using Sukt.Module.Core.Extensions;

namespace Sukt.Identity.Application.Users
{
    public class IdentityUserAppService : IIdentityUserAppService
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly ILogger _logger;

        public IdentityUserAppService(IdentityUserManager userManager, ILogger<IdentityUserAppService> logger)
        {
            _identityUserManager = userManager;
            _logger = logger;
        }
       

        public virtual async Task CreateUserAsync(IdentityUserCreateInputDto input)
        {
            var identityUser = new IdentityUser(input.UserName, input.Email, input.NickName, phoneNumber:input.PhoneNumber);
            _logger.LogError($"创建用户日志打印：{identityUser.ToJson()}");

            identityUser.SetPasswordHash(input.PasswordHash);

            if(!input.TenantId.IsNullOrEmpty())
            {
                identityUser.SetTenantId(input.TenantId);
            }
            await _identityUserManager.CreateAsync(identityUser, identityUser.PasswordHash);
        }
        public virtual async Task UpdateUserForIdAsync(string id, IdentityUserUpdateInputDto input)
        {
            var identityUser = await _identityUserManager.FindByIdAsync(id);

            identityUser.SetUserName(input.UserName);
            identityUser.SetNormalizedUserName(input.UserName);
            identityUser.SetNikeName(input.NickName);
            identityUser.SetEmail(input.Email);
            identityUser.SetNormalizedEmail(input.Email);
            if (!input.TenantId.IsNullOrEmpty())
            {
                identityUser.SetTenantId(input.TenantId);
            }
            await _identityUserManager.UpdateAsync(identityUser);
        }

        public virtual async Task DeleteUserForIdAsync(string id)
        {
            var identityUser = await _identityUserManager.FindByIdAsync(id);
            await _identityUserManager.DeleteAsync(identityUser);
        }

        public virtual async Task UpdateRoleForUserIdAsync(string id,IEnumerable<string> roles)
        {
            var identityUser = await _identityUserManager.FindByIdAsync(id);
            await _identityUserManager.RemoveFromRolesAsync(identityUser, roles);
            await _identityUserManager.AddToRolesAsync(identityUser, roles);
            await _identityUserManager.UpdateAsync(identityUser);
        }

    }
}
