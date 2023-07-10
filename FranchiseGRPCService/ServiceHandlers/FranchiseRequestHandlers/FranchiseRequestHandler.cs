using AccoutGRPCService.Protos;
using AutoMapper;
using ChatGRPCService.Protos;
using ChatPackage;
using Franchise;
using FranchiseGRPCService.Data;
using FranchiseGRPCService.MicroservicesClients.AccountClient;
using FranchiseGRPCService.MicroservicesClients.ConversationClients;
using FranchiseGRPCService.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace FranchiseGRPCService.ServiceHandlers.FranchiseRequestHandlers
{
    public class FranchiseRequestHandler : IFranchiseRequestHandler
    {
        private readonly FranchiseConnectContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountClientService _accountClientService;
        private readonly IConversationClientService _conversionClientService;

        public FranchiseRequestHandler(FranchiseConnectContext context, IMapper mapper, IAccountClientService accountClientService, IConversationClientService conversationClientService)
        {
            _context = context;
            _mapper = mapper;
            _accountClientService = accountClientService;
            _conversionClientService = conversationClientService;
        }
        public FranchiseUserResponse CreateFranchiseRequest(CreateFranchiseUserRequest userRequest)
        {
            try
            {
                var requestModel = _context.FranchiseRequestModel.Any(freq => freq.FranchiseId == userRequest.FranchiseId && freq.CreatedId == userRequest.UserId && freq.IsRequestStatus == "Pending");


                // DEV - Undo in PROD

                if (requestModel)
                {
                    throw new Exception("Request Already Made. Please wait for franchisor approval");
                }


                // Get Franchise Info

                var franchise = _context.FranchiseModel.Where(f => f.FranchiseId == userRequest.FranchiseId).FirstOrDefault();

                // Get the Profile for Franchisor owner and Franchisee

                // Profile - Franchise Owner
                GetProfileResponse ownerProfile = _accountClientService.GetProfile(userRequest.OwnerId);
                Console.WriteLine(ownerProfile);

                // Profile - Franchisee
                GetProfileResponse userProfile = _accountClientService.GetProfile(userRequest.UserId);
                Console.WriteLine(userProfile);

                // Create a conversion between two party
                // TODO - Optional - ADD profile photo link
                CreateConversationResponse newConversion = _conversionClientService.CreateConversation(new CreateConversationRequest
                {
                    FranchiseeId = userRequest.UserId,
                    FranchiseeName = userProfile.Response.Name,
                    FranchisorId = userRequest.OwnerId,
                    FranchisorName = ownerProfile.Response.Name,
                    FranchisorFranchiseName = franchise.FranchiseName
                });


                Models.FranchiseRequestModel model = new Models.FranchiseRequestModel();
                model.FranchiseId = userRequest.FranchiseId;
                model.ownerId = userRequest.OwnerId;
                model.IsRequestStatus = "Pending";
                model.CreatedAt = DateTime.Now;
                model.CreatedId = userRequest.UserId;
                model.ConversionId = newConversion.ConversationId;
                model.InvestmentBudget = userRequest.InvestmentBudget;
                model.Space = userRequest.Space;



                _context.FranchiseRequestModel.Add(model);

                _context.SaveChanges();

                foreach (var selectedService in userRequest.ServicesId)
                {
                    FranchiseSelectedServiceModel franchiseSelectedService = new FranchiseSelectedServiceModel();
                    franchiseSelectedService.FranchiseProvideServiceId = selectedService;
                    franchiseSelectedService.FranchiseRequestId = model.FranchiseRequestId;
                    _context.franchiseSelectedServiceModels.Add(franchiseSelectedService);
                }
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
                var responseFromDB = _context.FranchiseRequestModel.Where(f => f.ownerId == userID.UserId && f.IsRequestStatus == "Pending").Include(f => f.franchiseId).Include(f => f.franchiseId.franchiseGallery).Include(f => f.franchiseId.franchiseServices).Include(f => f.FranchiseSelectedServices).ToList();


                var responseList = new FranchiseRequestResponseList();

                List<FranchiseRequestResponse> franchiseRequestList = new List<FranchiseRequestResponse>();

                foreach (var request in responseFromDB)
                {
                    FranchiseRequestResponse franchiseRequest = new FranchiseRequestResponse()
                    {
                        Franchise = _mapper.Map<Franchise.Franchise>(request.franchiseId),
                        FranchiseRequest = _mapper.Map<Franchise.FranchiseRequestModel>(request)
                    };
                    franchiseRequest.FranchiseGalleryList.Add(TypeCastingGalleryList(request.franchiseId.franchiseGallery));
                    franchiseRequest.FrachiseServiceList.Add(TypeCastingServiceList(request.franchiseId.franchiseServices));
                    franchiseRequest.FranchiseSelectedServiceList.Add(TypeCastingSelectedServiceList(request.FranchiseSelectedServices));

                    franchiseRequestList.Add(franchiseRequest);
                }

                responseList.Responses.Add(franchiseRequestList);
               
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
                // TODO : Update Request Accepted Status in Conversation Table


                var requestModel = _context.FranchiseRequestModel.Where(f => f.FranchiseRequestId == updateStatusRequest.FranchiseRequestId).FirstOrDefault();

                CommonResponse response = _conversionClientService.UpdateConversationStatus(new UpdateAcceptedStatusRequest
                {
                    ConversationId = (int)requestModel.ConversionId,
                    Status = updateStatusRequest.IsRequestStatus

                });

                Console.WriteLine(response);


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


        private List<FranchiseGalleryResponse> TypeCastingGalleryList(ICollection<FranchiseGalleryModel> franchiseGallery)
        {
            List<FranchiseGalleryResponse> franchiseGalleryresponseFromProto = new List<FranchiseGalleryResponse>();
            foreach (var gallery in franchiseGallery)
            {
                franchiseGalleryresponseFromProto.Add(new FranchiseGalleryResponse()
                {
                    FranchiseGalleryId = gallery.FranchiseGalleryId,
                    FranchiseId = gallery.FranchiseId,
                    FranchisePhotoUrl = gallery.FranchisePhotoUrl,

                });
            }
            return franchiseGalleryresponseFromProto;
        }
        private List<FranchiseServiceModelResponse> TypeCastingServiceList(ICollection<FranchiseServiceModel> franchiseServiceModels)
        {
            List<FranchiseServiceModelResponse> franchiseServiceresponseFromProto = new List<FranchiseServiceModelResponse>();
            foreach (var service in franchiseServiceModels)
            {
                franchiseServiceresponseFromProto.Add(new FranchiseServiceModelResponse()
                {
                    FranchiseId = service.FranchiseId,
                    FranchiseProvideServiceName = service.FranchiseProvideServiceName,
                    FranchiseProvideServiceDescription = service.FranchiseProvideServiceDescription,
                    FranchiseServiceId = service.FranchiseServiceId,
                    FranchiseCustomizationStatus = service.FranchiseServiceCustomizationStatus



                });
            }
            return franchiseServiceresponseFromProto;
        }
        private List<FranchiseSelectedServiceResponse> TypeCastingSelectedServiceList(ICollection<FranchiseSelectedServiceModel> franchiseServiceModels)
        {
            List<FranchiseSelectedServiceResponse> franchiseSelectedServiceresponseFromProto = new List<FranchiseSelectedServiceResponse>();
            foreach (var service in franchiseServiceModels)
            {
                franchiseSelectedServiceresponseFromProto.Add(new FranchiseSelectedServiceResponse()
                {
                   FranchiseProvideServiceId = service.FranchiseProvideServiceId,
                   FranchiseSelectedServiceId = service.FranchiseSelectedServiceId,
                   RequestId = service.FranchiseRequestId


                });
            }
            return franchiseSelectedServiceresponseFromProto;
        }

        public FranchiseRequestResponseList GetFranchiseAllRequest(GetFranchiseRequestByUserID userID)
        {
            try
            {
                var responseFromDB = _context.FranchiseRequestModel.Where(f => f.ownerId == userID.UserId).Include(f => f.franchiseId).Include(f => f.franchiseId.franchiseGallery).Include(f => f.franchiseId.franchiseServices).Include(f => f.FranchiseSelectedServices).ToList();


                var responseList = new FranchiseRequestResponseList();

                List<FranchiseRequestResponse> franchiseRequestList = new List<FranchiseRequestResponse>();

                foreach (var request in responseFromDB)
                {
                    FranchiseRequestResponse franchiseRequest = new FranchiseRequestResponse()
                    {
                        Franchise = _mapper.Map<Franchise.Franchise>(request.franchiseId),
                        FranchiseRequest = _mapper.Map<Franchise.FranchiseRequestModel>(request)
                    };
                    franchiseRequest.FranchiseGalleryList.Add(TypeCastingGalleryList(request.franchiseId.franchiseGallery));
                    franchiseRequest.FrachiseServiceList.Add(TypeCastingServiceList(request.franchiseId.franchiseServices));
                    franchiseRequest.FranchiseSelectedServiceList.Add(TypeCastingSelectedServiceList(request.FranchiseSelectedServices));

                    franchiseRequestList.Add(franchiseRequest);
                }

                responseList.Responses.Add(franchiseRequestList);
                

                return responseList;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }
    }
}
