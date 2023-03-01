﻿using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Models.Validations
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSize = new[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames =
            { nameof(Restaurant.Name), nameof(Restaurant.Category), nameof(Restaurant.Address.City) };
        public RestaurantQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSize)}]");
                }
            });

            RuleFor(x => x.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
