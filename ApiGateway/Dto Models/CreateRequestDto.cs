namespace ApiGateway.Dto_Models
{
    public class CreateRequestDto
    {
        public int FranchiseId {  get; set; }

        public int OwnerId { get; set; }

        public List<int> ServicesId { get; set; }

        public string InvestmentBudget { get; set; }

        public string Space { get; set; }
    }
}
