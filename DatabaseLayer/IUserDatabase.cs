using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    public interface IUserDatabase
    {
        bool SignUp(User user);

        List<User> GetUsers();
        List<Friend> GetFriends();
        Friend ConfirmFriendship(Friend friend);
        User GetUserByPassportId(long passportId);

        User FindUserOrAdd(User user);
        bool AddFriend(Friend friend);
    }
}
