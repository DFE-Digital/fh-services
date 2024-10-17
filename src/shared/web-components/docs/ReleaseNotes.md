# Release Notes

# 9.5

Rename `<unordered-list>` to `govuk-ul` and improve.
Add `govuk-ol` tag helper.

# 9.4

Add `<unordered-list>` tag helper.

# 9.3

Add /info page which returns app's build version. Can include build number / commit hash etc.
To help identify what version of code is live.

* 9.3.5 Add support for data-testid attribute to summary rows (used by summary card and summary list)

# 9.2

Add support for a page component with a single autocomplete textbox (or select drop-down when no Javascript).

Also included:
* fix error summary layout on single textbox page component

* 9.2.1 => 9.2.2 miscellaneous single autocomplete improvements

# 9.1

Add hint text support to radio page component

Upgrade to GDS 5.2 with new tags, crown icon, improved accessibility etc
Upgrade to MOJ 2.1.1 and DFE Frontend 1.0.1

* 9.1.1 => 9.15 Fixes
* 9.1.6 Add support for a class attribute to `<Summary-List>` and `<Summary-Card>`
* 9.1.7 Add MS Clarity support. Add the Clarity ID to the `Analytics` config section with the key `ClarityId`

# 9

Add Summary Card support. See the example for more info.

Breaking changes:

* `<summary-list-row>` has been renamed to `<summary-row>`, as it's now used for both summary lists and summary cards.

## 8.4

Add `CommonRadios.YesNo` for use when the radio component is used for a yes/no question.

## 8.3

Add `fh-pre-wrap` class for preserving line breaks and spaces in a block of text.

Add class attribute support to `<SummaryListRow>`, which adds any classes supplied to the generated `<dd>`.

See the summary list example page for more info.

## 8.2

Add a full page checkbox component. See example page for more info.

## 8.1

Add support for a new full page control with radio buttons. See example page.

* 8.1.1 Fix heading size in radio page's legend
* 8.1.2 Fix back-links styling

# 8

Add support for a new full page control with a single textarea. See example page for more info.

Breaking changes:
* namespaces for classes and interfaces in the `FamilyHubs.SharedKernel.Razor.FullPages` namespace have changed.

* 8.0.1 : Make an empty text area an optional error condition (actually a breaking change!)

## 7.4

Add 'show-empty' attribute to the `<<summary-list-row>` tag helper. If set to true, both the key can value are shown, even if the value is empty.

## 7.3

Add time entry component.

* 7.3.1 : Add missing styles for the time entry component and add an example.
* 7.3.2 : Convert TimeComponent to immutable record (from mutable class).
* 7.3.3 : Add error support to the time entry component.

## 7.2

Fixed an issue with the add-another component when the first instance in the DOM is errored.

It used to duplicate the error message from the first instance in the new instance, but now it strips out any error related DOM or classes.

At some point, we might switch to duplicating a hidden template of mark-up in the DOM instead.

## 7.1

Add support for accessible-autocomplete to work with errored fields.

# 7

Breaking changes:
* update AddAnotherAutocompleteErrorChecker to report all instances of errors, rather than just the first. Also report _all_ sets of duplicate values
* remove the old error handling implementation

Update the add-another component example to work with error messages.

## 6.6

Add fh-add-another, a forked version of MOJ's add-another component, that works with accessible-autocomplete.

See the example page for more info.

* 6.6.1 : Make AddAnotherAutocompleteErrorChecker serializable.
* 6.6.2 : Fix Add Another when the selects are generated in the view with a pre-selected value.
* 6.6.3 : Fixes to `AddAnotherAutocompleteErrorChecker`.

## 6.5

