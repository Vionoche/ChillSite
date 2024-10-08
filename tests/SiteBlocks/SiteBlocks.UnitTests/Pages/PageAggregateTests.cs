﻿using System;
using System.Linq;
using FluentAssertions;
using Stellaxis.SiteBlocks.Common;
using Stellaxis.SiteBlocks.Pages;
using Stellaxis.SiteBlocks.Pages.Aggregates;
using Xunit;

namespace Stellaxis.SiteBlocks.UnitTests.Pages;

public class PageAggregateTests
{
    [Fact]
    public void Test_Page_Creation_And_Publication()
    {
        var mainLayout = new LayoutComponentType(
            new LayoutComponentTypeName("MainLayout"),
            typeof(object));

        var pageTemplate = new TemplateComponentType(
            new TemplateComponentTypeName("HomePage"),
            typeof(object),
            mainLayout);
        
        var page = Page.Create(
            _dateTimeProvider,
            _domainEventBuffer,
            pageTemplate,
            name: "Home",
            title: "Home",
            description: "The main page",
            seoDescription: null,
            seoKeywords: null);

        var pageAggregate = new PageAggregate(_dateTimeProvider, _domainEventBuffer, page);
        
        pageAggregate.Update("Home", "Home page", "Welcome site", null);
        pageAggregate.Publish();

        page = pageAggregate.Page;

        page.PageId.Should().NotBeEmpty();
        page.TemplateComponentType.Should().Be(pageTemplate);
        page.Title.Should().Be("Home");
        page.Description.Should().Be("Home page");
        page.SeoDescription.Should().Be("Welcome site");
        page.SeoKeywords.Should().BeNull();
        page.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        page.ModificationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    [Fact]
    public void Test_Page_Name_Validation()
    {
        var mainLayout = new LayoutComponentType(
            new LayoutComponentTypeName("MainLayout"),
            typeof(object));

        var pageTemplate = new TemplateComponentType(
            new TemplateComponentTypeName("HomePage"),
            typeof(object),
            mainLayout);
        
        var page = Page.Create(
            _dateTimeProvider,
            _domainEventBuffer,
            pageTemplate,
            name: "Home",
            title: "Home",
            description: "The main page",
            seoDescription: null,
            seoKeywords: null);
        
        var pageAggregate = new PageAggregate(_dateTimeProvider, _domainEventBuffer, page);

        pageAggregate.UpdateName("Home01");
        pageAggregate.UpdateName("Home_Page");
        pageAggregate.UpdateName("Home-Page");
        pageAggregate.UpdateName("Главная");
        pageAggregate.UpdateName("Главная01");
        pageAggregate.UpdateName("Главная_Страница");
        pageAggregate.UpdateName("Главная-Страница");
        
       var action = () =>  pageAggregate.UpdateName("Home 01");
       action.Should().Throw<Exception>();
       
       action = () =>  pageAggregate.UpdateName("Home's Page");
       action.Should().Throw<Exception>();
       
       action = () =>  pageAggregate.UpdateName("Home!");
       action.Should().Throw<Exception>();
       
       action = () =>  pageAggregate.UpdateName("Home%");
       action.Should().Throw<Exception>();
       
       action = () =>  pageAggregate.UpdateName("");
       action.Should().Throw<Exception>();
       
       action = () =>  pageAggregate.UpdateName(new string(Enumerable.Range(0, 30).Select(_ => 'w').ToArray()));
       action.Should().Throw<Exception>();
    }
    
    private readonly IDateTimeProvider _dateTimeProvider = new UtcDateTimeProvider();
    private readonly IDomainEventBuffer _domainEventBuffer = new InMemoryDomainEventBuffer(new UtcDateTimeProvider());
}