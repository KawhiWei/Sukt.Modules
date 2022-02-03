
using Microsoft.EntityFrameworkCore;
using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Domain.Repositories.Roles;
using Sukt.Identity.Domain.Repositories.Users;


namespace Sukt.Identity.Domain.Aggregates.Users
{
    public class IdentityUserStore :
          IQueryableUserStore<IdentityUser>,
          IUserLoginStore<IdentityUser>,
          IUserClaimStore<IdentityUser>,
          IUserPasswordStore<IdentityUser>,
          IUserSecurityStampStore<IdentityUser>,
          IUserEmailStore<IdentityUser>,
          IUserLockoutStore<IdentityUser>,
          IUserPhoneNumberStore<IdentityUser>,
          IUserTwoFactorStore<IdentityUser>,
          IUserAuthenticationTokenStore<IdentityUser>,
          IUserAuthenticatorKeyStore<IdentityUser>,
          IUserTwoFactorRecoveryCodeStore<IdentityUser>,
          IUserRoleStore<IdentityUser>
    {
        private readonly IIdentityUserRepository _userRepository;
        private readonly IIdentityRoleRepository _roleRepository;
        private const string InternalLoginProvider = "[AspNeIdentityUserStore]";
        private const string AuthenticatorKeyTokenName = "AuthenticatorKey";
        private const string RecoveryCodeTokenName = "RecoveryCodes";
        private bool _disposed;
        public IdentityUserStore(IIdentityUserRepository userRepository, IIdentityRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }


        #region 实现IdentityUser的查询属性 <IdentityUser>

        /// <summary>
        /// Returns an <see cref="T:System.Linq.IQueryable`1" /> collection of users.
        /// </summary>
        /// <value>An <see cref="T:System.Linq.IQueryable`1" /> collection of users.</value>
        public IQueryable<IdentityUser> Users => _userRepository.TrackEntities;

        #endregion Implementation of IQueryableUserStore<IdentityUser>

        #region 实现IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _disposed = true;
        }

        #endregion Implementation of IDisposable

        #region 实现IUserStore<IdentityUser>接口

        /// <summary>
        /// Gets the user name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the name for the specified <paramref name="user" />.</returns>
        public Task<string> GeIdentityUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.UserName);
        }

        /// <summary>
        /// Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="userName">The user name to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SeIdentityUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetUserName(userName);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the normalized user name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose normalized name should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the normalized user name for the specified <paramref name="user" />.</returns>
        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.NormalizedUserName);
        }

        /// <summary>
        /// Sets the given normalized name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="normalizedName">The normalized name to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetNormalizedUserName(normalizedName);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates the specified <paramref name="user" /> in the user store.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the creation operation.</returns>
        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            await _userRepository.InsertAsync(user);

            return IdentityResult.Success;
        }

        /// <summary>
        /// Updates the specified <paramref name="user" /> in the user store.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the update operation.</returns>
        public async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(user.Email))
            {
                user.SetEmailConfirmed(false);
            }
            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                user.SetPhoneNumberConfirmed(false);
            }
            await _userRepository.UpdateAsync(user);
            return IdentityResult.Success;
        }

        /// <summary>
        /// Deletes the specified <paramref name="user" /> from the user store.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the update operation.</returns>
        public async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            if (user.IsSystem)
            {
                throw new SuktAppBusinessException($"用户“{user.UserName}”是系统用户，不能删除");
            }
            await _userRepository.DeleteAsync(user);
            return IdentityResult.Success;
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified <paramref name="userId" />.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="userId" /> if it exists.
        /// </returns>
        public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var id = ConvertIdFromString(userId);
            if (id.IsNullOrEmpty())
            {
                throw new SuktAppBusinessException($"Id为空");
            }
            var identityUser = await _userRepository.TrackEntities.Include(x => x.Roles)
                .Include(x => x.Logins)
                .Include(x => x.Claims)
                .Include(x => x.Tokens).FirstOrDefaultAsync(x => x.Id == id);
#pragma warning disable CS8603 // 可能返回 null 引用。
            return identityUser;
#pragma warning restore CS8603 // 可能返回 null 引用。
                              //return Task.FromResult(IdentityUser);
        }

        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name.
        /// </summary>
        /// <param name="normalizedUserName">The normalized user name to search for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user matching the specified <paramref name="normalizedUserName" /> if it exists.
        /// </returns>
        public async Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var identityUser = await _userRepository.TrackEntities.Include(x => x.Roles)
                .Include(x => x.Logins)
                .Include(x => x.Claims)
                .Include(x => x.Tokens).FirstOrDefaultAsync(m => m.NormalizedUserName == normalizedUserName, cancellationToken);
