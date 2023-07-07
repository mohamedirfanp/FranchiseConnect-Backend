

using Franchise;

namespace ApiGateway.Dto_Models
{
    public class CreateFranchiseDto
    {
        public FranchiseDto Franchise { get; set; }

        public List<string> GalleryRequestList { get; set; }

        public List<FranchiseServiceModelRequest> FranchiseServiceRequestsList { get; set; }

        public FranchiseSocialModelRequest FranchiseSocialRequest { get; set; }


    }
    
}
