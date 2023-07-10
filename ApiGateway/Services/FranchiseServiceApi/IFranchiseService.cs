using ApiGateway.Dto_Models;
using Franchise;
using FranchiseGRPCService.Protos;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.FranchiseService
{
    public interface IFranchiseService
    {
        // Get all Franchises
        GetFranchiseResponse GetAllFranchise();

        // Get Franchise by Id
        GetFranchisesRespsonse GetFranchiseById(int id);

        // Get Franchise by Owner Id 
        GetFranchisesRespsonse GetFranchiseByOwnerId();

        // Create a Franchise
        CreateFranchiseResponse CreateFranchise(CreateFranchiseDto request);

        // Franchise Exist or not
        FranchiseExistResponse GetFranchiseExist();


        // Incrementing View Count
        IncreseViewCountResponse IncrementViewCount(IncreseViewCountRequest request);


        /* Franchise Gallery */
        // Upload new Photo 
        IActionResult UploadGallery(FranchiseGalleryUploadRequest uploadRequest);

        // Delete a Photo
        IActionResult DeleteGallery(FranchiseGalleryDeleteRequest deleteRequest);


        /* Franchise Provide Service */

        // Create a Provide Service
        IActionResult CreateProvideService(CreateFranchiseServiceRequest serviceRequest);

        // Delete a Provide Service
        IActionResult DeleteProvidedService(int franchise_provide_service_id);

        /* Franchise Request Service */

        // Create a User Request for franchisee
        IActionResult CreateUserRequest(CreateRequestDto franchiseUserRequest);

        // Get All the Pending request for franchisor
        FranchiseRequestResponseList GetAllFranchiseRequest();

        // Get All the Requests for franchisor
        FranchiseRequestResponseList GetAllFranchiseRequests();

        // Update a Status of request from franchisor
        IActionResult UpdateRequestStatus(UpdateStatusRequest statusRequest);


        /* User Wishlist Service */

        // Add a new wishlist
        IActionResult AddUserWishlist(CommonRequest userRequest);

        // Get all wishlist for franchisee
        GetUserWishListResponse GetAllWishlist();

        // Remove a wishlist
        IActionResult RemoveUserWishlist(RemoveUserWishListRequest removeUserWishRequest);





    }
}
