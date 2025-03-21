# Google Analytics

Google Analytics (GA) is a web analytics service provided by Google. It can be
used to track website traffic and events.

Currently GA is available for 'Single directory' and 'Manage' with projects for
both the Production and Test URLs.

# Access

Access to GA can be obtained by sending a request to the helpdesk. It’s worth
noting that a Google account is required for this, but there is a process to
link a DfE account to a google account detailed
[here](https://measureu.com/access-google-analytics-without-gmail/#Option_1_Use_your_own_email_address_to_access_GA)

# Links

Copied below are links to the GA documentation as well as the 6 Project URLs.

[Single directory Test](https://analytics.google.com/analytics/web/?authuser=2#/p351124658/reports/intelligenthome)
[Manage Test](https://analytics.google.com/analytics/web/?authuser=2#/p439164411/reports/intelligenthome)

[Single directory Production](https://analytics.google.com/analytics/web/?authuser=2#/p347101383/reports/intelligenthome)
[Manage Production](https://analytics.google.com/analytics/web/?authuser=2#/p438768701/reports/intelligenthome)

[GA Help / Documentation](https://support.google.com/analytics/#topic=14090456)

# Measurement IDs

Sites are linked to their related GA projects via a Measurement ID (located in
the <head> of the site. In the table below is a list of sites and Measurement
IDs in the Family Hubs estate.

| Website | Measurement ID |
| --- | --- |
| Single directory (Test) | G-HXVL3XGHE2 |
| Manage (Test) | G-2VRSHGP9CY |
| Single directory (Prod) | G-0FDRRXT218 |
| Manage (Prod) | G-0BB0C0YPYX |

# Configuration

## Page view event

### Filter page

The page view event for the filter page contains query parameters representing the current search criteria, page number etc. The parameters can be found in the page\_location or page\_path parameters. One view of the page view events can be found in the Analytics dashboard [here](https://analytics.google.com/analytics/web/#/p351124983/reports/explorer?params=_u..nav%3Dmaui%26_r.explorerCard..selmet%3D%5B%22screenPageViews%22%5D%26_r.explorerCard..seldim%3D%5B%22unifiedPagePathScreen%22%5D&r=all-pages-and-screens&collectionId=4468230811).

Here’s an example path…

```
/ServiceFilter?postcode=M27
&adminarea=E08000006
&latitude=53.508884
&longitude=-2.294605
&activities=activities,school-clubs
&family-support=bullying,debt
&cost=free,pay-to-use
&show=family-hubs,services
&search\_within=20
&children\_and\_young-option-selected=true
&children\_and\_young=14
```

Here’s a breakdown of each parameter:

| Query Parameter | Notes | Example Value |
| --- | --- | --- |
| postcode | The normalised outcode portion of the postcode that was entered into the postcode search page, which is PII safe. | M27 |
| adminarea | The LA administrative area, which can be at the county or district/unitary authority level, to which the postcode has been assigned. | E08000006 |
| activities | The (comma separated) selected ‘**Activities, clubs and groups**’ sub-categories.  The possible values are:  **activities**  **school-clubs**  **holiday-clubs**  **mad**  **baby-groups**  **playgroup**  **sports** | activities,school-clubs |
| family-support | The (comma separated) selected ‘**Family support**’ sub-categories.  The possible values are:  **bullying**  **debt**  **domestic-abuse**  **family-support**  **money**  **parenting**  **parent-conflict**  **separating-parents**  **smoking**  **substance-misuse**  **youth-support**  **youth-justice** | smoking,substance-misuse |
| health | The (comma separated) selected ‘**Health**’ sub-categories.  The possible values are:  **hearing-sight**  **mental-health**  **nutrition**  **oral**  **public-health** | hearing-sight |
| pregnancy | The (comma separated) selected ‘**Pregnancy, birth and early years**’ sub-categories.  The possible values are:  birth-registration  early-years  health-visiting  infant-feeding  midwife-maternity  perinatal-mental-health | early-years |
| send | The (comma separated) selected ‘**Special educational needs and disabilities (SEND) support**’ sub-categories.  The possible values are:  **asd**  **breaks-respite**  **early-years**  **send**  **hearing-impairment**  **learning-difficulties**  **multi-sensory-impairment**  **other-difficulties**  **physical-disabilities**  **social-emotional**  **speech-language**  **visual-impairment** | send |
| transport | The (comma separated) selected ‘**Transport**’ sub-categories.  The possible values are:  **community-transport** | community-transport |
| cost | The selected ‘**Cost**’ options, comma separated.  The possible values are:  **free**  **pay-to-use** | free,pay-to-use |
| show | The selected ‘**Show**’ options, comma separated.  The possible values are:  **family-hubs**  **services** | family-hubs,services |
| search\_within | The (single) selected ‘**Search within**’ option.  The possible values are:  **1, 2, 5, 10, 20** | 20 |
| children\_and\_young-option-selected | Whether the user has checked the ‘For children and young people’ checkbox under ‘**Children and young people**’.  The possible values are:  **true, false** | true |
| children\_and\_young | The (single) selected ‘**For children and young people**’ value.  Possible values are:  **all, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25** | 0 |
| frompostcodesearch | Whether the user has just entered the filter page from the postcode search page. Is absent from the moment the user performs an ‘Apply filters’ or removes any filters.  Possible values are: **True, False** | True |
| pageNum | The page number of the results being viewed, corresponding to the number clicked in the pagination control (1 being the first page) | 1 |

Notes

The URL in the address bar will show the full postcode and also the latitude and longitude. Before the URL is sent to GA, the latitude and longitude is removed and the postcode shortened to comply with the GDPR.

### Postcode Search

When we report an error to the user, related to the postcode they entered, the URL will contain a query parameter called postcodeError, e.g.:

postcodeError=InvalidPostcode

The possible values are

| Value | Scenario |
| --- | --- |
| NoPostcode | No postcode supplied |
| InvalidPostcode | Postcode in an invalid format |
| PostcodeNotFound | Postcode is in a valid format, but not found |

### Service Errors and 404’s

When a general error occurs, the page title will be:

> Sorry, there is a problem with the service

When a page is not found, the page title will be:

> Page not found

## Custom Events

Our custom events can be viewed in the [Analytics](https://analytics.google.com/analytics/web/#/p351124983/reports/dashboard?params=_u..nav%3Dmaui%26_r..dimension-value%3D%7B%22dimension%22:%22eventName%22,%22value%22:%22filter_page%22%7D&r=events-overview&collectionId=4468230811) dashboard.

Here’s [GA4 docs on reporting using custom events](https://support.google.com/analytics/answer/12229021?hl=en). We’ll probably need to report on custom event parameters, so we’ll have to [create custom dimensions or metric](https://support.google.com/analytics/answer/10075209)s to use them in reports.

### Analytics custom event

A custom event that indicates when a user has accepted or rejected analytics cookies.

The event has 2 custom params,
accepted=true|false: whether an user has accepted or rejected analytics cookies
source=banner|cookies: whether the decision was made via the banner or the cookies page

Use the custom source parameter to determine if analytics was accepted or rejected from the global cookie banner, or from the cookie page. Looking for a 'page\_path' of '/cookies' isn't appropriate to use, as the user might have interacted with the cookie banner whilst on the cookies page.

Note: to meet the requirement of counting the number of users who decline analytics cookies, when an user declines cookies, we enable consent, create a cookie, sent a custom event, then disable consent and delete the analytics cookies.

### filter\_page custom event

The filter\_page custom event has a parameter called total\_results, which contains the number of results that match the search criteria. It can be used to see how many searches returned results, and when it contains 0, it indicates that no results matched.

Examining the referrer on the filter page page\_view (or filter\_page) event, will allow us to see if the user has been through the standard journey and entered a postcode, or if they’ve navigated straight to a results page (e.g. via a bookmark). This will be more reliable than looking for the query param ‘frompostcodesearch’, as that could be part of the bookmark.