using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServer.Services
{
    public class ScopeStore : IScopeStore
    {
        private readonly IEnumerable<Scope> _scopes;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:IdentityServer3.Core.Services.InMemory.InMemoryScopeStore" /> class.
        /// </summary>
        /// <param name="scopes">The scopes.</param>
        public ScopeStore(IEnumerable<Scope> scopes)
        {
            this._scopes = scopes;
        }
        /// <summary>
        /// Gets all scopes.
        /// </summary>
        /// <returns>
        /// List of scopes
        /// </returns>
        public Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null)
            {
                throw new ArgumentNullException("scopeNames");
            }
            var scopes =
                (from s in this._scopes
                where scopeNames.ToList<string>().Contains(s.Name)
                select s).ToList();

            scopes.AddRange(StandardScopes.All);

            return Task.FromResult<IEnumerable<Scope>>(scopes.ToList<Scope>());
        }
        /// <summary>
        /// Gets all defined scopes.
        /// </summary>
        /// <param name="publicOnly">if set to <c>true</c> only public scopes are returned.</param>
        /// <returns></returns>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            if (publicOnly)
            {
                return Task.FromResult<IEnumerable<Scope>>(
                    from s in this._scopes
                    where s.ShowInDiscoveryDocument
                    select s);
            }
            return Task.FromResult<IEnumerable<Scope>>(this._scopes);
        }
    }
}