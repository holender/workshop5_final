using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Work3.Core.Entities;
using Work3.WebApi.Controllers;

namespace Work3.WebApi.Services
{
	/// <summary>
	/// User Services
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Authenticate
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		User Authenticate(string username, string password);

		/// <summary>
		/// Get All
		/// </summary>
		/// <returns></returns>
		IEnumerable<User> GetAll();

		/// <summary>
		/// Get By Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		User GetById(Guid id);
	}

	/// <summary>
	/// User Service
	/// </summary>
	public class UserService : IUserService
	{
		private List<User> _users = new List<User>
		{
			new User { Id = Guid.NewGuid(), FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
			new User { Id = Guid.NewGuid(), FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User }
		};

		private readonly AppSettings _appSettings;

		public UserService(IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
		}
		public User Authenticate(string username, string password)
		{
			var user = _users.SingleOrDefault(x =>
				x.Username == username &&
				x.Password == password);

			if (user == null)
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Id.ToString()),
					new Claim(ClaimTypes.Role, user.Role)
				}),
				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			user.Token = tokenHandler.WriteToken(token);

			// remove password before returning
			user.Password = null;

			return user;
		}

		public IEnumerable<User> GetAll()
		{
			return _users.Select(x => {
				x.Password = null;
				return x;
			});
		}

		public User GetById(Guid id)
		{
			var user = _users.FirstOrDefault(x => x.Id == id);

			if (user != null)
				user.Password = null;

			return user;
		}
	}
}