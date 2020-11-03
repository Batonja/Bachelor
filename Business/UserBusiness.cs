using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using Common.ErrorObjects;
using Common.Models;
using DatabaseLayer;

namespace BusinessLayer
{
    public class UserBusiness : IUserBusiness
    {
        IUserDatabase _userDatabase;

        public UserBusiness(IUserDatabase userDatabase)
        {
            _userDatabase = userDatabase;
        }

        public Holder<User> SignUp(User user)
        {
            User userFromDb = _userDatabase.GetUserByPassportId(user.PassportId);

            if (userFromDb != null)
                return CheckUser(user, 400, "User with this passport is already registered");


            List<User> usersFromDb = new List<User>();
            usersFromDb = _userDatabase.GetUsers();


            foreach (var userDb in usersFromDb)
                if (String.Compare(userDb.Email, user.Email) == 0)
                    return CheckUser(user, 400, "User with this email already exists");



            string[] encryptedUserPassword = PasswordHelper.EncryptPassword(user.Key);
            string safePassword = PasswordHelper.GetMeAString(encryptedUserPassword[1]);
            string salt = PasswordHelper.GetMeAString(encryptedUserPassword[0]);

            user.Key = safePassword;
            user.Salt = salt;



            return _userDatabase.SignUp(user) ? CheckUser(user, 200, "") : CheckUser(user, 400, "Unable to register user");


        }

        public Holder<Friend> AddFriend(Friend friend)
        {
            return _userDatabase.AddFriend(friend) ? CheckFriend(friend, 200, "") :
                CheckFriend(friend, 400, "Error while trying to add friend");
        }

        public List<Friend> GetFriends()
        {
            return _userDatabase.GetFriends();
        }

        public Holder<Friend> ConfirmFriendship(Friend friend)
        {
            Friend retval = _userDatabase.ConfirmFriendship(friend);

            return retval.FriendshipId == 0 ? CheckFriend(friend, 400, "Error while trying to confirm friendship")
                : CheckFriend(retval, 200, "");
        }

        public Holder<User> SignIn(User user)
        {
            List<User> usersFromDb = new List<User>();
            usersFromDb = _userDatabase.GetUsers();
            User userFromDb = new User();

            foreach (var userDb in usersFromDb)
            {
                if (userDb.Email == user.Email)
                {
                    userFromDb = userDb;
                    break;
                }
            }

            if (userFromDb.UserId == 0)
                return CheckUser(user, 404, "User doesn't exists");

            return PasswordHelper.Autheticate(userFromDb.Key, userFromDb.Salt, user.Key) ? CheckUser(userFromDb, 200, "") : CheckUser(user, 400, "User invalid");


        }

        public string Encrypt(string secret)
        {

            string ret = PasswordHelper.EncryptPassword(secret)[0];


            return ret;

        }

        public List<User> GetUsers()
        {
            List<User> users = _userDatabase.GetUsers();

            return users;
        }

        public User FindUserOrAdd(User user)
        {
            return _userDatabase.FindUserOrAdd(user);
        }

        #region helpers

        public Holder<User> CheckUser(User user, int errorCode, string description) =>
            errorCode == 200 ? Holder<User>.Success(user) : Holder<User>.Fail(errorCode, description);
        public Holder<Friend> CheckFriend(Friend friend, int errorCode, string description) =>
            errorCode == 200 ? Holder<Friend>.Success(friend) : Holder<Friend>.Fail(errorCode, description);

        #endregion
    }
}
