using System.ComponentModel.DataAnnotations.Schema;


namespace Common.Models
{
    public class Friend
    {
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FriendshipId { get; set; }
        public int FriendOfId { get; set; }
        public int FriendWithId { get; set; }
        public bool Confirmed { get; set; }

        public User FriendOf { get; set; }
        public User FriendWith { get; set; }
    }
}
