namespace Community_Library_Management_API.Dtos.Members
{
    public class MemberCreateDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MembershipType { get; set; } = string.Empty;
    }
}
