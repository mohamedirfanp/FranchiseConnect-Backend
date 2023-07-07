﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public FranchiseRequestHandler(FranchiseConnectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public FranchiseUserResponse CreateFranchiseRequest(CreateFranchiseUserRequest userRequest)
        {
            try
            {
                var requestModel = _context.FranchiseRequestModel.Any(freq => freq.FranchiseId == userRequest.FranchiseId && freq.CreatedId == userRequest.UserId && freq.IsRequestStatus == "Pending");
                /*
                                if(requestModel)
                                {
                                    throw new Exception("Request Already Made. Please wait for franchisor approval");
                                }*/


                Models.FranchiseRequestModel model = new Models.FranchiseRequestModel();
                model.FranchiseId = userRequest.FranchiseId;
/*                model.FranchiseCustomizedOption = userRequest.FranchiseCustomizationOption;
                model.FranchiseSampleBoxOption = userRequest.FranchiseSampleBoxOption;*/
                //model.FranchiseCustomizedOptionId = userRequest.FranchiseCustomizationOption ? userRequest.FranhiseCustomizationId : 0;
                model.ownerId = userRequest.OwnerId;
                model.IsRequestStatus = "Pending";
                model.CreatedAt = DateTime.Now;
                model.CreatedId = userRequest.UserId;

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
               /* foreach (var request in responseFromDB)
                {
                    dynamic tempObj = new System.Dynamic.ExpandoObject();
                    tempObj.franchiseRequest = request;
                    response.FranchiseRequest = Google.Protobuf.WellKnownTypes.Any.Pack(tempObj.franchiseRequest);
                    Dictionary<int, List<FranchiseCustomizedOptionModel>> franchiseCustomizedOptionModelResponse = new Dictionary<int, List<FranchiseCustomizedOptionModel>>();

                    if (request.FranchiseCustomizedOption)
                    {
                        *//*var customizedOptionResponse = _context.FranchiseCustomizedOptionModel.Include(f => f.FranchiseSelectedServices).Where(f => f.FranchiseCustomizedOptionId == request.FranchiseCustomizedOptionId).ToList();

                        franchiseCustomizedOptionModelResponse.Add(request.FranchiseCustomizedOptionId, customizedOptionResponse);

                        tempObj.franchiseCustomization = franchiseCustomizedOptionModelResponse;
                        response.FranchiseCustomization.Add(tempObj.franchiseCustomization);*//*
                    }
                    responseList.Responses.Add(response);


                }*/

               
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
                /* foreach (var request in responseFromDB)
                 {
                     dynamic tempObj = new System.Dynamic.ExpandoObject();
                     tempObj.franchiseRequest = request;
                     response.FranchiseRequest = Google.Protobuf.WellKnownTypes.Any.Pack(tempObj.franchiseRequest);
                     Dictionary<int, List<FranchiseCustomizedOptionModel>> franchiseCustomizedOptionModelResponse = new Dictionary<int, List<FranchiseCustomizedOptionModel>>();

                     if (request.FranchiseCustomizedOption)
                     {
                         *//*var customizedOptionResponse = _context.FranchiseCustomizedOptionModel.Include(f => f.FranchiseSelectedServices).Where(f => f.FranchiseCustomizedOptionId == request.FranchiseCustomizedOptionId).ToList();

                         franchiseCustomizedOptionModelResponse.Add(request.FranchiseCustomizedOptionId, customizedOptionResponse);

                         tempObj.franchiseCustomization = franchiseCustomizedOptionModelResponse;
                         response.FranchiseCustomization.Add(tempObj.franchiseCustomization);*//*
                     }
                     responseList.Responses.Add(response);
                 }*/

                return responseList;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Unknown, ex.Message));
            }

        }
    }
}
