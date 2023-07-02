using Franchise;
using FranchiseGRPCService.Data;
using FranchiseGRPCService.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseRequestHandlers
{
    public class FranchiseRequestHandler : IFranchiseRequestHandler
    {
        private readonly FranchiseConnectContext _context;

        public FranchiseRequestHandler(FranchiseConnectContext context)
        {
            _context = context;
        }
        public FranchiseUserResponse CreateFranchiseRequest(CreateFranchiseUserRequest userRequest)
        {
            try
            {
                FranchiseRequestModel model = new FranchiseRequestModel();
                model.FranchiseId = userRequest.FranchiseId;
                model.FranchiseCustomizedOption = userRequest.FranchiseCustomizationOption;
                model.FranchiseSampleBoxOption = userRequest.FranchiseSampleBoxOption;
                model.FranchiseCustomizedOptionId = userRequest.FranchiseCustomizationOption ? userRequest.FranhiseCustomizationId : 0;
                model.ownerId = userRequest.OwnerId;
                model.IsRequestStatus = "Pending";
                

                _context.FranchiseRequestModel.Add(model);

                _context.SaveChanges();

                return new FranchiseUserResponse
                {
                    Response = "Successfully Request Submitted"
                };

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }
        }

        public FranchiseRequestResponseList GetFranchiseRequest(GetFranchiseRequestByUserID userID)
        {
            try
            {
                var responseFromDB = _context.FranchiseRequestModel.Where(f => f.ownerId == userID.UserId).Include(f => f.franchiseId).Include(f => f.franchiseId.franchiseGallery).Include(f => f.franchiseId.franchiseServices).ToList();

                var responseList = new FranchiseRequestResponseList();
                var response = new FranchiseRequestResponse();
                foreach (var request in responseFromDB)
                {
                    dynamic tempObj = new System.Dynamic.ExpandoObject();
                    tempObj.franchiseRequest = request;
                    response.FranchiseRequest = Google.Protobuf.WellKnownTypes.Any.Pack(tempObj.franchiseRequest);
                    Dictionary<int, List<FranchiseCustomizedOptionModel>> franchiseCustomizedOptionModelResponse = new Dictionary<int, List<FranchiseCustomizedOptionModel>>();

                    if (request.FranchiseCustomizedOption)
                    {
                        var customizedOptionResponse = _context.FranchiseCustomizedOptionModel.Include(f => f.FranchiseSelectedServices).Where(f => f.FranchiseCustomizedOptionId == request.FranchiseCustomizedOptionId).ToList();

                        franchiseCustomizedOptionModelResponse.Add(request.FranchiseCustomizedOptionId, customizedOptionResponse);

                        tempObj.franchiseCustomization = franchiseCustomizedOptionModelResponse;
                        response.FranchiseCustomization.Add(tempObj.franchiseCustomization);
                    }
                    responseList.Responses.Add(response);


                }

               
                return responseList;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }

        public FranchiseUserResponse UpdateRequestStatus(UpdateStatusRequest updateStatusRequest)
        {
            try
            {

                var requestModel = _context.FranchiseRequestModel.Where(f => f.FranchiseRequestId == updateStatusRequest.FranchiseRequestId).FirstOrDefault();

                if (requestModel != null)
                {
                    requestModel.IsRequestStatus = updateStatusRequest.IsRequestStatus;

                    _context.FranchiseRequestModel.Update(requestModel);
                    _context.SaveChanges();

                    return new FranchiseUserResponse
                    {
                        Response = "Successfully Updated"
                    };

                }

                return new FranchiseUserResponse
                {
                    Response = "No Request Found"
                };
            }
            catch(Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }
    }
}
