namespace WebAPI.DTO
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Information { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime DateUpdating { get; set; }

        // Dados do usuário
        public UserDTO? User { get; set; }
    }
}
