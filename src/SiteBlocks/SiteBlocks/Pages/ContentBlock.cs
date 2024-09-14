﻿using System;

namespace ChillSite.SiteBlocks.Pages;

public abstract record ContentBlock(
    Guid ContentBlockId, // todo: maybe string or add content block name?
    ContentBlockComponentType ContentBlockComponentType);