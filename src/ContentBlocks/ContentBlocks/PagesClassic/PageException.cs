﻿using System;

namespace ChillSite.ContentBlocks.PagesClassic;

public class PageException : Exception
{
    public PageException()
    {
    }

    public PageException(string message) : base(message)
    {
    }

    public PageException(string message, Exception inner) : base(message, inner)
    {
    }
}