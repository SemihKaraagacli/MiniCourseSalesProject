﻿namespace MiniCourseSalesProject.Web.Models.Dtos
{
    public record OrderResponse(Guid Id, List<BasketItemInCourseResponse> BasketItemInCourseResponses, decimal Wallet, Guid UserId, decimal TotalAmount, DateTime CreatedDate, DateTime UpdatedDate, string Status);
}