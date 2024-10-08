﻿using System.Collections.Generic;

namespace Stellaxis.SiteBlocks.Pages;

public record PageContainer(
    PageContainerName Name,
    IReadOnlyCollection<PageContentBlock> PageContentBlocks);
    
public record struct PageContainerName(
    string Value);