#pragma warning disable CS8603 // 可能返回 null 引用。
            return identityUser;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }

        public Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            user.SetUserName(userName);
            return Task.CompletedTask;
        }

        public Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion Implementation of IUserStore<IdentityUser>

        #region 实现IUserLoginStore<IdentityUser>

        /// <summary>
        /// Adds an external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to add the login to.</param>
        /// <param name="login">The external <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> to add to the specified <paramref name="user" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public async Task AddLoginAsync(IdentityUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.AddLogin(login);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Attempts to remove the provided login information from the specified <paramref name="user" />.
        /// and returns a flag indicating whether the removal succeed or not.
        /// </summary>
        /// <param name="user">The user to remove the login information from.</param>
        /// <param name="loginProvider">The login provide whose information should be removed.</param>
        /// <param name="providerKey">The key given by the external login provider for the specified user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public async Task RemoveLoginAsync(IdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.RemoveLogin(loginProvider, providerKey);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Retrieves the associated logins for the specified <param ref="user" />.
        /// </summary>
        /// <param name="user">The user whose associated logins to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing a list of <see cref="T:Microsoft.AspNetCore.Identity.UserLoginInfo" /> for the specified <paramref name="user" />, if any.
        /// </returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            IList<UserLoginInfo> loginInfos = user.Logins.Select(m =>
                new UserLoginInfo(m.LoginProvider, m.ProviderKey, m.ProviderDisplayName)).ToList();
            return Task.FromResult(loginInfos);
        }

        /// <summary>
        /// Retrieves the user associated with the specified login provider and login provider key.
        /// </summary>
        /// <param name="loginProvider">The login provider who provided the <paramref name="providerKey" />.</param>
        /// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> for the asynchronous operation, containing the user, if any which matched the specified login provider and key.
        /// </returns>
        public async Task<IdentityUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var identityUser = await _userRepository.FindByLoginAsync(loginProvider, providerKey, cancellationToken: cancellationToken);
            //if (identityUser is null)
            //{
            //    throw new SuktAppBusinessException("用户不存在");
            //}
#pragma warning disable CS8603 // 可能返回 null 引用。
            return identityUser;
