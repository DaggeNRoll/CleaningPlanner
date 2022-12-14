using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public class PersonCleaningSpaceModel
    {
        public int CleaningSpaceId { get; set; }
        public CleaningSpaceViewModel CLeaningSpace { get; set; }
        public List<UserViewModel> Users { get; set; }
        public int SelectedUserId { get; set; } = 0;
    }

    public class PersonCleaningSpaceApiModel
    {
        public CleaningSpaceApiModel CleaningSpace { get; set; }
        public int SelectedUserId { get; set; }
    }
}
