# Family Hub web framework and components

This package is designed to work in conjunction with the [familyhubs-frontend](https://www.npmjs.com/package/familyhubs-frontend) node package, to rapidly create new GOV.UK websites and add standard components.

It builds on top of these packages:
* [GOV.UK Design System](https://www.npmjs.com/package/govuk-frontend)
* [Ministry of Justice Frontend](https://www.npmjs.com/package/@ministryofjustice/frontend)
* [DfE Frontend](https://www.npmjs.com/package/dfe-frontend)
* [Accessible autocomplete](https://github.com/alphagov/accessible-autocomplete)

The package contains:
* configurable standard GOV.UK layout (although only the DfE header is included)
* configurable header and footer links
* cookie banner and page with pluggable content
* Google Analytics support
* GOV.UK pagination support
* Error handling, error pages and GOVUK error summary component
* MOJ dashboard support
* alternative configs for when pages need different layouts, headers, footers etc.
* Url helpers for picking Urls from config, manipulating them and inheriting them from ancestor configs
* a set of SASS files that import the above packages and add some additional styling
* .NET distributed cache support for SQL Server and Redis
* JavaScript postcode helpers
* HTTP security header support

There is an [example ASP.Net 7 site](https://github.com/DFE-Digital/fh-web-components/tree/main/example/FamilyHubs.Example) that shows how to use the packages.

## Consuming the packages

Install the familyhubs-frontend package into the website project using the following command:

```
npm install familyhubs-frontend
```

Installing the package, will add files to the wwwroot folder. (todo document which files)

In the styles/application.scss file, add the following line:

```
@import "../node_modules/familyhubs-frontend/styles/all";
```

Add the FamilyHubs.SharedKernel.Razor package to the website project.

The FamilyHubs.SharedKernel.Razor package contains:

* the layout
* common shared partial views
* todo add rest here

Check that the npm package and the Razor Class Library are on the same version.

Add the configuration section to the appsettings.json file of the website project.

### Configuration

Here's an example configuration section that should be added to the appsettings.json file of a Family Hubs website:

```json
  "FamilyHubsUi": {
    "ServiceName": "Manage family support services and accounts",
    "Phase": "Beta",
    "FeedbackUrl": "https://example.com/feedback",
    "SupportEmail": "find-support-for-your-family.service@education.gov.uk",
    "Analytics": {
      "CookieName": "manage_family_support_cookies_policy",
      "CookieVersion": 1,
      "MeasurementId": "",
      "ContainerId": ""
    },
    "Header": {
	  "NavigationLinks": [
		{ "Text": "Requests Sent", "Url": "https://dev.manage-connection-requests.education.gov.uk/" },
		{ "Text": "Search for service", "Url": "/ProfessionalReferral/Search" },
	  ],
      "ActionLinks": [
		{ "Text": "My account", "Url": "/account/my-account" },
		{ "Text": "Sign out", "Url": "/account/signout" }
	  ]
	},
    "Footer": {
      "Links": [
        { "Text": "Accessibility" },
        { "Text": "Contact Us" },
        { "Text": "Cookies" },
        { "Text": "Feedback", "ConfigUrl": "FamilyHubsUi:FeedbackUrl" },
        { "Text": "Terms and conditions" }
      ] 
    } 
```

Notes:

* Google Analytics is only enabled if the MeasurementId and ContainerId are set.

* The Options classes have XML documentation on the properties.

* If your cookie page is at a different location to `/cookies`, you can set it using `CookiePageUrl` in the `Analytics` section.

## Version numbers

To ease testing, we should keep the version number of the NPM package and the Razor Class Library in sync. Consumers should then ensure that both packages are on the same version.

The version of the familyhubs-frontend package is given in its package.json file, as the value of the version property.

The version of the FamilyHubs.SharedKernel.Razor package is given in its FamilyHubs.SharedKernel.Razor.csproj file, as the value of the VersionPrefix property.

## familyhubs-frontend

To publish this npm package, you�ll need to follow these steps:

* Create a user account on the npm website if you don�t already have one.
* In your terminal or command prompt, navigate to the `familyhubs-frontend` directory, containing the package files.
* Run the `npm login` command and enter your npm username, password, and email when prompted.
* Update the package.json file in the package directory with the version number synced to the FamilyHubs.SharedKernel.Razor version.
* Run the `npm publish` command to publish the package to the npm registry.

After publishing the package, it will be available for others to install and use nearly instantaneously.

It's best to reference the package using its exact version number, otherwise it might not pick up the latest, just published version.

## FamilyHubs.SharedKernel.Razor

The package is automatically built when the solution is built.

It is not currently published automatically to the NuGet feed, and needs to be manually uploaded to NuGet.

## Components

### Cookie page

Call `AddCookiePage()` on your `IServiceCollection`, like so...

```
    services.AddCookiePage(configuration);
```

Create a new Razor Page. Inject ICookiePage into the PageModel's constructor, stash it away, then pass it to the cookie page partial in the View.

To add support for users running without Javascript, add an OnPost method as per the example.

E.g.

```
public class IndexModel : PageModel
{
    public readonly ICookiePage CookiePage;

    public IndexModel(ICookiePage cookiePage)
    {
        CookiePage = cookiePage;
    }

    public void OnPost(bool analytics)
    {
        CookiePage.OnPost(analytics, Request, Response);
    }
}
```

and add in your view...

```
    <partial name="~/Pages/Shared/_CookiePage.cshtml" model="Model.CookiePage"/>
```

Add a partial view called `Pages/Shared/_CookiePolicy.cshtml` and add the cookie policy content into it.

If you want to pick up the cookie policy content from a different partial view, pass its name into `AddCookiePage()`, e.g.

```
    services.AddCookiePage(configuration, "SomeOtherView.cshtml");
```

### User-friendly, branded error pages

To add user-friendly Family Hub branded error pages, call `UseErrorHandling()` on `WebApplication`, e.g.

```
    app.UseErrorHandling();
```

By default, the error handling middleware will only be added if it's not the development environment. If you want to always add it, irrespective of the environment (useful for local testing), pass `true` as the first parameter.

If `SupportEmail` is set in the configuration, the error page will include a link to the given support email address.

To test the not found page, navigate to a URL that doesn't exist, e.g. `/not-found`.

To test the error page, navigate to `/error/test`, which is a fault-injection page included in the library, explicitly for testing the error page handling.

## [Release Notes](https://github.com/DFE-Digital/fh-web-components/blob/main/docs/ReleaseNotes.md)

## [Possible Improvements](https://github.com/DFE-Digital/fh-web-components/blob/main/docs/historic/3.0.0/ToDo.md)
