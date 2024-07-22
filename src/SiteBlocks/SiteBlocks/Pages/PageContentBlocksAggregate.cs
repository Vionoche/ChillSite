﻿using System;
using System.Collections.Generic;
using System.Linq;
using ChillSite.SiteBlocks.ContentBlocks;

namespace ChillSite.SiteBlocks.Pages;

public class PageContentBlocksAggregate
{
    public Page Page { get; private set; }
    
    public IReadOnlyDictionary<PageContainer, ContentBlock[]> ContentBlocksMap => _contentBlocks;

    public PageContentBlocksAggregate(
        Page page,
        IEnumerable<PageContentBlock> pageContentBlocks)
    {
        Page = page;

        _contentBlocks = pageContentBlocks
            .GroupBy(x => x.PageContainer)
            .ToDictionary(
                grouping => grouping.Key,
                grouping => grouping
                    .OrderBy(pageContentBlock => pageContentBlock.ContainerPosition)
                    .Select(pageContentBlock => pageContentBlock.ContentBlock)
                    .ToArray());
    }

    public void AddContentBlock(PageContainer container, ContentBlock contentBlock)
    {
        throw new NotImplementedException();
    }
    
    private readonly Dictionary<PageContainer, ContentBlock[]> _contentBlocks;
}