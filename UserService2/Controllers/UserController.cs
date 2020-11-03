using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Common.ErrorObjects;
using Common.Events;
using Common.Models;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace UserService2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IMediator _mediatr; 
        IUserBusiness _userBusiness;
        IHttpClientFactory _httpClientFactory;
        public UserController(IUserBusiness userBusiness,IMediator mediator, IHttpClientFactory clientFactory)
        {
            _userBusiness = userBusiness;
            _mediatr = mediator;
            _httpClientFactory = clientFactory;
        }

        [HttpPost]
        public Holder<Friend> ConfirmFriendship([FromBody]Friend friend)
        {
            return _userBusiness.ConfirmFriendship(friend);
        }


        // POST: api/User
        [HttpPost]
        public Holder<User> SignUp([FromBody] User user)
        {
            Holder<User> retVal = _userBusiness.SignUp(user);
            if ((int)retVal.ErrorCode == 200)
            { 
                _userBusiness.AddEvent(new UserDeleted() { User = retVal.Value });
                UserCreatedAnnouncement();
            }
            return retVal;

        }

        public async Task UserCreatedAnnouncement()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/api/flight/UserIsCreated");
            HttpClient client = _httpClientFactory.CreateClient();

            await client.SendAsync(request);



        }

        [HttpPost]
        public Holder<User> SignIn([FromBody] User user)
        {

            return _userBusiness.SignIn(user);
        }


        [HttpGet]
        public List<User> GetUsers()
        {
            List<User> users = _userBusiness.GetUsers();

            return users;
            
        }

        [HttpGet]
        public List<Friend> GetFriends()
        {
            return _userBusiness.GetFriends();
        }

        [HttpPost]
        public Holder<Friend> AddFriend([FromBody]Friend friend)
        {
            Holder<Friend> retVal = _userBusiness.AddFriend(friend);

            return retVal;
        }

        [HttpPost]
        public Holder<User> googleAuthorization([FromBody]User theUser)
        {
            string errorMessage = "";
            User user = new User();
            try
            {
                //SimpleLogger.Log("userView = " + userView.tokenId);
                var payload = GoogleJsonWebSignature.ValidateAsync(theUser.TokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;


                user = _userBusiness.FindUserOrAdd(theUser);



                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _userBusiness.Encrypt(AppSettings.appSettings.JwtEmailEncryption + user.Email)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.appSettings.JwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(String.Empty,
                  String.Empty,
                  claims,
                  expires: DateTime.Now.AddSeconds(55 * 60),
                  signingCredentials: creds);

                user.TokenId = new JwtSecurityTokenHandler().WriteToken(token);

                return CheckUser(user, 200, "");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                BadRequest(ex.Message);
            }
            return CheckUser(user, 400, errorMessage);
        }
        #region helpers
        public Holder<User> CheckUser(User user, int errorCode, string description) =>
                errorCode == 200 ? Holder<User>.Success(user) : Holder<User>.Fail(errorCode, description);
        #endregion
    }
}
