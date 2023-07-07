namespace ApiGateway.Dto_Models
{
    public class CreateRequestDto
    {
        public int FranchiseId {  get; set; }

        public int OwnerId { get; set; }


        public List<int> ServicesId { get; set; }
    }
}