#pragma warning restore CS8603 // 可能返回 null 引用。
                              //string userId = _userLoginRepository.NoTrackEntities.Where(m => m.LoginProvider == loginProvider && m.ProviderKey == providerKey)
                              //    .Select(m => m.UserId).FirstOrDefault();
                              //if (Equals(userId, default(string)))
                              //{
                              //    return Task.FromResult(default(IdentityUser));
                              //}
                              //IdentityUser user = _userRepository.GetById(userId);
                              //return Task.FromResult(user);
        }

        #endregion Implementation of IUserLoginStore<IdentityUser>

        #region 实现IUserClaimStore<IdentityUser>

        /// <summary>
        /// Gets a list of <see cref="T:System.Security.Claims.Claim" />s to be belonging to the specified <paramref name="user" /> as an asynchronous operation.
        /// </summary>
        /// <param name="user">The role whose claims to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a list of <see cref="T:System.Security.Claims.Claim" />s.
        /// </returns>
        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            IList<Claim> claims = user.Claims.Select(x => x.ToClaim()).ToList();
            return Task.FromResult(claims);
        }

        /// <summary>Add claims to a user as an asynchronous operation.</summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="claims">The collection of <see cref="T:System.Security.Claims.Claim" />s to add.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.AddClaims(claims);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Replaces the given <paramref name="claim" /> on the specified <paramref name="user" /> with the <paramref name="newClaim" />
        /// </summary>
        /// <param name="user">The user to replace the claim on.</param>
        /// <param name="claim">The claim to replace.</param>
        /// <param name="newClaim">The new claim to replace the existing <paramref name="claim" /> with.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.ReplaceClaim(claim, newClaim);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Removes the specified <paramref name="claims" /> from the given <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user to remove the specified <paramref name="claims" /> from.</param>
        /// <param name="claims">A collection of <see cref="T:System.Security.Claims.Claim" />s to remove.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.RemoveClaims(claims);
            await _userRepository.UpdateAsync(user);
        }

        ///// <summary>
        ///// Returns a list of users who contain the specified <see cref="T:System.Security.Claims.Claim" />.
        ///// </summary>
        ///// <param name="claim">The claim to look for.</param>
        ///// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        ///// <returns>
        ///// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a list of <typeparamref name="IdentityUser" /> who
        ///// contain the specified claim.
        ///// </returns>
        //public Task<IList<IdentityUser>> GeIdentityUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    ThrowIfDisposed();

        //    string[] userIds = _userClaimRepository.NoTrackEntities.Where(m => m.ClaimType == claim.Type && m.ClaimValue == claim.Value)
        //        .Select(m => m.UserId).ToArray();
        //    IList<IdentityUser> users = _userRepository.NoTrackEntities.Where(m => userIds.Contains(m.Id)).ToList();
        //    return Task.FromResult(users);
        //}

        #endregion Implementation of IUserClaimStore<IdentityUser>

        #region 实现IUserPasswordStore<IdentityUser>

        /// <summary>
        /// Sets the password hash for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose password hash to set.</param>
        /// <param name="passwordHash">The password hash to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetPasswordHash(passwordHash);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the password hash for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose password hash to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning the password hash for the specified <paramref name="user" />.</returns>
        public Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="user" /> has a password.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a password
        /// otherwise false.
        /// </returns>
        public Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion Implementation of IUserPasswordStore<IdentityUser>

        #region 实现IUserSecurityStampStore<IdentityUser>

        /// <summary>
        /// Sets the provided security <paramref name="stamp" /> for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be set.</param>
        /// <param name="stamp">The security stamp to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetSecurityStampAsync(IdentityUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetSecurityStamp(stamp);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the security stamp for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the security stamp for the specified <paramref name="user" />.</returns>
        public Task<string> GetSecurityStampAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.SecurityStamp);
        }

        #endregion Implementation of IUserSecurityStampStore<IdentityUser>

        #region 实现IUserEmailStore<IdentityUser>

        /// <summary>
        /// Sets the <paramref name="email" /> address for a <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email should be set.</param>
        /// <param name="email">The email to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetEmailAsync(IdentityUser user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetEmail(email);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the email address for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object containing the results of the asynchronous operation, the email address for the specified <paramref name="user" />.</returns>
        public Task<string> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Gets a flag indicating whether the email address for the specified <paramref name="user" /> has been verified, true if the email address is verified otherwise
        /// false.
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous operation, a flag indicating whether the email address for the specified <paramref name="user" />
        /// has been confirmed or not.
        /// </returns>
        public Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// Sets the flag indicating whether the specified <paramref name="user" />'s email address has been confirmed or not.
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be set.</param>
        /// <param name="confirmed">A flag indicating if the email address has been confirmed, true if the address is confirmed otherwise false.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetEmailConfirmed(true);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the user, if any, associated with the specified, normalized email address.
        /// </summary>
        /// <param name="normalizedEmail">The normalized email address to return the user for.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the user if any associated with the specified normalized email address.
        /// </returns>
        public async Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var identityUser = await _userRepository.FindByNormalizedEmailAsync(normalizedEmail);
            //if (identityUser is null)
            //{
            //    throw new SuktAppBusinessException("用户不存在");
            //}
#pragma warning disable CS8603 // 可能返回 null 引用。
            return identityUser;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }

        /// <summary>
        /// Returns the normalized email for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email address to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the normalized email address if any associated with the specified user.
        /// </returns>
        public Task<string> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.NormalizedUserName);
        }

        /// <summary>
        /// Sets the normalized email for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose email address to set.</param>
        /// <param name="normalizedEmail">The normalized email to set for the specified <paramref name="user" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetNormalizedEmailAsync(IdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetNormalizedEmail(normalizedEmail);
            return Task.CompletedTask;
        }

        #endregion Implementation of IUserEmailStore<IdentityUser>

        #region 实现IUserLockoutStore<IdentityUser>

        /// <summary>
        /// Gets the last <see cref="T:System.DateTimeOffset" /> a user's last lockout expired, if any.
        /// Any time in the past should be indicates a user is not locked out.
        /// </summary>
        /// <param name="user">The user whose lockout date should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the result of the asynchronous query, a <see cref="T:System.DateTimeOffset" /> containing the last time
        /// a user's lockout expired, if any.
        /// </returns>
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.LockoutEnd);
        }

        /// <summary>
        /// Locks out a user until the specified end date has passed. Setting a end date in the past immediately unlocks a user.
        /// </summary>
        /// <param name="user">The user whose lockout date should be set.</param>
        /// <param name="lockoutEnd">The <see cref="T:System.DateTimeOffset" /> after which the <paramref name="user" />'s lockout should end.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetLockoutEnd(lockoutEnd);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Records that a failed access has occurred, incrementing the failed access count.
        /// </summary>
        /// <param name="user">The user whose cancellation count should be incremented.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the incremented failed access count.</returns>
        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.SetAccessFailedCount();
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>Resets a user's failed access count.</summary>
        /// <param name="user">The user whose failed access count should be reset.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        /// <remarks>This is typically called after the account is successfully accessed.</remarks>
        public Task ResetAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.ResetAccessFailedCount(0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves the current failed access count for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose failed access count should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the failed access count.</returns>
        public Task<int> GetAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>
        /// Retrieves a flag indicating whether user lockout can enabled for the specified user.
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be returned.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, true if a user can be locked out, otherwise false.
        /// </returns>
        public Task<bool> GetLockoutEnabledAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>
        /// Set the flag indicating if the specified <paramref name="user" /> can be locked out.
        /// </summary>
        /// <param name="user">The user whose ability to be locked out should be set.</param>
        /// <param name="enabled">A flag indicating if lock out can be enabled for the specified <paramref name="user" />.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetLockoutEnabled(enabled);
            return Task.CompletedTask;
        }

        #endregion Implementation of IUserLockoutStore<IdentityUser>

        #region 实现IUserPhoneNumberStore<IdentityUser>

        /// <summary>
        /// Sets the telephone number for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose telephone number should be set.</param>
        /// <param name="phoneNumber">The telephone number to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetPhoneNumber(phoneNumber);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the telephone number, if any, for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose telephone number should be retrieved.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the user's telephone number, if any.</returns>
        public Task<string> GetPhoneNumberAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

