

namespace AwesomeDevEvents.Api.Models
{
    public class DevEventViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DevEventSpeakerViewModel>? Speakers { get; set; }
    }

    public class DevEventSpeakerViewModel 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TalkTitle { get; set; }
        public string? TalkDescrition { get; set; }
        public string? LinkedInProfile { get; set; }
    }
}
