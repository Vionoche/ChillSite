﻿using FluentValidation;

namespace ChillSite.ContentBlocks.PagesClassic.Rules;

public record PageUnpublishingRule(bool IsPublished);

public class PageUnpublishingRuleValidator : AbstractValidator<PageUnpublishingRule>
{
    public PageUnpublishingRuleValidator()
    {
        RuleFor(x => x.IsPublished)
            .Equal(true)
            .WithMessage($"The page already has been unpublished.");
    }
}