#pragma warning disable CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
            return Task.FromResult(user.PhoneNumber);
#pragma warning restore CS8619 // 值中的引用类型的为 Null 性与目标类型不匹配。
        }

        /// <summary>
        /// Gets a flag indicating whether the specified <paramref name="user" />'s telephone number has been confirmed.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether their telephone number is confirmed.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, returning true if the specified <paramref name="user" /> has a confirmed
        /// telephone number otherwise false.
        /// </returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>
        /// Sets a flag indicating if the specified <paramref name="user" />'s phone number has been confirmed.
        /// </summary>
        /// <param name="user">The user whose telephone number confirmation status should be set.</param>
        /// <param name="confirmed">A flag indicating whether the user's telephone number has been confirmed.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetPhoneNumberConfirmed(confirmed);
            return Task.CompletedTask;
        }

        #endregion Implementation of IUserPhoneNumberStore<IdentityUser>

        #region 实现IUserTwoFactorStore<IdentityUser>

        /// <summary>
        /// Sets a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set.</param>
        /// <param name="enabled">A flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            user.SetTwoFactorEnabled(enabled);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user" /> has two factor authentication enabled or not,
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose two factor authentication enabled status should be set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing a flag indicating whether the specified
        /// <paramref name="user" /> has two factor authentication enabled or not.
        /// </returns>
        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.TwoFactorEnabled);
        }

        #endregion Implementation of IUserTwoFactorStore<IdentityUser>

        #region 实现IUserAuthenticationTokenStore<IdentityUser>

        /// <summary>Sets the token value for a particular user.</summary>
        /// <param name="user">The user.</param>
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// <param name="value">The value of the token.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public async Task SetTokenAsync(IdentityUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();


            user.SetToken(loginProvider, name, value);
            await _userRepository.UpdateAsync(user);

        }

        /// <summary>Deletes a token for a user.</summary>
        /// <param name="user">The user.</param>
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public async Task RemoveTokenAsync(IdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.RemoveToken(loginProvider, name);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>Returns the token value.</summary>
        /// <param name="user">The user.</param>
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task<string> GetTokenAsync(IdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var token = user.FindToken(loginProvider, name);
            if (token is not null)
            {
                return Task.FromResult(token.Value);
            }
            var value = "";
            return Task.FromResult(value);
        }

        #endregion Implementation of IUserAuthenticationTokenStore<IdentityUser>

        #region 实现IUserAuthenticatorKeyStore<IdentityUser>



        /// <summary>
        /// Sets the authenticator key for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose authenticator key should be set.</param>
        /// <param name="key">The authenticator key to set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public Task SetAuthenticatorKeyAsync(IdentityUser user, string key, CancellationToken cancellationToken)
        {
            return SetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, key, cancellationToken);
        }

        /// <summary>
        /// Get the authenticator key for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose security stamp should be set.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the security stamp for the specified <paramref name="user" />.</returns>
        public Task<string> GetAuthenticatorKeyAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return GetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, cancellationToken);
        }

        #endregion Implementation of IUserAuthenticatorKeyStore<IdentityUser>

        #region 实现IUserTwoFactorRecoveryCodeStore<IdentityUser>

        /// <summary>
        /// Updates the recovery codes for the user while invalidating any previous recovery codes.
        /// </summary>
        /// <param name="user">The user to store new recovery codes for.</param>
        /// <param name="recoveryCodes">The new recovery codes for the user.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The new recovery codes for the user.</returns>
        public Task ReplaceCodesAsync(IdentityUser user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
        {
            string mergedCodes = string.Join(";", recoveryCodes);
            return SetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, mergedCodes, cancellationToken);
        }

        /// <summary>
        /// Returns whether a recovery code is valid for a user. Note: recovery codes are only valid
        /// once, and will be invalid after use.
        /// </summary>
        /// <param name="user">The user who owns the recovery code.</param>
        /// <param name="code">The recovery code to use.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>True if the recovery code was found for the user.</returns>
        public async Task<bool> RedeemCodeAsync(IdentityUser user, string code, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            string mergedCodes = await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken) ?? String.Empty;
            string[] splitCodes = mergedCodes.Split(';');
            if (splitCodes.Contains(code))
            {
                List<string> updatedCodes = new List<string>(splitCodes.Where(s => s != code));
                await ReplaceCodesAsync(user, updatedCodes, cancellationToken);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns how many recovery code are still valid for a user.
        /// </summary>
        /// <param name="user">The user who owns the recovery code.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The number of valid recovery codes for the user..</returns>
        public async Task<int> CountCodesAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            string mergedCodes = await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken);
            if (mergedCodes.Length > 0)
            {
                return mergedCodes.Split(';').Length;
            }
            return 0;
        }

        #endregion Implementation of IUserTwoFactorRecoveryCodeStore<IdentityUser>

        #region 实现IUserRoleStore<IdentityUser>

        /// <summary>
        /// Add a the specified <paramref name="user" /> to the named role.
        /// </summary>
        /// <param name="user">The user to add to the named role.</param>
        /// <param name="normalizedRoleName">The name of the role to add the user to.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public async Task AddToRoleAsync(IdentityUser user, string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.AddRole(normalizedRoleName);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Add a the specified <paramref name="user" /> from the named role.
        /// </summary>
        /// <param name="user">The user to remove the named role from.</param>
        /// <param name="normalizedRoleName">The name of the role to remove.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
        public async Task RemoveFromRoleAsync(IdentityUser user, string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            user.RemoveRole(normalizedRoleName);
            await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// Gets a list of role names the specified <paramref name="user" /> belongs to.
        /// </summary>
        /// <param name="user">The user whose role names to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing a list of role names.</returns>
        public Task<IList<string>> GetRolesAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            IList<string> roles = new List<string>();
            foreach (var item in user.Roles.Select(x => x.RoleId).ToList())
            {
                roles.Add(item);
            }
            return Task.FromResult(roles);
        }

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="user" /> is a member of the give named role.
        /// 所有和roleName || roleName
        /// </summary>
        /// <param name="user">The user whose role membership should be checked.</param>
        /// <param name="roleName">The name of the role to be checked.</param>
        /// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="user" /> is
        /// a member of the named role.
        /// </returns>
        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            //return await _roleRepository.TrackEntities.Where(x => x.Id == roleName).AnyAsync();

            //TRoleKey roleId = _roleRepository.NoTrackEntities.Where(m => m.NormalizedName == roleName).Select(m => m.Id).FirstOrDefault();
            //if (Equals(roleId, default(TRoleKey)))
            //{
            //    throw new InvalidOperationException($"名称为“{roleName}”的角色信息不存在");
            //}
            //bool exist = _userRoleRepository.NoTrackEntities.Where(m => m.UserId.Equals(user.Id) && m.RoleId.Equals(roleId)).Any();
            //return Task.FromResult(exist);
            //user.IsInRole(roleName)//考虑到性能问题，暂时先不做校验
            return Task.FromResult(true);
        }

        public async Task<IList<IdentityUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _userRepository.GetListByNormalizedRoleNameAsync(roleName);
        }

        #endregion Implementation of IUserRoleStore<IdentityUser>

        #region Other

        /// <summary>
        /// Converts the provided <paramref name="id"/> to a strongly typed key object.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        public virtual string? ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(string);
            }
            return TypeDescriptor.GetConverter(typeof(string)).ConvertFromInvariantString(id) as string;
        }

        /// <summary>
        /// Converts the provided <paramref name="id"/> to its string representation.
        /// </summary>
        /// <param name="id">The id to convert.</param>
        /// <returns>An <see cref="string"/> representation of the provided <paramref name="id"/>.</returns>
        public virtual string? ConvertIdToString(string id)
        {
            if (id.Equals(default(string)))
            {
                return null;
            }
            return id.ToString();
        }

        /// <summary>
        /// 如果已释放，则抛出异常
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }






        #endregion Other
    }
}
