namespace RazorPagesLibraryProject.DTOes
{
    public class ResponseDTO<T>
    {
        public int Count { get; set; }
        public IEnumerable<T>? Entities { get; set; } = new List<T>();
    }
}
