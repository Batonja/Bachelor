using Common.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    public class User
    {
        public User()
        {
        }

        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Salt { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Key { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }
        public long Phone { get; set; }

        public long PassportId { get; set; }
        public Roles Role { get; set; }


        public string TokenId { get; set; }

        public virtual ICollection<Friend> FriendsOf { get; set; }
        public virtual ICollection<Friend> FriendsWith { get; set; }
    }
}
