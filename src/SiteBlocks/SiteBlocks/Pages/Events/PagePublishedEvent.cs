﻿using System;
using Stellaxis.SiteBlocks.Common;

namespace Stellaxis.SiteBlocks.Pages.Events;

public record PagePublishedEvent: DomainEvent
{
    public Guid PageId { get; }

    public PagePublishedEvent(
        Guid eventId,
        DateTime occuredOn,
        Guid pageId) : base(eventId, occuredOn)
    {
        PageId = pageId;
    }
    
    public static PagePublishedEvent Create(IDateTimeProvider dateTimeProvider, Guid pageId)
    {
        return new PagePublishedEvent(
            eventId: Guid.NewGuid(),
            occuredOn: dateTimeProvider.UtcNow,
            pageId);
    }
}