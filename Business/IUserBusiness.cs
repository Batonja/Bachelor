using Common.ErrorObjects;
using Common.Events;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IUserBusiness
    {
        Holder<User> SignIn(User user);
        Holder<Friend> AddFriend(Friend friend);
        Holder<User> SignUp(User user);
        Holder<Friend> ConfirmFriendship(Friend friend);
        string Encrypt(string secret);
        List<Friend> GetFriends();
        User FindUserOrAdd(User user);
        Task<Guid> AddEvent(UserAdded userAdded);
        List<User> GetUsers();
    }
}