Add support for accessible-autocomplete. See [GitHub](https://github.com/alphagov/accessible-autocomplete) and the [node package](https://www.npmjs.com/package/accessible-autocomplete) for more info.

## 6.4

Added `MaxLength` support to the single textbox component.

## 6.3

Added an improved error implementation. The original error handling is still available (until all known consumers have been switched to the new implementation). See the new example page for more info.

The single textbox page (and it's example page) has been updated to use the new error handling.

## 6.2

Add initial support for a full page, single textbox component. See the example pages for more info.

## 6.1

Add `<open-close-button target="elementId">`.

A button that's only visible on mobile that toggles the target element on and off.

See the example page for more options.

* 6.1.1 : Fix responsive handling wrt `<open-close-button>`.

# 6

Dashboards now support additional consumer supplied query parameters that get added to all links (such as the column sort and pagination links).

This is to support the use case of the dashboard displaying filterable content.

Dashboard columns can now be right aligned or [numeric](https://design-system.service.gov.uk/components/table/#numbers-in-a-table).

### Example website changes

There is now an example paginated dashboard, and the existing dashboard example has right aligned and numeric columns.

Breaking changes:
* `ColumnImmutable()` now accepts a `ColumnType` enum parameter, rather than the old `Align` enum.

## 5.8

Add Align parameter to ColumnImmutable constructor, to allow the alignment of dashboard columns to be specified.

Remove deprecated `fh-new-tab` attribute from `a` tag helper.

## 5.7

Add support for actions column to summary list row tag helper.

See Summary List example page for more info.

## 5.6

Add `AddFamilyHubsHealthChecks()` extension method to `IHealthChecksBuilder` to add:
* standard Family Hubs health checks, e.g. for any configured Feedback site
* health checks from configuration (see example site's `appsettings.json` for examples)

Add `MapFamilyHubsHealthChecks()` extension method to `WebApplication` to map the health checks to the `/health` endpoint.

## 5.5

Customise default [MOJ filter](https://design-patterns.service.justice.gov.uk/components/filter/) styling and add 'fh-' styles for enhancing the filtering, for such things as scrollable sub groups of radios/checkboxes.

The styles work in conjunction with the filtering classes introduced in v5.3.0.

## 5.4

### Visibility toggle

Add the attribute `data-toggle-visibility-id="id-of-element-to-toggle"` to an element, to make it toggle the visibility of the specified element, when it's clicked.

See the example page for more info.

## 5.3

### Summary Lists

Add summary list tag helpers, e.g.

```
<summary-list>
    <summary-list-row key="Name">Sarah Philips</summary-list-row>
</summary-list>
```

See the summary list example page for more info.

### &lt;a&gt; tag helper

New attributes are available for &lt;a&gt;'s...

`web-page` - to indicate that the link is to a web page
`email` - to indicate that the link is to an email address
`phone` - to indicate that the link is to a phone number

Also note that `fh-new-tab` is deprecated, please use `new-tab` instead. `fh-new-tab` will be removed once all known consumers have been updated.

See the example page for more info.

### Filtering

Initial support for filtering, based on the [MOJ Filter](https://design-patterns.service.justice.gov.uk/components/filter/) component.

Example and documentation to follow.

## 5.2

* Make the feedback link in the phase banner open in the same tab.

* Add a tag helper for anchors that open in a new tab. To use...

Add this to your `_ViewImports.cshtml` file:

```
@addTagHelper *, FamilyHubs.SharedKernel.Razor
```

Then add the `fh-new-tab` attribute to the anchor element. That will add the relevant attributes to open the link in a new tab and add " (opens in new tab)" to the link text.

```
<a href="https://www.gov.uk" fh-new-tab>GOV.UK</a>
```

## 5.1

Add a 'Contact Us' partial view.

Add `InputErrorMessageParaId` to `Error` and use it to set the id on the `<p>` element in the `_ErrorMessage` partial view.

Further error handling improvements.

# 5

### Content Security Policy (CSP) changes

The CSP now has inline scripts disabled and nonce support for scripts has been added.

To add a nonce to an inline script:

#### Add a using directive for the TagHelpers

Add the following to the `_ViewImports.cshtml` file in your application. This makes the tag-helper available in your Razor views.

```
@addTagHelper *, NetEscapades.AspNetCore.SecurityHeaders.TagHelpers
```

#### Whitelist elements using the TagHelpers
Add the NonceTagHelper to an element by adding the asp-add-nonce attribute.

```
<script asp-add-nonce>
```

For more info, see [NetEscapades.AspNetCore.SecurityHeaders](https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders).

### Remove IE support.

[GDS no longer requires IE support](https://technology.blog.gov.uk/2022/06/16/service-manual-testing-requirement-changes-for-internet-explorer-11/) (and we don't test IE), so we've removed support for it.

### Other changes

* add _ErrorMessage partial view (and an example error handling page)
* fix family hubs js mappping for easier debugging
* further examples added

# 4

Breaking changes:
* security header's CSP has had `unsafe-inline` removed from the `style-src`
* Https has been removed from img-src (but GA specific domains have been added)

* 4.0.1 : Add more domain exceptions to the CSP to fix GA4 (as [recommended by Google](https://developers.google.com/tag-platform/security/guides/csp#google_analytics_4_google_analytics)).

## 3.1

Add support for Areas by adding the `ViewStart.InitialiseFamilyHubs()` method that can be called from an Area's `_ViewStart.cshtml`, to initialise Family Hubs for the Area.

* 3.1.1 : Remove link to homepage from 404 & 500 pages.

# 3

Breaking changes:
* the class `app-back-link` has been renamed to `fh-back-link`
* the behaviour of back links with the class has also been changed, to not show the back link, if the page has been opened in a new tab.
* the class `app-custom-main` has been renamed to `fh-custom-main`.

Fixes typescript transpilation.

* 3.0.1 : Fixed layout issue on cookie page.

## 2.5

Rename `custom-main` class on the `<main>` element to `app-custom-main`.

## 2.4

Add `PathPrefix` to `FamilyHubsUiOptions`. If supplied,
the prefix will be prepended to all files included through the layout,
e.g. css, js and asset files.

Useful for when the site is being used behind an App Gateway using path based routing.

## 2.3

Update libraries:
* [MOJ Frontend to v1.8.0 (from v1.6.6)](https://github.com/ministryofjustice/moj-frontend/blob/main/CHANGELOG.md)
* [jQuery to v3.7.1 (from v3.7.0)](https://github.com/jquery/jquery/releases).

Patches:
* 2.3.1 : Update phase banner wording.

## 2.2

When calling `Url<>()` on an alternative `FamilyHubsUiOptions`, if the Url isn't present in the alternative's Url config section, it will traverse the alternative's ancestors looking for the nearest instance of the Url.

This allows inheritance of Urls from ancestors, whilst still allowing the overriding of Urls.

* 2.2.1 : Support BaseUrl inheritance in configured links.

## 2.1

Add support to allow link status to be set in config and/or during [Navigation|Action]Links() calls.

# 2

New major version as contains breaking changes.

Improvements to header links handling to provide more flexibility.

`ServiceName` in `FamilyHubsUiOptions` is now optional. This allows consumers to use components without having to use the layout or provide a dummy `ServiceName`.

Update [GOV.UK Frontend to v4.7.0](https://github.com/alphagov/govuk-frontend/releases).

## 1.16

Add ability to change header links per page.

See, `IFamilyHubsHeader.NavigationLinks()` and `IFamilyHubsHeader.ActionLinks()`.

## 1.15

`_Layout.cshtml` now has a `Head` section for inserting page specific content at the end of the head section.

* 1.15.1 : Fix reference to our js file in the layout.

## 1.14

Support query params/fragment in the relativeUrl param to `FamilyHubsUiOptions.Url<>()`.

## 1.13

Add support for header and footer links that have a `BaseUrlKey` value that contains a path component.

## 1.12

Fix `FamilyHubsUiOptions.Url<>()` when the base URL has a path component.

## 1.11

Add `GetAlternative()` method to `FamilyHubsUiOptions` to get alternative service config.

## 1.10

Add `GetFamilyHubsUiOptions()` and other helper extension methods to ViewDataDictionary

## 1.9 & 1.8

Add support for alternative service layout by page (see Alternative example page)

## 1.7

Adds optional configurable link for the header (see `Header:ServiceNameLink`) 

## 1.6

Add `Url()` helper to FamilyHubsUiOptions

## 1.5

Adds:
* BaseUrlKey support in header and footer links

## 1.4

Adds:
* Url helpers (`Html.FamilyHubUrl` & `Html.FamilyHubConfigUrl`)
* static DontShow pagination instances (`IPagination.DontShow` & `ILinkPagination.DontShow`)

See the example site for usage.

Notes:
* This version requires `_ViewStart.cshtml` to not be overridden in the consuming project.
If you need to override `_ViewStart.cshtml`, ensure the contents of the version in this package is included in your overridden version. A later version might contain a helper for this.

## 1.3 and below

Lost in the mists of time.